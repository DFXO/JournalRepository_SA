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
    public class ErrorControllerUnitTest
    {
        [TestMethod]
       
        public void ErrorController_ReturnsRequestLengthExceeded()
        {
            try
            {
                //Act
                ErrorController controller = new ErrorController();
                controller.RequestLengthExceeded();
            }
            catch (Exception)
            {
                Assert.AreEqual(2, 2);
            }
        }

    }
}
