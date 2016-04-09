using ArtistEventListings.DAL;
using ArtistEventListings.Models;
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
        private IEventRepository _repository;

        public EventsController(IEventRepository repository)
        {
            _repository = repository;
        }

        [Route("", Name = "Events")]
        public async Task<ActionResult> Index(int? page = 1)
        {
            return View(await _repository.GetEvents());
        }        
    }
}