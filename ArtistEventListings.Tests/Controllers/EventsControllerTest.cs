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
using ArtistEventListings.DAL;

namespace ArtistEventListings.Tests.Controllers
{
    [TestClass]
    public class EventsControllerTest
    {
        private readonly IEventRepository _repository;

        [TestMethod]
        public void Index()
        {
            //// Arrange
            //const bool shouldBeComplete = true;
            //EventsController controller = new EventsController(_repository);

            //// Act
            //Task<ActionResult> result = controller.Index() as Task<ActionResult>;

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(shouldBeComplete, result.IsCompleted, "Task Should Have Completed");
        }
    }
}
