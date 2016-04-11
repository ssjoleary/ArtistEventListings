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

        public async Task<IReadOnlyList<Event>> GetEvents()
        {
            var token = await _api.OAuth2.GetClientAccessTokenAsync(new List<string>());
            await _api.TokenStore.SetTokenAsync(token);

            return await _api.Events.GetAllByCategoryAsync(4243);
        }
    }
}