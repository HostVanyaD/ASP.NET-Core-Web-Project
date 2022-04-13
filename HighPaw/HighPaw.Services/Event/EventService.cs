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
    }
}
