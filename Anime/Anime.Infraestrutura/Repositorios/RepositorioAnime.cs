using Anime.Dominio.Interfaces.Repositorios;

namespace Anime.Infraestrutura.Repositorios
{
    public class RepositorioAnime : Repositorio<Dominio.Entidades.Anime>, IRepositorioAnime
    {
        public RepositorioAnime(Contexto.Contexto contexto) : base(contexto)
        {
        }

        public void RemoverLogicamente(Dominio.Entidades.Anime anime)
        {
            var animeBanco = Buscar(x => x.Codigo == anime.Codigo).SingleOrDefault();

            animeBanco.DefinirApagado(true);
        }
    }
}