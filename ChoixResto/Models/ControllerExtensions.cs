using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Models
{
    public static class ControllerExtensions
    {
        public static void ValideLeModele<T>(this Controller controller, T modele)
        {
            controller.ModelState.Clear();

            ValidationContext context = new ValidationContext(modele, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(modele, context, validationResults, true);

            foreach (ValidationResult result in validationResults)
            {
                foreach (string memberName in result.MemberNames)
                {
                    controller.ModelState.AddModelError(memberName, result.ErrorMessage);
                }
            }
        }
    }
}