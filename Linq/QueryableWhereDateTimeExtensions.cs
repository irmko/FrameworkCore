using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq {
    public static class QueryableWhereDateTimeExtensions {
        /// <summary>
        /// Найти объекты попавшие в заданный год
        /// </summary>
        public static IQueryable<TEnt> WhereOverlapYear<TEnt>(this IQueryable<TEnt> source,
            Expression<Func<TEnt, DateTime?>> fromField,
            Expression<Func<TEnt, DateTime?>> toField,
            int year)
            where TEnt : class {
            var yearStart = new DateTime(year, 1, 1);
            return source.WhereOverlap(
                fromField: fromField,
                toField: toField,
                from: yearStart,
                to: yearStart.AddYears(1),
                excludeTo: true);
        }

        /// <summary>
        /// Найти объекты у которых {field} в заданном году
        /// </summary>
        public static IQueryable<TEnt> WhereInYear<TEnt>(this IQueryable<TEnt> source,
            Expression<Func<TEnt, DateTime>> field,
            int year)
            where TEnt : class {
            var yearStart = new DateTime(year, 1, 1);
            return source.WhereInRange(
                field: field,
                from: yearStart,
                to: yearStart.AddYears(1),
                excludeTo: true);
        }
    }
}
