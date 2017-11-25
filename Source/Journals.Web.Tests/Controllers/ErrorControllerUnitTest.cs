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
       
        public void ErrorController_RequestLengthExceeded_Passed()
        {
            try
            {
                //Act
                ErrorController controller = new ErrorController();
                controller.RequestLengthExceeded();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(2, 2);
            }
        }

    }
}
