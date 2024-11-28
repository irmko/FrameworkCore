using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq {
    /// <summary>
    /// Строит запросы для поиска объектов в заданном дипазоне
    /// </summary>
    public static class QueryableWhereInRangeExtensions {
        /// <summary>
        /// Найти объекты у которых {field} между {from} и {to}
        /// </summary>
        public static IQueryable<TEnt> WhereInRange<TEnt, TProp>(
            this IQueryable<TEnt> source,
            Expression<Func<TEnt, TProp?>> field,
            TProp? from, TProp? to,
            bool excludeFrom = false,
            bool excludeTo = false)
            where TEnt : class
            where TProp : struct {

            if (!from.HasValue && !to.HasValue)
                return source;

            return source.Where(Predicate.WhereInRange(
                field: field,
                from: from,
                to: to,
                excludeFrom: excludeFrom,
                excludeTo: excludeTo
            ));
        }

        /// <summary>
        /// Найти объекты у которых {field} между {from} и {to}
        /// </summary>
        public static IQueryable<TEnt> WhereInRange<TEnt, TProp>(
            this IQueryable<TEnt> source,
            Expression<Func<TEnt, TProp>> field,
            TProp? from, TProp? to,
            bool excludeFrom = false,
            bool excludeTo = false)
            where TEnt : class
            where TProp : struct {

            if (!from.HasValue && !to.HasValue)
                return source;

            return source.Where(Predicate.WhereInRange(
                field: field,
                from: from,
                to: to,
                excludeFrom: excludeFrom,
                excludeTo: excludeTo
            ));
        }
    }
}
