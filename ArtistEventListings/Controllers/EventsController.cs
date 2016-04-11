using ArtistEventListings.DAL;
using ArtistEventListings.Models;
using ArtistEventListings.Services;
using GogoKit;
using GogoKit.Enumerations;
using GogoKit.Exceptions;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using HalKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArtistEventListings.Controllers
{
    public class EventsController : Controller
    {
        private IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [Route("", Name = "Events")]
        public async Task<JsonResult> GetEventsWithCheapestHighlighted()
        {
            return Json(await _eventService.GetEventsWithCheapestHighlighted(), JsonRequestBehavior.AllowGet);
        }        
    }
}