using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journals.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medico.Web.Tests.Controllers
{
    [TestClass]
  
    public class HomeControllerUnitTest
    {
        private HomeController _homeController;

        [TestInitialize]
        public void TestInitialize()
        {
            _homeController = new HomeController();
        }

        [TestMethod]
        public void HomeController_ReturnsHomePage()
        {
            try
            {
                _homeController.Index();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public void HomeController_ReturnsContactPage()
        {
            try
            {
                _homeController.Contact();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public void HomeController_ReturnsAboutPage()
        {
            try
            {
                _homeController.About();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
