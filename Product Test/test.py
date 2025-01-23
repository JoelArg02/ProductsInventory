import pytest
import logging
import random
import string
from sqlalchemy import create_engine, text
from sqlalchemy.orm import sessionmaker

# Configuración de logging
logging.basicConfig(level=logging.INFO, format="%(asctime)s - %(levelname)s - %(message)s")
logger = logging.getLogger(__name__)

# Conexión a tu base de datos existente
CONNECTION_STRING = "mssql+pyodbc://sa:YourStrong!Passw0rd@localhost/Products?driver=ODBC+Driver+17+for+SQL+Server&TrustServerCertificate=yes"
engine = create_engine(CONNECTION_STRING)
Session = sessionmaker(bind=engine)

@pytest.fixture(scope='module')
def session():
    session = Session()
    yield session
    session.close()

def random_string(length=8):
    """Generar un string aleatorio para nombres únicos."""
    return ''.join(random.choices(string.ascii_letters, k=length))

def test_category_product_relationship(session):
    category_name = f"Category_{random_string()}"
    product_name = f"Product_{random_string()}"
    
    logger.info("Iniciando prueba de relación entre categorías y productos.")
    
    try:
        # Crear una categoría y un producto relacionado
        logger.info(f"Creando categoría '{category_name}'.")
        session.execute(text(f"""
            INSERT INTO Categories (Name, Description)
            VALUES ('{category_name}', 'Descripción de {category_name}')
        """))

        logger.info(f"Creando producto '{product_name}' relacionado con la categoría '{category_name}'.")
        session.execute(text(f"""
            INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
            VALUES ('{product_name}', 'Descripción de {product_name}', 1500.00, 10, 
                    (SELECT TOP 1 Id FROM Categories WHERE Name = '{category_name}'))
        """))
        session.commit()

        # Verificar la relación
        logger.info("Verificando la relación entre el producto y la categoría.")
        result = session.execute(text(f"""
            SELECT p.Name, c.Name
            FROM Products p
            JOIN Categories c ON p.CategoryId = c.Id
            WHERE p.Name = '{product_name}'
        """)).fetchone()
        
        logger.info(f"Resultado de la consulta: {result}")
        print(f"Relación verificada: Producto '{result[0]}' pertenece a la categoría '{result[1]}'.")
        assert result is not None
        assert result[0] == product_name  # Producto
        assert result[1] == category_name  # Categoría
    except Exception as e:
        logger.error(f"Error durante la prueba: {e}")
        raise

def test_product_sale_relationship(session):
    # Generar nombres aleatorios para garantizar unicidad
    customer_first_name = f"Customer_{random_string()}"
    customer_last_name = f"Last_{random_string()}"
    customer_email = f"{random_string()}@example.com"
    product_name = f"Product_{random_string()}"
    category_name = "General"

    logger.info("Iniciando prueba de relación entre productos y ventas.")

    try:
        # Asegurar que la categoría 'General' exista
        logger.info(f"Verificando existencia de la categoría '{category_name}'.")
        session.execute(text(f"""
            IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = '{category_name}')
            BEGIN
                INSERT INTO Categories (Name, Description)
                VALUES ('{category_name}', 'Descripción de categoría general')
            END
        """))

        # Crear un cliente
        logger.info(f"Creando cliente '{customer_first_name} {customer_last_name}'.")
        session.execute(text(f"""
            INSERT INTO Customers (FirstName, LastName, Email, Phone)
            VALUES ('{customer_first_name}', '{customer_last_name}', '{customer_email}', '1234567890')
        """))

        # Crear un producto relacionado
        logger.info(f"Creando producto '{product_name}' relacionado con la categoría '{category_name}'.")
        session.execute(text(f"""
            INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
            VALUES ('{product_name}', 'Descripción de {product_name}', 2000.00, 5,
                    (SELECT TOP 1 Id FROM Categories WHERE Name = '{category_name}'))
        """))
        session.commit()

        # Crear una venta
        logger.info(f"Creando venta para el producto '{product_name}' relacionado con el cliente '{customer_first_name} {customer_last_name}'.")
        session.execute(text(f"""
            INSERT INTO Sales (ProductId, CustomerId, Quantity, SaleDate, Total)
            VALUES (
                (SELECT TOP 1 Id FROM Products WHERE Name = '{product_name}'),
                (SELECT TOP 1 Id FROM Customers WHERE Email = '{customer_email}'),
                1, GETDATE(), 2000.00
            )
        """))
        session.commit()

        # Verificar la relación
        logger.info("Verificando la relación entre la venta, el producto y el cliente.")
        result = session.execute(text(f"""
            SELECT s.Quantity, p.Name, c.FirstName, c.LastName
            FROM Sales s
            JOIN Products p ON s.ProductId = p.Id
            JOIN Customers c ON s.CustomerId = c.Id
            WHERE p.Name = '{product_name}'
        """)).fetchone()
        
        logger.info(f"Resultado de la consulta: {result}")
        print(f"Relación verificada: Cliente '{result[2]} {result[3]}' compró '{result[1]}' (cantidad: {result[0]}).")
        assert result is not None
        assert result[0] == 1  # Cantidad
        assert result[1] == product_name  # Producto
        assert result[2] == customer_first_name  # Cliente Nombre
        assert result[3] == customer_last_name  # Cliente Apellido
    except Exception as e:
        logger.error(f"Error durante la prueba: {e}")
        raise
