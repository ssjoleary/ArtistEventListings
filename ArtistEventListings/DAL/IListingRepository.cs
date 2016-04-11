using ArtistEventListings.Models;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistEventListings.DAL
{
    public interface IListingRepository
    {
        Task<PagedResource<Listing>> GetListings(int eventId, int? numberOfTickets, int? page);
        Task<IReadOnlyList<Listing>> GetListingsByTicketType(int eventId, int? numberOfTickets);
    }
}
