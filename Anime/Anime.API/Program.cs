using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Aplicacao.Mapeador;
using Anime.Aplicacao.Servicos;
using Anime.Dominio.Interfaces.Repositorios;
using Anime.Infraestrutura.Contexto;
using Anime.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Contexto>(op =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConexaoMySql");

    op.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapeadorDeDominioParaDTO));

builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IRepositorioAnime, RepositorioAnime>();

builder.Services.AddScoped(typeof(IServicoDTO<,>), typeof(ServicoDTO<,>));
builder.Services.AddScoped<IServicoAnimeDTO, ServicoAnimeDTO>();


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
