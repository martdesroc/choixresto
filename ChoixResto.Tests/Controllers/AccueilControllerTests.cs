using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ChoixResto.Controllers;
using ChoixResto.Models;

namespace ChoixResto.Tests.Controllers
{
    [TestClass]
    class AccueilControllerTests
    {
        [TestMethod]
        public void AccueilController_Index_RenvoiVueParDefaut()
        {
            AccueilController controller = new AccueilController();

            ViewResult resultat = (ViewResult)controller.Index();

            Assert.AreEqual(string.Empty, resultat.ViewName);
        }

        
    }
}
