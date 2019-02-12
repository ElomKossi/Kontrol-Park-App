using APP.Areas.Administraton.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Administraton.Controllers
{
    public class JournalController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AdminViewModel model = new AdminViewModel();

        public ActionResult Index()
        {
            model.connexion = db.Connexion.ToList();
            model.utilisateur = db.Utilisateur.ToList();
            model.profil = db.Profil.ToList();
            model.action = db.Action.ToList();

            HttpCookie aCookie = Request.Cookies["ident"];
            string name = Server.HtmlEncode(aCookie.Value);
            Utilisateur actif = db.Utilisateur.Single(p => p.Identifiant == name);
            if (actif.IdProfil != 6 && actif.IdProfil != 9)
            {
                return View("AccesInterdit");
            }
            
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Consultation du journal de connexion";
            db.Action.Add(nvelActions);
            db.SaveChanges();

            return View(model);
        }
    }
}