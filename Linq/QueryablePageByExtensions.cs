using SkyNET.Framework.Common.Query;

namespace SkyNET.Framework.Common.Linq; 
public static class QueryablePageByExtensions {
    public static IQueryable<T> PageBy<T>(this IQueryable<T> query, IPaging paging) {
        if (paging != null && paging.TakeAll != true) {
            query = query.PageBy(paging.Skip, paging.Take);
        }
        return query;
    }

    public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int? skip, int? take) {
        if (skip != null && skip > 0) {
            query = query.Skip(skip.Value);
        }
        if (take != null && take > 0) {
            query = query.Take(take.Value);
        }
        return query;
    }
}
