using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ChoixResto.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required]
        public string Prenom { get; set; }
        public string MotDePasse { get; set; }
    }
}