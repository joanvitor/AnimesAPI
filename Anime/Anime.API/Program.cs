using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Aplicacao.Mapeador;
using Anime.Aplicacao.Servicos;
using Anime.Aplicacao.Utils;
using Anime.Dominio.Interfaces.Repositorios;
using Anime.Infraestrutura.Contexto;
using Anime.Infraestrutura.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Contexto>(op =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConexaoMySql");

    op.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAutoMapper(typeof(MapeadorDeDominioParaDTO));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Protech - Desafio técnico - API",
        Version = "v1.0.0",
        Description = "Criação de uma Web API",
        Contact = new OpenApiContact()
        {
            Name = "Joan Vitor Mendonça de Jesus",
            Email = "joanvitor1997@gmail.com",
            Url = new Uri("https://github.com/joanvitor")
        }
    });

    var arquivoXml = "Anime.API.xml";
    var caminhoXml = Path.Combine(AppContext.BaseDirectory, arquivoXml);
    x.IncludeXmlComments(caminhoXml);
});

// Autenticação
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ChaveSecreta:chave"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IRepositorioAnime, RepositorioAnime>();

builder.Services.AddScoped(typeof(IServicoDTO<,>), typeof(ServicoDTO<,>));
builder.Services.AddScoped<IServicoAnimeDTO, ServicoAnimeDTO>();
builder.Services.AddScoped(typeof(IServicoToken), typeof(ServicoToken));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("v1/swagger.json", "API V1");
    });
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();