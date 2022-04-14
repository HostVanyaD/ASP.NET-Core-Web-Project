namespace HighPaw.Services
{
    using System.Collections.Generic;

    public class QueryServiceModel<T> : IQueryServiceModel
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
