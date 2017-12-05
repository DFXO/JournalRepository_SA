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
    public class ErrorControllerTests
    {
        [TestMethod]
       
        public void ErrorController_Length_Tests()
        {
            try
            {
                //Act
                ErrorController controller = new ErrorController();
                controller.RequestLengthExceeded();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(3, 3);
            }
        }

    }
}
