using Microsoft.EntityFrameworkCore;
using SistemaDeVentasCafe;
using SistemaDeVentasCafe.DTOs;
using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Repository;
using SistemaDeVentasCafe.Repository.IRepository;
using SistemaDeVentasCafe.Service;
using SistemaDeVentasCafe.Service.IService;
using SistemaDeVentasCafe.UnitOfWork;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Realizo la conexion con Dbapicontext conectando a la base de datos con la cadena de conexion.
builder.Services.AddDbContext<DbapiContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

//agrego automapper
builder.Services.AddAutoMapper(typeof(AutomapperConfig));
//APIResponse
builder.Services.AddScoped<APIResponse>();
//repository
builder.Services.AddScoped<IRepositoryGeneric<Cliente>, RepositoryCliente>();
builder.Services.AddScoped<IRepositoryGeneric<Producto>, RepositoryProducto>();
builder.Services.AddScoped<IRepositoryGeneric<Factura>, RepositoryFactura>();
builder.Services.AddScoped<IRepositoryFacturaProducto, RepositoryFacturaProducto>();
//service
builder.Services.AddScoped<IServiceGeneric<ClienteUpdateDto, ClienteCreateDto>, ServiceCliente>();
builder.Services.AddScoped<IServiceGeneric<ProductoUpdateDto, ProductoCreateDto>, ServiceProducto>();
builder.Services.AddScoped<IServiceFactura, ServiceFactura>();
//unitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Configuracion de conversion de JSON para que no se produzcan ciclos
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

