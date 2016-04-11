using ArtistEventListings.Models;
using GogoKit;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArtistEventListings.DAL
{
    public class VApiListingRepository : IListingRepository
    {
        private readonly IViagogoClient _api;

        public VApiListingRepository(IViagogoClient api)
        {
            _api = api;
        }

        public async Task<PagedResource<Listing>> GetListings(int eventId, int? numberOfTickets, int? page = 1)
        {
            return await _api.Listings.GetByEventAsync(eventId, new ListingRequest { Page = page, PageSize = 16, NumberOfTickets = numberOfTickets });
        }

        public async Task<IReadOnlyList<Listing>> GetListingsByTicketType(int eventId, int? numberOfTickets)
        {
            return await _api.Listings.GetAllByEventAsync(eventId, new ListingRequest { NumberOfTickets = numberOfTickets });
        }
    }
}