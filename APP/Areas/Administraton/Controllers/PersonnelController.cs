using APP.Areas.Administraton.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Administraton.Controllers
{
    public class PersonnelController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AdminViewModel model = new AdminViewModel();

        // GET: Administraton/Personnel
        public ActionResult Index()
        {
            model.conducteur = db.Conducteur.ToList();
            model.mecanicien = db.Mecanicien.ToList();

            return View(model);
        }

        public ActionResult Ajouter(Conducteur tmp, int choix=0)
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            if (choix == 1)
            {
                var tech = new Mecanicien
                {
                    NumCNIMecanicien = tmp.NumCNIConducteur,
                    NomMecanicien = tmp.NomConducteur,
                    PrenomMecanicien = tmp.PrenomConducteur,
                    DateNaissanceMecanicien = tmp.DateNaissanceConducteur,
                    LieuxNaissanceMecanicien = tmp.LieuxNaissanceConducteur,
                    SexeMecanicien = tmp.SexeConducteur,
                    AdresseMecanicien = tmp.AdresseConducteur,
                    EmailMecanicien = tmp.EmailConducteur,
                    TelMecanicien = tmp.TelConducteur,
                    DateEmbaucheMecanicien = tmp.DateEmbaucheConducteur,
                    EnActivite = true
                };
                db.Mecanicien.Add(tech);

                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout du mécanicien " + tmp.NomConducteur + " " + tmp.PrenomConducteur;
                db.Action.Add(nvelActions);

                db.SaveChanges();
            }
            else
            {
                Conducteur conducteur = new Conducteur
                {
                    NumCNIConducteur = tmp.NumCNIConducteur,
                    NomConducteur = tmp.NomConducteur,
                    PrenomConducteur = tmp.PrenomConducteur,
                    DateNaissanceConducteur = tmp.DateNaissanceConducteur,
                    LieuxNaissanceConducteur = tmp.LieuxNaissanceConducteur,
                    SexeConducteur = tmp.SexeConducteur,
                    AdresseConducteur = tmp.AdresseConducteur,
                    EmailConducteur = tmp.EmailConducteur,
                    TelConducteur = tmp.TelConducteur,
                    DateEmbaucheConducteur = tmp.DateEmbaucheConducteur,
                    EnActivite = true,
                    EnMission = false,
                };
                db.Conducteur.Add(conducteur);

                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout du conducteur " + tmp.NomConducteur + " " + tmp.PrenomConducteur;
                db.Action.Add(nvelActions);

                db.SaveChanges();
            }

            return RedirectToAction("index");
        }
    }
}