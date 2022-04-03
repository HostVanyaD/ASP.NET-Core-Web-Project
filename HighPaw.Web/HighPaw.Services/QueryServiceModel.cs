namespace HighPaw.Services
{
    using System.Collections.Generic;

    public class QueryServiceModel<T> : IQueryServiceModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();

        public virtual IEnumerable<string> Filters { get; set; }
    }
}
