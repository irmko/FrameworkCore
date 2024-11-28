using SkyNET.Framework.Common.Interface;
using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq;

public static class QueryableFilterByExtensions {
    public static IQueryable<T> FilterBy<T, TK>(this IQueryable<T> context, TK entry, Expression<Func<T, TK>> fieldExpr) {
        return entry == null ? context : context.Where(Predicate.FilterBy(fieldExpr, entry));
    }

    public static IQueryable<T> FilterBy<T>(this IQueryable<T> context, string entry, Expression<Func<T, string>> fieldExpr) {
        return string.IsNullOrWhiteSpace(entry) ? context : context.Where(Predicate.FilterBy(fieldExpr, entry));
    }

    public static IQueryable<T> FilterBy<T, TK>(this IQueryable<T> context, ICollection<TK> entries, Expression<Func<T, TK>> fieldExpr) {
        return entries == null || !entries.Any() ? context : context.Where(Predicate.FilterBy(fieldExpr, entries));
    }

    public static IQueryable<T> FilterBy<T>(this IQueryable<T> context, Guid entry, Expression<Func<T, Guid?>> fieldExpr) {
        return entry == default ? context : context.Where(Predicate.FilterBy(fieldExpr, entry));
    }

    public static IQueryable<T> FilterBy<T>(this IQueryable<T> context, ICollection<Guid> entries, Expression<Func<T, Guid?>> fieldExpr) {
        return entries == null || !entries.Any() ? context : context.Where(Predicate.FilterBy(fieldExpr, entries));
    }

    public static IQueryable<T> FilterBy<T, TM, TK>(this IQueryable<T> context, ICollection<TM> entries, Expression<Func<T, TK>> fieldExpr)
        where TM : INamedEntry<TK> {
        return entries == null || !entries.Any() ? context : context.Where(Predicate.FilterBy(fieldExpr, entries));
    }

    public static IQueryable<T> FilterBy<T, TM>(this IQueryable<T> context, ICollection<TM> entries, Expression<Func<T, Guid?>> fieldExpr)
        where TM : class, INamedEntry {
        return entries == null || !entries.Any() ? context : context.Where(Predicate.FilterBy(fieldExpr, entries));
    }
}
