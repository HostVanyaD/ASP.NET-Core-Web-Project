namespace HighPaw.Services.Event
{
    using System;
    using System.Collections.Generic;
    using HighPaw.Services.Event.Models;

    public interface IEventService
    {
        public IEnumerable<EventServiceModel> All();

        public int Create(
            string title,
            string description,
            string location,
            DateTime date);

        public void Delete(int id);
    }
}
