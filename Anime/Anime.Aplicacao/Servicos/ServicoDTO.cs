using Anime.Aplicacao.Interfaces.Marcadores;
using Anime.Aplicacao.Interfaces.Servicos;
using AutoMapper;
using System.Linq.Expressions;
using Anime.Dominio.Interfaces.Marcadores;
using Anime.Dominio.Interfaces.Repositorios;
using Anime.Aplicacao.Conversores;
using AutoMapper.QueryableExtensions;

namespace Anime.Aplicacao.Servicos
{
    public class ServicoDTO<TEntidade, TEntidadeDTO> : IServicoDTO<TEntidade, TEntidadeDTO> 
                                                            where TEntidadeDTO : IEntidadeDTO 
                                                            where TEntidade : class, IEntidadeDominio
    {
        private readonly IMapper _mapeador;
        private readonly IRepositorio<TEntidade> _repositorio;

        public ServicoDTO(IMapper mapeador, IRepositorio<TEntidade> repositorio)
        {
            _mapeador = mapeador;
            _repositorio = repositorio;
        }

        public void Atualizar(TEntidadeDTO entidadeDto)
        {
            var entidadeDominio = _mapeador.Map<TEntidade>(entidadeDto);

            _repositorio.Atualizar(entidadeDominio);
        }

        public IQueryable<TEntidadeDTO> Buscar(Expression<Func<TEntidadeDTO, bool>> expressaoWhere)
        {
            var expressaoDominio = ConversorExpressao.Converter<TEntidade, TEntidadeDTO>(expressaoWhere);

            var queryEntidadeDominio = _repositorio.Buscar(expressaoDominio).AsQueryable();

            return queryEntidadeDominio.ProjectTo<TEntidadeDTO>(_mapeador.ConfigurationProvider);
        }

        public TEntidadeDTO Buscar(int codigo)
        {
            var entidadeDominio = _repositorio.Buscar(codigo);

            var entidadeDto = _mapeador.Map<TEntidadeDTO>(entidadeDominio);

            return entidadeDto;
        }

        public IQueryable<TEntidadeDTO> BuscarPaginado(int numeroDaPagina, int quantidadeEmUmaPagina)
        {
            var queryEntidadeDominio = _repositorio.BuscarPaginado(numeroDaPagina, quantidadeEmUmaPagina).AsQueryable();

            return queryEntidadeDominio.ProjectTo<TEntidadeDTO>(_mapeador.ConfigurationProvider);
        }

        public IQueryable<TEntidadeDTO> BuscarTodos()
        {
            var queryEntidadeDominio = _repositorio.BuscarTodos().AsQueryable();

            return queryEntidadeDominio.ProjectTo<TEntidadeDTO>(_mapeador.ConfigurationProvider);
        }

        public void Cadastrar(TEntidadeDTO entidadeDto)
        {
            var entidadeDominio = _mapeador.Map<TEntidade>(entidadeDto);

            _repositorio.Cadastrar(entidadeDominio);
        }

        public void Excluir(TEntidadeDTO entidadeDto)
        {
            var entidadeDominio = _mapeador.Map<TEntidade>(entidadeDto);

            _repositorio.Excluir(entidadeDominio);
        }

        public void Salvar()
        {
            _repositorio.Salvar();
        }
    }
}