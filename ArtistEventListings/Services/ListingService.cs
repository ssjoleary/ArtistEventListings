using ArtistEventListings.DAL;
using ArtistEventListings.Models;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArtistEventListings.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _repository;

        public ListingService(IListingRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<ListingsViewModel> GetListings(int eventId, int? numberOfTickets, int? page = 1)
        {
            ListingsViewModel listingsViewModel;
            var listings = await _repository.GetListings(eventId, numberOfTickets, page);            
            listingsViewModel = new ListingsViewModel
            {
                EventID = eventId,
                NumberOfTickets = numberOfTickets,
                Listings = listings.Items,
                CurrentPageNum = listings.Page.Value,
                NextPage = listings.NextLink != null ? listings.Page.Value + 1 : (int?)null,
                PreviousPage = listings.PrevLink != null ? listings.Page.Value - 1 : (int?)null,
                TotalListings = listings.TotalItems.Value
            };

            return listingsViewModel;
        }

        public async Task<ListingsViewModel> GetListingsByTicketType(int eventId, int? numberOfTickets, string ticketType, int? page = 1)
        {
            int? startingIndex = (16 * (page - 1));

            var listings = await _repository.GetListingsByTicketType(eventId, numberOfTickets);
            IReadOnlyList<Listing> listingsByTicketType = listings.Where(l => l.TicketType.Type == ticketType).ToList();

            ListingsViewModel listingsViewModel = new ListingsViewModel
            {
                EventID = eventId,
                NumberOfTickets = numberOfTickets,
                Listings = listingsByTicketType.Skip((int)startingIndex).Take(16).ToList(),
                CurrentPageNum = page,
                NextPage = (int?)null,
                PreviousPage = (int?)null,
                TotalListings = listingsByTicketType.Count
            };

            return listingsViewModel;
        }
    }
}