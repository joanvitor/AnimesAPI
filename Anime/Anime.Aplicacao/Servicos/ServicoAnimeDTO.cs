using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Aplicacao.DTOs;
using Anime.Dominio.Entidades;
using Anime.Dominio.Interfaces.Repositorios;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Anime.Aplicacao.Servicos
{
    public class ServicoAnimeDTO : ServicoDTO<Dominio.Entidades.Anime, AnimeDTO>, IServicoAnimeDTO
    {
        private readonly IMapper _mapeador;
        private readonly IRepositorioAnime _repositorio;
        private readonly IRepositorio<Diretor> _repositorioDiretor;

        public ServicoAnimeDTO(IMapper mapeador, 
                               IRepositorioAnime repositorio, 
                               IRepositorio<Diretor> repositorioDiretor) : base(mapeador, repositorio)
        {
            _mapeador = mapeador;
            _repositorio = repositorio;
            _repositorioDiretor = repositorioDiretor;
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

        public void Cadastrar(AnimeDTO entidadeDto)
        {
            var diretor = _repositorioDiretor.Buscar(entidadeDto.DiretorCodigo);

            if (diretor is null)
                throw new Exception("Diretor não cadastrado!");

            var entidadeDominio = _mapeador.Map<Dominio.Entidades.Anime>(entidadeDto);

            _repositorio.Cadastrar(entidadeDominio);
        }
    }
}