namespace HighPaw.Web.Models
{
    public class QueryViewModel : IQueryViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int TotalItems { get; set; }
        public string SearchString { get; set; }
    }
}
