using CursoDez.Application.Interfaces;
using CursoDez.Application.UseCases;
using CursoDez.Infrastructure.Context;
using CursoDez.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registra as classes e interfaces no DI
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoUseCase, CursoUseCase>();

// Carregar configurações de banco de dados do appsettings
var dbConfig = builder.Configuration.GetSection("DatabaseConfig");

// Verifica qual banco de dados vai usar
if (dbConfig.GetValue<int>("DatabaseType") == 1)
{
    builder.Services.AddDbContext<CursoDezContextSQLServer>(options =>
        options.UseSqlServer(dbConfig.GetSection("SQLServer:ConnectionString").Value));
}
else if (dbConfig.GetValue<int>("DatabaseType") == 2)
{
    builder.Services.AddSingleton(new CursoDezContextMongoDB(
        dbConfig.GetSection("MongoDB:ConnectionString").Value,
        dbConfig.GetSection("MongoDB:DatabaseName").Value));
}

// Add services to the container.
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
