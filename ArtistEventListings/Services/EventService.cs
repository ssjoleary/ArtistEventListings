using ArtistEventListings.DAL;
using ArtistEventListings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArtistEventListings.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<EventModel>> GetEventsWithCheapestHighlighted()
        {
            var allEvents = await _repository.GetEvents();
            List<EventModel> eventModels = new List<EventModel>();
            foreach (var eventItem in allEvents)
            {
                eventModels.Add(new EventModel(eventItem));
            }
            var eventsByCountry = eventModels.GroupBy(e => e.Venue.Country.Code);

            foreach (var eventCountryGroup in eventsByCountry)
            {
                if (eventCountryGroup.Count() > 1)
                {
                    var minTicketPrice = eventCountryGroup.Where(e => e.MinTicketPrice != null).Min(e => e.MinTicketPrice.Amount);
                    EventModel cheapestEvent = eventCountryGroup.Where(e => e.MinTicketPrice != null && e.MinTicketPrice.Amount == minTicketPrice).Single();
                    cheapestEvent.IsCheapestEvent = true;
                }
            }
            return eventsByCountry.SelectMany(grp => grp.ToList()).OrderBy(e => e.StartDate).ToList();
        }
    }
}