BACKUP DATABASE Products
TO DISK = 'C:\Users\JOEL\Desktop\Products_Backup.bak'
WITH FORMAT,
     NAME = 'Backup de Products',
     DESCRIPTION = 'Backup completo de la base de datos Products',
     STATS = 10; 

PRINT 'Backup completado exitosamente.'

USE master;
ALTER DATABASE Products SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

RESTORE DATABASE Products
FROM DISK = 'C:\Users\JOEL\Desktop\Products_Backup.bak'
WITH REPLACE, 
     MOVE 'Products' TO 'C:\Users\JOEL\Desktop\Products_Data.mdf',
     MOVE 'Products_Log' TO 'C:\Users\JOEL\Desktop\Products_Log.ldf',
     STATS = 10; 

ALTER DATABASE Products SET MULTI_USER;

PRINT 'Restauración completada exitosamente.'
