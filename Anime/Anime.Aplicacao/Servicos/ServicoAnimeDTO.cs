using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Aplicacao.DTOs;
using Anime.Dominio.Interfaces.Repositorios;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Anime.Aplicacao.Servicos
{
    public class ServicoAnimeDTO : ServicoDTO<Dominio.Entidades.Anime, AnimeDTO>, IServicoAnimeDTO
    {
        private readonly IMapper _mapeador;
        private readonly IRepositorioAnime _repositorio;

        public ServicoAnimeDTO(IMapper mapeador, IRepositorioAnime repositorio) : base(mapeador, repositorio)
        {
            _mapeador = mapeador;
            _repositorio = repositorio;
        }

        public IQueryable<AnimeDTO> ObterAtivos()
        {
            var animesAtivos = _repositorio.BuscarTodos().Where(x => !x.Apagado);

            return animesAtivos.ProjectTo<AnimeDTO>(_mapeador.ConfigurationProvider);
        }

        public void RemoverLogicamente(AnimeDTO animeDto)
        {
            var entidadeDominio = _mapeador.Map<Dominio.Entidades.Anime>(animeDto);

            _repositorio.RemoverLogicamente(entidadeDominio);
        }
    }
}