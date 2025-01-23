using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ProductsInventory.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
builder.WebHost.ConfigureKestrel(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.ListenAnyIP(5144); 
    }
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();