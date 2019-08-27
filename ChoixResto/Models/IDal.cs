using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

namespace ChoixResto.Models
{
    public interface IDal : IDisposable
    {
        void CreerRestaurant(string nom, string telephone);
        void ModifierRestaurant(int id, string nom, string telephone);
        List<Resto> ObtientTousLesRestaurants();
        bool RestaurantExiste(string nom);
        Utilisateur ObtenirUtilisateur(int id);
        Utilisateur ObtenirUtilisateur(string id);
        int AjouterUtilisateur(string prenom, string motdepasse);
        Utilisateur Authentifier(string prenom, string motdepasse);
        bool ADejaVote(int idSondage, string idStr);
        int CreerUnSondage();
        void AjouterVote(int idSondage, int idResto, int idUtilisateur);
        List<Resultats> ObtenirLesResultats(int idSondage);
    }
}