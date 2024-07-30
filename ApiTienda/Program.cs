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

string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??     
                          builder.Configuration.GetConnectionString("ConexionSQL");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
                                                    options.UseSqlServer(connectionString));
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

//Soporte para cors
builder.Services.AddCors(p => p.AddPolicy("PoliticaCors",build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8080); // Puerto HTTP
    serverOptions.ListenAnyIP(8081, listenOptions =>
    {
        listenOptions.UseHttps(); // Puerto HTTPS
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiTienda V1");
    c.RoutePrefix = string.Empty; // Serve Swagger at the root URL
});


//app.UseHttpsRedirection();

//Soporte cors
app.UseCors("PoliticaCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
