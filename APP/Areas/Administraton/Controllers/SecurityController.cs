using APP.Areas.Administraton.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Administraton.Controllers
{
    public class SecurityController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AdminViewModel model = new AdminViewModel();

        // GET: Administraton/Security
        public ActionResult Index()
        {
            model.conducteur = db.Conducteur.ToList();
            model.mecanicien = db.Mecanicien.ToList();

            model.utilisateur = db.Utilisateur.ToList();
            model.profil = db.Profil.ToList();
            model.droit = db.Droit.ToList();
            model.avoirDroit = db.AvoirDroit.ToList();
            model.connexion = db.Connexion.ToList();
            model.action = db.Action.ToList();

            model.mission = db.Mission.ToList();

            model.Vehicule = db.Vehicule.ToList();

            HttpCookie aCookie = Request.Cookies["ident"];
            string name = Server.HtmlEncode(aCookie.Value);
            Utilisateur actif = db.Utilisateur.Single(p => p.Identifiant == name);
            if (actif.IdProfil != 6 && actif.IdProfil != 9)
            {
                return View("AccesInterdit");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AjouterProfil(Profil nouveauProfil, List<int> idDroits)
        {
            if (!model.Pofilexiste(nouveauProfil))
            {
                nouveauProfil.Actif = true;
                nouveauProfil.ActifProfil = true;
                nouveauProfil.DateDesactivation = DateTime.Now;
                db.Profil.Add(nouveauProfil);
                db.SaveChanges();
                AvoirDroit avDroit = new AvoirDroit();
                foreach (var i in idDroits)
                {
                    var droit = db.Droit.Single(p => p.IdDroit == i);
                    avDroit.IdDroit = droit.IdDroit;
                    avDroit.IdProfil = nouveauProfil.IdProfil;
                    avDroit.Supprimer = false;
                    avDroit.DateSupprimer = DateTime.Now;
                    db.AvoirDroit.Add(avDroit);
                    db.SaveChanges();
                }

                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Ajout du Profil " + nouveauProfil.LibelleProfil;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditProfil()
        {
            throw new NotImplementedException();
        }

        public ActionResult EtatProfil(int id)
        {
            Profil pro = db.Profil.Find(id);

            if (pro.ActifProfil == true)
            {
                pro.ActifProfil = false;
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();

                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Désactivation du Profil " + pro.LibelleProfil;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);
                db.SaveChanges();
            }
            else
            {
                pro.ActifProfil = true;
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();

                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Activation du Profil " + pro.LibelleProfil;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AjouterDroit(Droit droit)
        {
            if (!model.Droitexiste(droit))
            {
                droit.ActifDroit = true;
                droit.DateSupprimer = DateTime.Now;
                db.Droit.Add(droit);
                db.SaveChanges();
                droit.ActifDroit = false;
                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Ajout du droit " + droit.LibelleDroit;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditDroit()
        {
            throw new NotImplementedException();
        }

        public ActionResult EtatDroit(int id)
        {
            Droit drt = db.Droit.Find(id);

            if (drt.ActifDroit == true)
            {
                drt.ActifDroit = false;
                db.Entry(drt).State = EntityState.Modified;
                db.SaveChanges();

                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Désactivation du droit " + drt.LibelleDroit;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);
                db.SaveChanges();
            }
            else
            {
                drt.ActifDroit = true;
                db.Entry(drt).State = EntityState.Modified;
                db.SaveChanges();

                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Activation du droit " + drt.LibelleDroit;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}