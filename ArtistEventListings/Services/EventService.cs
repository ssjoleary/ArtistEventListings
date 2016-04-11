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
        public async Task<List<EventsViewModel>> GetEventsWithCheapestHighlighted()
        {
            var allEvents = await _repository.GetEvents();
            List<EventsViewModel> eventModels = new List<EventsViewModel>();
            foreach (var eventItem in allEvents)
            {
                eventModels.Add(new EventsViewModel(eventItem));
            }
            var eventsByCountry = eventModels.GroupBy(e => e.Venue.Country.Code);

            foreach (var eventCountryGroup in eventsByCountry)
            {
                if (eventCountryGroup.Count() > 1)
                {
                    var minTicketPrice = eventCountryGroup.Where(e => e.MinTicketPrice != null).Min(e => e.MinTicketPrice.Amount);
                    EventsViewModel cheapestEvent = eventCountryGroup.Where(e => e.MinTicketPrice != null && e.MinTicketPrice.Amount == minTicketPrice).Single();
                    cheapestEvent.IsCheapestEvent = true;
                }
            }
            return eventsByCountry.SelectMany(grp => grp.ToList()).OrderBy(e => e.StartDate).ToList();
        }
    }
}