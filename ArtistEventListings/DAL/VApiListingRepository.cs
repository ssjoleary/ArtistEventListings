using ArtistEventListings.Models;
using GogoKit;
using GogoKit.Models.Request;
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

        public async Task<ListingsViewModel> GetListings(int eventId, int? numberOfTickets, int? page = 1)
        {
            ListingsViewModel listingsViewModel;
            var listings = await _api.Listings.GetByEventAsync(eventId, new ListingRequest { Page = page, PageSize = 16, NumberOfTickets = numberOfTickets });

            listingsViewModel = new ListingsViewModel
            {
                EventID = eventId,
                NumberOfTickets = numberOfTickets,
                Listings = listings.Items,
                CurrentPageNum = listings.Page.Value,
                NextPage = listings.NextLink != null ? listings.Page.Value + 1 : (int?)null,
                PreviousPage = listings.PrevLink != null ? listings.Page.Value - 1 : (int?)null,
                TotalPages = (int)Math.Ceiling((double)listings.TotalItems.Value / listings.PageSize.Value)
            };

            return listingsViewModel;
        }
    }
}