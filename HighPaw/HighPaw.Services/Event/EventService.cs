namespace HighPaw.Services.Event
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
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
    }
}
