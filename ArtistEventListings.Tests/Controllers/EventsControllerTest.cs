using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtistEventListings;
using ArtistEventListings.Controllers;
using System.Threading.Tasks;
using GogoKit;

namespace ArtistEventListings.Tests.Controllers
{
    [TestClass]
    public class EventsControllerTest
    {
        private readonly IViagogoClient _api;

        [TestMethod]
        public void Index()
        {
            // Arrange
            const bool shouldBeComplete = true;
            EventsController controller = new EventsController(_api);

            // Act
            Task<ActionResult> result = controller.Index() as Task<ActionResult>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(shouldBeComplete, result.IsCompleted, "Task Should Have Completed");
        }
    }
}
