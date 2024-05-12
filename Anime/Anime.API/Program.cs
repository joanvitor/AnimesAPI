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

builder.Services.AddAutoMapper(typeof(MapeadorDeDominioParaDTO));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(x =>
//{
//    x.SwaggerDoc("versão 01", new OpenApiInfo
//    {
//        Title = "Titulo da API",
//        Version = "v1.0.0",
//        Description = "Descrição da api"
//    });
//});

builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IRepositorioAnime, RepositorioAnime>();

builder.Services.AddScoped(typeof(IServicoDTO<,>), typeof(ServicoDTO<,>));
builder.Services.AddScoped<IServicoAnimeDTO, ServicoAnimeDTO>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();