namespace HighPaw.Services
{
    public interface IQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }
    }
}
