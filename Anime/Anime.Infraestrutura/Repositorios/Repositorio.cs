using System.Linq.Expressions;
using Anime.Dominio.Interfaces.Entidades;
using Anime.Dominio.Interfaces.Repositorios;

namespace Anime.Infraestrutura.Repositorios
{
    public class Repositorio<TEntidade> : IRepositorio<TEntidade> where TEntidade : class, IEntidade
    {
        private readonly Contexto.Contexto _contexto;
        public Repositorio(Contexto.Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Atualizar(TEntidade entidade)
        {
            _contexto.Update(entidade);
        }

        public IQueryable<TEntidade> Buscar(Expression<Func<TEntidade, bool>> expressaoWhere)
        {
            return BuscarTodos().Where(expressaoWhere).AsQueryable();
        }

        public IQueryable<TEntidade> BuscarPaginado(int numeroDaPagina, int quantidadeEmUmaPagina)
        {
            var quantidadeRegistrosASeremIgnorados = (numeroDaPagina - 1) * quantidadeEmUmaPagina;

            return BuscarTodos().Skip(quantidadeRegistrosASeremIgnorados)
                                .Take(quantidadeEmUmaPagina)
                                .AsQueryable();
        }

        public IQueryable<TEntidade> BuscarTodos()
        {
            return _contexto.Set<TEntidade>().AsQueryable();
        }

        public void Cadastrar(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Add(entidade);
        }

        public void Excluir(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Remove(entidade);
        }

        public void Salvar()
        {
            _contexto.SaveChanges();
        }
    }
}