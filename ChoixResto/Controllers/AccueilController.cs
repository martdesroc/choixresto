using ChoixResto.Models;
using ChoixResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Accueil2
        public ActionResult Index()
        {
            // Affichage de la liste 
            //AccueilViewModel vm = new AccueilViewModel
            //{
            //    Message = "Bonjour depuis le <span style=\"color:red\">contrôleur</span>",
            //    Date = DateTime.Now,
            //    ListeDesRestos = new List<Resto>
            //    {
            //        new Resto { Nom = "Resto pinambour", Telephone = "1234" },
            //        new Resto { Nom = "Resto tologie", Telephone = "1234" },
            //        new Resto { Nom = "Resto ride", Telephone = "5678" },
            //        new Resto { Nom = "Resto toro", Telephone = "555" },
            //    }
            //};
            //return View(vm);

            //List<Models.Resto> listeDesRestos = new List<Resto>
            //{
            //    new Resto { Id = 1, Nom = "Resto pinambour", Telephone = "1234" },
            //    new Resto { Id = 2, Nom = "Resto tologie", Telephone = "1234" },
            //    new Resto { Id = 5, Nom = "Resto ride", Telephone = "5678" },
            //    new Resto { Id = 9, Nom = "Resto toro", Telephone = "555" },
            //};

            //ViewBag.ListeDesRestos = new SelectList(listeDesRestos, "Id", "Nom", 5);

            return View();
        }

        [ChildActionOnly]
        public ActionResult AfficheListeRestaurant()
        {
            List<Models.Resto> listeDesRestos = new List<Resto>
            {
                new Resto { Id = 1, Nom = "Resto pinambour", Telephone = "1234" },
                new Resto { Id = 2, Nom = "Resto tologie", Telephone = "1234" },
                new Resto { Id = 5, Nom = "Resto ride", Telephone = "5678" },
                new Resto { Id = 9, Nom = "Resto toro", Telephone = "555" },
            };
            return PartialView(listeDesRestos);
        }     

    }
}