using ArtistEventListings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistEventListings.DAL
{
    public interface IListingRepository
    {
        Task<ListingsViewModel> GetListings(int eventId, int? numberOfTickets, int? page = 1);
    }
}
