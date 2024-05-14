using Anime.Dominio.Interfaces.Marcadores;
using Anime.Dominio.Interfaces.OperacaoesBancoDeDados;

namespace Anime.Dominio.Interfaces.Repositorios
{
    public interface IRepositorio<TEntidade> : ICRUD<TEntidade> where TEntidade : class, IEntidadeDominio
    {
    }
}