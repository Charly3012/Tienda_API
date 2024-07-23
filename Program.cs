using ApiTienda.Data;
using ApiTienda.Repository.IRepository;
using ApiTienda.Repository;
using Microsoft.EntityFrameworkCore;
using ApiTienda.TiendaMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDBContext>(options =>
                                                    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

//Agregar los repositorios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();


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
