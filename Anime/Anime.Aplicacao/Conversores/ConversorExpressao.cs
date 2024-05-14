using System.Linq.Expressions;
using Anime.Aplicacao.Interfaces.Marcadores;
using Anime.Dominio.Interfaces.Marcadores;

namespace Anime.Aplicacao.Conversores
{
    public static class ConversorExpressao<TEntidade, TEntidadeDTO> where TEntidade : class, IEntidadeDominio 
                                                                    where TEntidadeDTO : IEntidadeDTO
    {
        public static Expression<Func<TEntidade, bool>> Converter(Expression<Func<TEntidadeDTO, bool>> expressao)
        {
            var parameter = Expression.Parameter(typeof(TEntidade), "x");

            // Substitui os parâmetros da expressão original pelos parâmetros da nova expressão
            var body = new ParameterReplacerVisitor(expressao.Parameters.Single(), parameter).Visit(expressao.Body);

            return Expression.Lambda<Func<TEntidade, bool>>(body, parameter);
        }

        private class ParameterReplacerVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _parametroAntigo;
            private readonly ParameterExpression _parametroNovo;

            public ParameterReplacerVisitor(ParameterExpression parametroAntigo, ParameterExpression parametroNovo)
            {
                _parametroAntigo = parametroAntigo;
                _parametroNovo = parametroNovo;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _parametroAntigo ? _parametroNovo : base.VisitParameter(node);
            }
        }
    }
}