using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq {
    /// <summary>
    /// Строит запросы поиска объектов пересекающихся с заданным диапазоном
    /// </summary>
    public static class QueryableWhereOverlapExtensions {
        /// <summary>
        /// Найти объекты попавшие в диапазон
        /// </summary>
        public static IQueryable<TEnt> WhereOverlap<TEnt, TProp>(
            this IQueryable<TEnt> source,
            Expression<Func<TEnt, TProp?>> fromField,
            Expression<Func<TEnt, TProp?>> toField,
            TProp? from, TProp? to,
            bool excludeFrom = false,
            bool excludeTo = false)
            where TEnt : class
            where TProp : struct {

            if (!from.HasValue && !to.HasValue)
                return source;

            return source.Where(Predicate.WhereOverlap(
                fromField: fromField,
                toField: toField,
                from: from,
                to: to,
                excludeFrom: excludeFrom,
                excludeTo: excludeTo
            ));
        }

        /// <summary>
        /// Найти объекты попавшие в диапазон
        /// </summary>
        public static IQueryable<TEnt> WhereOverlap<TEnt, TProp>(
            this IQueryable<TEnt> source,
            Expression<Func<TEnt, TProp>> fromField,
            Expression<Func<TEnt, TProp>> toField,
            TProp? from, TProp? to,
            bool excludeFrom = false,
            bool excludeTo = false)
            where TEnt : class
            where TProp : struct {

            if (!from.HasValue && !to.HasValue)
                return source;

            return source.Where(Predicate.WhereOverlap(
                fromField: fromField,
                toField: toField,
                from: from,
                to: to,
                excludeFrom: excludeFrom,
                excludeTo: excludeTo
            ));
        }

        /// <summary>
        /// Найти объекты попавшие в диапазон
        /// </summary>
        public static IQueryable<TEnt> WhereOverlap<TEnt, TProp>(
            this IQueryable<TEnt> source,
            Expression<Func<TEnt, TProp>> fromField,
            Expression<Func<TEnt, TProp?>> toField,
            TProp? from, TProp? to,
            bool excludeFrom = false,
            bool excludeTo = false)
            where TEnt : class
            where TProp : struct {

            if (!from.HasValue && !to.HasValue)
                return source;

            return source.Where(Predicate.WhereOverlap(
                fromField: fromField,
                toField: toField,
                from: from,
                to: to,
                excludeFrom: excludeFrom,
                excludeTo: excludeTo
            ));
        }

        /// <summary>
        /// Найти объекты попавшие в диапазон
        /// </summary>
        public static IQueryable<TEnt> WhereOverlap<TEnt, TProp>(
            this IQueryable<TEnt> source,
            Expression<Func<TEnt, TProp?>> fromField,
            Expression<Func<TEnt, TProp>> toField,
            TProp? from, TProp? to,
            bool excludeFrom = false,
            bool excludeTo = false)
            where TEnt : class
            where TProp : struct {

            if (!from.HasValue && !to.HasValue)
                return source;

            return source.Where(Predicate.WhereOverlap(
                fromField: fromField,
                toField: toField,
                from: from,
                to: to,
                excludeFrom: excludeFrom,
                excludeTo: excludeTo
            ));
        }
    }
}
