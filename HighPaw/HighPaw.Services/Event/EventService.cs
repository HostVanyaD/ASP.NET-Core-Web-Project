namespace HighPaw.Services.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Services.Event.Models;

    public class EventService : IEventService
    {
        private readonly HighPawDbContext data;
        private readonly IConfigurationProvider mapper;

        public EventService(
            HighPawDbContext data, 
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<EventServiceModel> All()
            => this.data
                .Events
                .Where(e => e.Date > DateTime.UtcNow)
                .ProjectTo<EventServiceModel>(this.mapper)
                .ToList();

        public int Create(
            string title, 
            string description, 
            string location, 
            DateTime date)
        {
            var eventData = new Event
            {
                Title = title,
                Description = description,
                Location = location,
                Date = date
            };

            this.data.Events.Add(eventData);
            this.data.SaveChanges();

            return eventData.Id;
        }

        public void Edit(EventServiceModel model)
        {
            var eventToEdit = this.data
                .Events
                .FirstOrDefault(e => e.Id == model.Id);

            eventToEdit.Title = model.Title;
            eventToEdit.Description = model.Description;
            eventToEdit.Location = model.Location;
            eventToEdit.Date = DateTime.Parse(model.Date);

            this.data.Update(eventToEdit);
            this.data.SaveChanges();
        }

        public EventServiceModel GetById(int? id)
            => this.data
                .Events
                .Where(e => e.Id == id)
                .ProjectTo<EventServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool DoesExist(int id)
            => this.data
                .Events
                .Any(e => e.Id == id);

        public void Delete(int id)
        {
            var eventToDelete = this.data
                .Events
                .Find(id);

            this.data
                .Events
                .Remove(eventToDelete);

            this.data.SaveChanges();
        }
    }
}
