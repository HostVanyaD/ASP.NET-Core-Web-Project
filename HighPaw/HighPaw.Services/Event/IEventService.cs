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

        public void Edit(EventServiceModel model);

        public EventServiceModel GetById(int? id);

        public bool DoesExist(int id);

        public void Delete(int id);
    }
}
