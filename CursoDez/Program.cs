using CursoDez.Application.Interfaces;
using CursoDez.Application.UseCases;
using CursoDez.Infrastructure.Context;
using CursoDez.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using CursoDez.Infrastructure.RabbitMQMessaging;

var builder = WebApplication.CreateBuilder(args);

// Registra as classes e interfaces no DI
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoUseCase, CursoUseCase>();

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoUseCase, AlunoUseCase>();

builder.Services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();

// Adicionando a configura��o do RabbitMQ
builder.Services.Configure<RabbitMqConfigSettings>(builder.Configuration.GetSection("RabbitMqConfig"));

// Carregar configura��es de banco de dados do appsettings
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

// Recupera a chave secreta ou o endpoint do provedor de autentica��o, se necess�rio.
var secretKey = "super_secret_key_1234567890123456";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Adiciona a configura��o para o JWT no Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Token JWT:",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configura��o do Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(@"C:\Logs\app-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "swagger"; // Faz com que o Swagger seja acess�vel apenas via /swagger
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();  // Valida��o do JWT
app.UseAuthorization();   // Autoriza��o de acessos aos endpoints

app.MapControllers();

app.Run();
