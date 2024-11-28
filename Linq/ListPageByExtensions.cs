using SkyNET.Framework.Common.Query;

namespace SkyNET.Framework.Common.Linq; 
public static class ListPageByExtensions {
    public static IList<T> PageBy<T>(this IList<T> query, IPaging paging) {
        if (paging != null) {
            query = query.PageBy(paging.Skip, paging.Take);
        }

        return query;
    }

    public static IList<T> PageBy<T>(this IList<T> query, int? skip, int? take) {
        if (skip != null && skip > 0) {
            query = query.Skip(skip.Value).ToList();
        }

        if (take != null && take > 0) {
            query = query.Take(take.Value).ToList();
        }

        return query;
    }
}
