using ArtistEventListings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistEventListings.Services
{
    public interface IEventService
    {
        Task<List<EventsViewModel>> GetEventsWithCheapestHighlighted();
    }
}
