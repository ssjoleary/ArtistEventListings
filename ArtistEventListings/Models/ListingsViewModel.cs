using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistEventListings.Models
{
    public class ListingsViewModel
    {
        public int EventID { get; set; }
        public int? NumberOfTickets { get; set; }
        public IReadOnlyCollection<Listing> Listings { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public int? CurrentPageNum { get; set; }
        public int? TotalPages { get; set; }
    }
}