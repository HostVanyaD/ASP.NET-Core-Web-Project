namespace HighPaw.Services.Event
{
    using System.Collections.Generic;
    using HighPaw.Services.Event.Models;

    public interface IEventService
    {
        public IEnumerable<EventServiceModel> All();
    }
}
