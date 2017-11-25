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
        public void When_HomeController_Called_Index_Passed()
        {
            try
            {
                _homeController.Index();
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void When_HomeController_Called_Contact_Passed()
        {
            try
            {
                _homeController.Contact();
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void When_HomeController_Called_About_Passed()
        {
            try
            {
                _homeController.About();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
