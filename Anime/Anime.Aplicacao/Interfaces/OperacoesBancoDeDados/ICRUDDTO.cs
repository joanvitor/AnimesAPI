using System.Linq.Expressions;
using Anime.Aplicacao.Interfaces.Marcadores;

namespace Anime.Aplicacao.Interfaces.OperacoesBancoDeDados
{
    public interface ICRUDDTO<TEntidade> where TEntidade: IEntidadeDTO
    {
        void Cadastrar(TEntidade entidadeDto);
        //IQueryable<TEntidade> Buscar(Expression<Func<TEntidade, bool>> expressaoWhere);
        TEntidade Buscar(int codigo);
        IQueryable<TEntidade> BuscarTodos();
        IQueryable<TEntidade> BuscarPaginado(int numeroDaPagina, int quantidadeEmUmaPagina);
        void Atualizar(TEntidade entidadeDto);
        void Excluir(TEntidade entidadeDto);
        void Salvar();
    }
}