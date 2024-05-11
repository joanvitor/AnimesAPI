using System.Linq.Expressions;
using Anime.Dominio.Interfaces.Marcadores;

namespace Anime.Dominio.Interfaces.OperacaoesBancoDeDados
{
    public interface ICRUD<TEntidade> where TEntidade : class, IEntidadeDominio
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