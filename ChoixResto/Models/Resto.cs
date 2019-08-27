using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChoixResto.Models
{
    [Table("Restos")]
    public class Resto : IValidatableObject
    //public class Resto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        //Methode ValidationAttribute passe par ici en premier
        [Helpers.AuMoinsUnDesDeux(Parametre1 = "Telephone", Parametre2 = "Email", ErrorMessage = "Class msg: Vous devez saisir au moins un moyen de contacter le restaurant")]
        public string Telephone { get; set; }
        [Helpers.AuMoinsUnDesDeux(Parametre1 = "Telephone", Parametre2 = "Email", ErrorMessage = "Class msg: Vous devez saisir au moins un moyen de contacter le restaurant")]
        public string Email { get; set; }

        //Si nous devons faire ce test dans l’action qui permet de créer, puis dans l’action qui permet de modifier, et peut-être ailleurs, nous allons dupliquer plein de code…
        //Il y a plusieurs solutions pour résoudre ce problème.
        //La première est de faire en sorte que le modèle porte sa propre validation, validation qui doit être compatible avec le mécanisme de validation du framework ASP.NET MVC.
        //Cette solution c’est l’implémentation de l’interface IValidatableObject
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Methode Validate msg passe ici en deuxième
            if (string.IsNullOrWhiteSpace(Telephone) && string.IsNullOrWhiteSpace(Email))
                yield return new ValidationResult("Validate msg: Vous devez saisir au moins un moyen de contacter le restaurant", new[] { "Telephone", "Email" });
            // etc.
        }
    }
}