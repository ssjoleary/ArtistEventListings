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
        private readonly IViagogoClient _api;

        public ListingsController(IViagogoClient api)
        {
            _api = api;
        }
        
        [Route("", Name = "Listings")]
        public async Task<ActionResult> Index(int eventId, int? numberOfTickets, int? page = 1)
        {
            ListingsViewModel listingsViewModel;
            try
            {
                var listings = await _api.Listings.GetByEventAsync(eventId, new GogoKit.Models.Request.ListingRequest { Page = page, PageSize = 16, NumberOfTickets = numberOfTickets });

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
            }
            catch (ResourceNotFoundException)
            {
                return HttpNotFound();
            }

            return View(listingsViewModel);
        }
    }
}