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
    class RestaurantControllerTests
    {
        [TestMethod]
        public void RestaurantController_Index_LeControleurEstOk()
        {
            using (IDal dal = new DalEnDur())
            {
                RestaurantController controller = new RestaurantController(dal);

                ViewResult resultat = (ViewResult)controller.Index();

                List<Resto> modele = (List<Resto>)resultat.Model;
                Assert.AreEqual("Resto pinambour", modele[0].Nom);
            }
        }

        /// <summary>
        /// Qu’est-ce que mon test cherche à vérifier ? Que le binding de modèle et que la validation fonctionne ?
        // Ceci est inutile à tester car le binding de modèle fonctionne ! C’est un composant du framework ASP.NET MVC, alors il fonctionne.
        // Il y a sûrement des tests unitaires écrits par les ingénieurs de Microsoft qui valident son fonctionnement.
        // Non, notre test doit vérifier que lorsque le modèle est invalide, alors la vue renvoyée est bien celle par défaut.C’est exactement ce que fait ce test.
        /// </summary>
        [TestMethod]
        public void RestaurantController_ModifierRestaurantAvecRestoInvalide_RenvoiVueParDefaut()
        {
            using (IDal dal = new DalEnDur())
            {
                RestaurantController controller = new RestaurantController(dal);
                controller.ModelState.AddModelError("Nom", "Le nom du restaurant doit être saisi");

                ViewResult resultat = (ViewResult)controller.ModifierRestaurant(new Resto { Id = 1, Nom = null, Telephone = "0102030405" });

                Assert.AreEqual(string.Empty, resultat.ViewName);
                Assert.IsFalse(resultat.ViewData.ModelState.IsValid);
            }
        }

        [TestMethod]
        public void RestaurantController_ModifierRestaurantAvecRestoInvalideEtBindingDeModele_RenvoiVueParDefaut()
        {
            RestaurantController controller = new RestaurantController(new DalEnDur());
            Resto resto = new Resto { Id = 1, Nom = null, Telephone = "0102030405" };
            controller.ValideLeModele(resto);

            ViewResult resultat = (ViewResult)controller.ModifierRestaurant(resto);

            Assert.AreEqual(string.Empty, resultat.ViewName);
            Assert.IsFalse(resultat.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void RestaurantController_ModifierRestaurantAvecRestoValide_CreerRestaurantEtRenvoiVueIndex()
        {
            using (IDal dal = new DalEnDur())
            {
                RestaurantController controller = new RestaurantController(dal);
                Resto resto = new Resto { Id = 1, Nom = "Resto mate", Telephone = "0102030405" };
                controller.ValideLeModele(resto);

                RedirectToRouteResult resultat = (RedirectToRouteResult)controller.ModifierRestaurant(resto);

                Assert.AreEqual("Index", resultat.RouteValues["action"]);
                Resto restoTrouve = dal.ObtientTousLesRestaurants().First();
                Assert.AreEqual("Resto mate", restoTrouve.Nom);
            }
        }
    }
}
