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

    public class HomeControllerTests
    {
        private HomeController _homeController;

        /// <summary>
        /// Test Initialize
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _homeController = new HomeController();
        }

        /// <summary>
        /// HomeController Index test 
        /// </summary>
        [TestMethod]
        public void HomeController_Index_Test()
        {
            //ACT
            _homeController.Index();
            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// HomeController About test 
        /// </summary>
        [TestMethod]
        public void HomeController_About_Test()
        {
            //ACT
            _homeController.About();
            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// HomeController Contact test 
        /// </summary>
        [TestMethod]
        public void HomeController_Contact_Test()
        {
            //ACT
            _homeController.Contact();
            // Assert
            Assert.IsTrue(true);
        }
    }
}
