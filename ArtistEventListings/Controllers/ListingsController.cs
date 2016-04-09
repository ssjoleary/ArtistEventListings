using ArtistEventListings.DAL;
using ArtistEventListings.Models;
using GogoKit;
using GogoKit.Exceptions;
using GogoKit.Models.Response;
using HalKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArtistEventListings.Controllers
{
    public class ListingsController : Controller
    {
        private readonly IListingRepository _repository;

        public ListingsController(IListingRepository repository)
        {
            _repository = repository;
        }
        
        [Route("", Name = "Listings")]
        public async Task<ActionResult> Index(int eventId, int? numberOfTickets, int? page = 1)
        {
            return View(await _repository.GetListings(eventId, numberOfTickets, page));
        }
    }
}