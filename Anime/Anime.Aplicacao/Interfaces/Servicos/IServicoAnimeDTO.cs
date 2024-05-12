using Anime.Aplicacao.DTOs;

namespace Anime.Aplicacao.Interfaces.Servicos
{
    public interface IServicoAnimeDTO : IServicoDTO<Dominio.Entidades.Anime, AnimeDTO>
    {
        void RemoverLogicamente(AnimeDTO animeDto);
        IQueryable<AnimeDTO> ObterAtivos();
    }
}