using GerenciadorDePedidos.Core.Library.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Library.Extensoes
{
    public static class ExtensaoDeQueries
    {
        /// <summary>
        /// Realiza a paginação diretamente na consulta ao banco de dados.
        /// </summary>
        /// <typeparam name="TEntity">O tipo dos elementos na consulta de origem.</typeparam>
        /// <param name="query">A consulta de origem a ser paginada. Não pode ser nula.</param>
        /// <param name="page">O número da página (base 1) a ser recuperada. Deve ser maior ou igual a 1.</param>
        /// <param name="pageSize">O número de itens por página. Deve ser maior ou igual a 1.</param>
        /// <returns>Um <see cref="IQueryable{TEntity}"/> contendo os elementos para a página especificada. Se a página exceder o
        /// número de itens disponíveis, o resultado será vazio.</returns>
        /// <exception cref="ArgumentNullException">Lançada se <paramref name="query"/> for nula.</exception>
        public static IQueryable<TEntity> PaginacaoNoBanco<TEntity>(this IQueryable<TEntity> query, int page, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "query cannot be null");
            }
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static int GetCountTotal<T>(this IQueryable<T> query)
        {
            var queryCount = query;
            int count = queryCount.Count();
            return count;
        }

        /// <summary>
        /// Aplica filtros dinâmicos em uma query existente.
        /// </summary>
        /// <param name="query">Query base para aplicar os filtros</param>
        /// <param name="filtros">Lista de filtros dinâmicos</param>
        /// <returns>Query com os filtros aplicados</returns>
        public static IQueryable<TEntity> AplicarFiltroDinamico<TEntity>(this IQueryable<TEntity> query, IEnumerable<FiltrosDinamicos> filtros)
        {
            if (!filtros.Any())
                return query;

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            Expression? comparison = null;

            foreach (var item in filtros)
            {
                var property = Expression.Property(parameter, item.NomePropriedade);
                var propertyType = property.Type;


                var constantValue = Convert.ChangeType(item.ValorPropriedade, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                var constant = Expression.Constant(constantValue, propertyType);

                Expression currentComparison = item.TipoDeIgualdade switch
                {
                    TipoFiltroDinamico.Igual => Expression.Equal(property, constant),
                    TipoFiltroDinamico.Diferente => Expression.NotEqual(property, constant),
                    TipoFiltroDinamico.Maior => Expression.GreaterThan(property, constant),
                    TipoFiltroDinamico.Menos => Expression.LessThan(property, constant),
                    TipoFiltroDinamico.Contem => Expression.Call(property, "Contains", null, constant),

                    _ => throw new NotSupportedException($"Tipo de filtro {item.TipoDeIgualdade} não suportado.")
                };

                comparison = comparison == null ? currentComparison : Expression.AndAlso(comparison, currentComparison);
            }

            if (comparison != null)
            {
                var lambda = Expression.Lambda<Func<TEntity, bool>>(comparison, parameter);
                query = query.Where(lambda);
            }

            return query;
        }
    }

}
