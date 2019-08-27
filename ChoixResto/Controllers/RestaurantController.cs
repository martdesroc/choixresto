using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoixResto.Models;

namespace ChoixResto.Controllers
{
    public class RestaurantController : Controller
    {
        private IDal dal;

        public RestaurantController() : this(new Dal())
        {
            // par défaut sur la BD par le navigateur (prod)
        }

        public RestaurantController(IDal dalIoc)
        {
            dal = dalIoc;
            // assigner le Dal hardcodé pour les Tests
        }

        public ActionResult Index()
        {
            List<Resto> listeDesRestaurants = dal.ObtientTousLesRestaurants();
            return View(listeDesRestaurants);
        }

        public ActionResult ModifierRestaurant(int? id)
        {
            if (id.HasValue)
            {
                using (IDal dal = new Dal())
                {
                    Resto restaurant = dal.ObtientTousLesRestaurants().FirstOrDefault(r => r.Id == id.Value);
                    if (restaurant == null)
                        return View("Error");
                    return View(restaurant);
                }
            }
            else
                return View("Error");
        }

        // ASP.NET MVC est capable automatiquement de transformer les valeurs de l’objet Form en paramètres du contrôleur ayant le même nom....
        //[HttpPost]
        //public ActionResult ModifierRestaurant(int? id, string nom, string telephone)
        //{
        //    if (id.HasValue)
        //    {
        //        using (IDal dal = new Dal())
        //        {
        //            // ... et remplace ceci:
        //            //string nouveauNom = Request.Form["Nom"];
        //            //string nouveauTelephone = Request.Form["Telephone"];

        //            dal.ModifierRestaurant(id.Value, nom, telephone);
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //        return View("Error");
        //}


        // Version encore plus simple...
        // ASP.NET MVC reconnaît bien que les propriétés ont les mêmes noms que les champs de formulaire. 
        // Il fait alors une liaison de données entre les deux, permettant ainsi de créer un objet Resto à partir de tous les éléments de la requête

        [HttpPost]
        public ActionResult ModifierRestaurant(Resto resto)
        {
            //if (string.IsNullOrWhiteSpace(resto.Nom))
            //{
            //    ViewBag.MessageErreur = "Le nom du restaurant doit être rempli";
            //    return View(resto);
            //}

            // plus simple ainsi...
            if (!ModelState.IsValid)
            {
                ViewBag.MessageErreur = ModelState["Nom"].Errors[0].ErrorMessage;
                return View(resto);
            }

            // et encore mieux ainsi:
            if (!ModelState.IsValid)
                return View(resto);

            using (IDal dal = new Dal())
            {
                dal.ModifierRestaurant(resto.Id, resto.Nom, resto.Telephone);
                return RedirectToAction("Index");
            }
        }

        public ActionResult CreerRestaurant()
        {
            return View();
        }

        // ASP.NET MVC est capable automatiquement de transformer les valeurs de l’objet Form en paramètres du contrôleur ayant le même nom....
        [HttpPost]
        public ActionResult CreerRestaurant(Resto resto)
        {
            using (IDal dal = new Dal())
            {
                if (dal.RestaurantExiste(resto.Nom))
                {
                    ModelState.AddModelError("Nom", "Ce nom de restaurant existe déjà");
                    return View(resto);
                }
                if (!ModelState.IsValid)
                    return View(resto);
                dal.CreerRestaurant(resto.Nom, resto.Telephone);
                return RedirectToAction("Index");
            }
        }
    }
}