using GogoKit.Models.Response;
using HalKit.Json;
using HalKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistEventListings.Models
{
    public class EventsViewModel
    {
        public EventsViewModel(Event e)
        {
            Id = e.Id;
            Name = e.Name;
            StartDate = e.StartDate;
            Venue = e.Venue;
            MinTicketPrice = e.MinTicketPrice;
            IsCheapestEvent = false;
            ListingsLink = e.ListingsLink;
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        [Embedded("venue")]
        public EmbeddedVenue Venue { get; set; }
        public Money MinTicketPrice { get; set; }
        public bool IsCheapestEvent { get; set; }
        [Rel("event:listings")]
        public Link ListingsLink { get; set; }
    }
}