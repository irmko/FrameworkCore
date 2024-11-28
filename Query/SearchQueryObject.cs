namespace SkyNET.Framework.Common.Query; 
public class SearchQueryObject : QueryObject {
    public string Search { get; set; }
}

public class SearchQueryObject<TFilter> : SearchQueryObject {
    public TFilter Filter { get; set; }
}
