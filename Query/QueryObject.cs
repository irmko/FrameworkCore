namespace SkyNET.Framework.Common.Query {
    public class QueryObject<TFilter> : QueryObject
        where TFilter : new() {

        public QueryObject() {
            Filter = new TFilter();
        }

        public TFilter Filter { get; set; }
    }

    public class QueryObject : IQueryObject {
        public QueryObject() {
            Paging = new Paging();
            Order = new List<Order>();
        }

        public Paging Paging { get; set; }

        public IEnumerable<Order> Order { get; set; }

        public string OrderBy => String.Join(",", Order);

        public string[] OptionalColumns { get; set; }
    }

    public interface IQueryObject {
        public Paging Paging { get; set; }

        public IEnumerable<Order> Order { get; set; }

        public string OrderBy { get; }

        public string[] OptionalColumns { get; set; }
    }
}
