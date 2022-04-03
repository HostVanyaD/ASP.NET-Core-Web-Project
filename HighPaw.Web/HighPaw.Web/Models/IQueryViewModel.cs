namespace HighPaw.Web.Models
{
    public interface IQueryViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public string SearchString { get; set; }
    }
}
