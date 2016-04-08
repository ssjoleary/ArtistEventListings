using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistEventListings.Models
{
    public class EventsViewModel
    {
        public IReadOnlyList<Event> Events { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public int? CurrentPageNum { get; set; }
        public int? TotalPages { get; set; }
    }
}