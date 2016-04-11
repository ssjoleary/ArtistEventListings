using ArtistEventListings.DAL;
using ArtistEventListings.Models;
using ArtistEventListings.Services;
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
        private readonly IListingService _listingService;

        public ListingsController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [Route("", Name = "Listings")]
        public ActionResult Index()
        {
            return View();
        }
                
        public async Task<JsonResult> GetListings(int eventId, int? numberOfTickets, int? page = 1)
        {
            return Json(await _listingService.GetListings(eventId, numberOfTickets, page), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetListingsByTicketType(int eventId, int? numberOfTickets, string ticketType, int? page = 1)
        {
            return Json(await _listingService.GetListingsByTicketType(eventId, numberOfTickets, ticketType, page), JsonRequestBehavior.AllowGet);
        }
    }
}