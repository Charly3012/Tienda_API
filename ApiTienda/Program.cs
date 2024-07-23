using ApiTienda.Data;
using ApiTienda.Repository.IRepository;
using ApiTienda.Repository;
using Microsoft.EntityFrameworkCore;
using ApiTienda.TiendaMapper;
using ApiTienda.Services.IServices;
using ApiTienda.Services;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

//Configuraciones para json
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//    });

// Add services to the container.

builder.Services.AddDbContext<ApplicationDBContext>(options =>
                                                    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));
builder.Services.AddScoped<IProductoCategoriaService, ProductoCategoriaService>();
builder.Services.AddScoped<IVentaService, VentaService>();

//Agregar los repositorios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();


//Agregar el automapper
builder.Services.AddAutoMapper(typeof(TiendaMapper));


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
