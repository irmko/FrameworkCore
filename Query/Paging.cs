namespace SkyNET.Framework.Common.Query; 
public class Paging : IPaging {
    public Paging(int page, int pageSize) {
        Page = page;
        PageSize = pageSize;
    }

    public Paging(int page)
        : this(page, 20) {
    }

    public Paging()
        : this(1, 20) {
    }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int Skip => PageSize * (Page - 1);

    public int Take => PageSize;

    public bool? TakeAll { get; set; } = true;
}
