namespace HighPaw.Services.Event.Models
{
    public class EventServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Description { get; set; }

        public string Location { get; set; }
    }
}
