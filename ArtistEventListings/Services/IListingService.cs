using ArtistEventListings.Models;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistEventListings.Services
{
    public interface IListingService
    {
        Task<ListingsViewModel> GetListings(int eventId, int? numberOfTickets, int? page = 1);
        Task<ListingsViewModel> GetListingsByTicketType(int eventId, int? numberOfTickets, string ticketType, int? page = 1);
    }
}
