using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Models
{
    public class UserViewModel
    {
        public List<Utilisateur> Users { get; set; }
        public Utilisateur User { get; set; }
        public List<Connexion> Connexions { get; set; }
        public bool DejaConnecte(Utilisateur x)
        {

            return Connexions.Any(p => p.Utilisateur.Equals(x) && p.FinConnexion == null);

        }
    }
}