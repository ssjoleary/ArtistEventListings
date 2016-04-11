using ArtistEventListings.Models;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArtistEventListings.DAL
{
    public interface IEventRepository
    {
        Task<IReadOnlyList<Event>> GetEvents();
    }
}
