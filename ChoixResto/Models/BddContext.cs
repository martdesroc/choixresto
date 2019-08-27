using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChoixResto.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Sondage> Sondages { get; set; }
        public DbSet<Resto> Restos { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Vote> Votes { get; set; }

    }

    //cette classe BddContext  doit être modifiée pour ajouter un accès direct aux utilisateurs.
}