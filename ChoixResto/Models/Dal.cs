using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

namespace ChoixResto.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        public void CreerRestaurant(string nom, string telephone)
        {
            bdd.Restos.Add(new Resto { Nom = nom, Telephone = telephone });
            bdd.SaveChanges();
        }

        public List<Resto> ObtientTousLesRestaurants()
        {
            return bdd.Restos.ToList();
        }

        public void ModifierRestaurant(int id, string nom, string telephone)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Id == id);
            if (restoTrouve != null)
            {
                restoTrouve.Nom = nom;
                restoTrouve.Telephone = telephone;
                bdd.SaveChanges();
            }
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        public bool RestaurantExiste(string nom)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Nom == nom);
            if (restoTrouve != null)
                return true;
            else
                return false;
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            Utilisateur uTrouve = bdd.Utilisateurs.FirstOrDefault(utilisateur => utilisateur.Id == id);
            return uTrouve;
        }

        public Utilisateur ObtenirUtilisateur(string id)
        {
            int n;
            bool isNumeric = int.TryParse(id, out n);

            if (!isNumeric)
                return null;
            else
            {
                int num = Int32.Parse(id);
                Utilisateur uTrouve = bdd.Utilisateurs.FirstOrDefault(utilisateur => utilisateur.Id == num);
                return uTrouve;
            }
        }

        public int AjouterUtilisateur(string nom, string motDePasse)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            Utilisateur utilisateur = new Utilisateur { Prenom = nom, MotDePasse = motDePasseEncode };
            bdd.Utilisateurs.Add(utilisateur);
            bdd.SaveChanges();
            return utilisateur.Id;
        }

        public Utilisateur Authentifier(string nom, string motDePasse)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            Utilisateur uTrouve = bdd.Utilisateurs.FirstOrDefault(user => (user.MotDePasse == motDePasseEncode) && (user.Prenom == nom));
            return uTrouve;
        }

        public bool ADejaVote(int idSondage, string idUser)
        {
            int id;
            bool isNumeric = int.TryParse(idUser, out id);
            if (isNumeric)
            {
                Sondage sondage = bdd.Sondages.First(s => s.Id == idSondage);
                if (sondage.Votes == null)
                    return false;
                return sondage.Votes.Any(v => v.Utilisateur != null && v.Utilisateur.Id == Int32.Parse(idUser));
            }
            return false;
        }

        public int CreerUnSondage()
        {
            try
            {
                Sondage sondage = new Sondage { Date = DateTime.Now, Votes = null };
                bdd.Sondages.Add(sondage);
                bdd.SaveChanges();
                return sondage.Id;
            }
            catch(Exception e)
            {
                return 0;
            }
        }

        public void AjouterVote(int idSondage, int idResto, int idUtilisateur)
        {
            Vote vote = new Vote
            {
                Resto = bdd.Restos.First(r => r.Id == idResto),
                Utilisateur = bdd.Utilisateurs.First(u => u.Id == idUtilisateur)
            };
            Sondage sondage = bdd.Sondages.First(s => s.Id == idSondage);
            if (sondage.Votes == null)
                sondage.Votes = new List<Vote>();
            sondage.Votes.Add(vote);
            bdd.SaveChanges();
        }

        public List<Resultats> ObtenirLesResultats(int idSondage)
        {
            List<Resto> restaurants = ObtientTousLesRestaurants();
            List<Resultats> resultats = new List<Resultats>();

            Sondage sondage = bdd.Sondages.First(s => s.Id == idSondage);
            foreach (IGrouping<int, Vote> grouping in sondage.Votes.GroupBy(v => v.Resto.Id))
            {
                int idRestaurant = grouping.Key;
                Resto resto = restaurants.First(r => r.Id == idRestaurant);
                int nombreDeVotes = grouping.Count();
                resultats.Add(new Resultats { Nom = resto.Nom, Telephone = resto.Telephone, NombreDeVotes = nombreDeVotes });
            }
            return resultats;
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

    }
}