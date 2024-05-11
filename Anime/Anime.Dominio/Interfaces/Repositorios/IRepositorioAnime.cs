namespace Anime.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioAnime : IRepositorio<Dominio.Entidades.Anime>
    {
        void RemoverLogicamente(Dominio.Entidades.Anime anime);
    }
}