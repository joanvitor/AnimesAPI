using Anime.Dominio.Interfaces.Entidades;
using System.Linq.Expressions;

namespace Anime.Dominio.Interfaces.OperacaoesBancoDeDados
{
    public interface ICRUD<TEntidade> where TEntidade : class, IEntidade
    {
        void Cadastrar(TEntidade entidade);
        IQueryable<TEntidade> Buscar(Expression<Func<TEntidade, bool>> expressaoWhere);
        IQueryable<TEntidade> BuscarTodos();
        IQueryable<TEntidade> BuscarPaginado(int numeroDaPagina, int quantidadeEmUmaPagina);
        void Atualizar(TEntidade entidade);
        void Excluir(TEntidade entidade);
        void Salvar();
    }
}