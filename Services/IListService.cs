using SkyNET.Framework.Common.Query;
using SkyNET.Framework.Common.Paged;

namespace SkyNET.Framework.Common.Services;
public interface IListService<TDto, in TFilter> where TFilter : IQueryObject {
    Task<PagedResult<TDto>> PagedListAsync(TFilter filter);
}

public static class ListServiceExtensions {
    public static Task<List<TDto>> ListAsync<TDto, TFilter>(this IListService<TDto, TFilter> service, TFilter filter) where TFilter : QueryObject {
        filter.Paging ??= new Paging();

        filter.Paging.TakeAll = true;

        return service.PagedListAsync(filter)
            .ContinueWith(o => o.Result.Items.ToList());
    }
}
