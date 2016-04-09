using ArtistEventListings.Models;
using GogoKit;
using GogoKit.Exceptions;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArtistEventListings.DAL
{
    public class VApiEventRepository : IEventRepository
    {
        private readonly IViagogoClient _api;
        
        public VApiEventRepository(IViagogoClient api)
        {
            _api = api;
        }

        public async Task<EventsViewModel> GetEvents(int? page = 1)
        {
            var token = await _api.OAuth2.GetClientAccessTokenAsync(new List<string>());
            await _api.TokenStore.SetTokenAsync(token);

            EventsViewModel eventsViewModel;
            var category = await _api.Categories.GetAsync(4243, new CategoryRequest { Page = page, PageSize = 15, OnlyWithTickets = true });
            var events = await _api.Hypermedia.GetAsync<PagedResource<Event>>(category.EventsLink);
            eventsViewModel = new EventsViewModel
            {
                Events = events.Items,
                CurrentPageNum = events.Page.Value,
                NextPage = events.NextLink != null ? events.Page.Value + 1 : (int?)null,
                PreviousPage = events.PrevLink != null ? events.Page.Value - 1 : (int?)null,
                TotalPages = (int)Math.Ceiling((double)events.TotalItems.Value / events.PageSize.Value)
            };

            return eventsViewModel;
        }
    }
}