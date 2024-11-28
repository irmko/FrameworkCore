namespace SkyNET.Framework.Common.Paged; 
public class PagedResult<T> {
    public PagedResult() {
    }

    public PagedResult(IEnumerable<T> items, int total) {
        Items = items;
        Total = total;
    }

    public IEnumerable<T> Items { get; set; }

    public int Total { get; set; }

    public static PagedResult<T> Empty() {
        return new PagedResult<T>(Enumerable.Empty<T>(), 0);
    }
}
