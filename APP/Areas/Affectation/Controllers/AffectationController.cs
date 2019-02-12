using APP.Areas.Affectation.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Affectation.Controllers
{
    public class AffectationController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            string name = Server.HtmlEncode(aCookie.Value);
            Utilisateur actif = db.Utilisateur.Single(p => p.Identifiant == name);
            if (actif.IdProfil != 7 && actif.IdProfil != 9)
            {
                return View("AccesInterdit");
            }

            model.categorieMission = db.CategorieMission.ToList();
            model.typeMission = db.TypeMission.ToList();
            model.mission = db.Mission.ToList();
            model.affectation = db.Affectation.ToList();
            model.Vehicule = db.Vehicule.ToList();
            model.conducteur = db.Conducteur.ToList();
            model.affectationP = new DAL.Affectation
            {
                DateAffectation = null
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Ajouter(DAL.Affectation affec, FormCollection collect)
        {
            //string date = collect["DateAffectation"];
            DateTime DateAff = Convert.ToDateTime(collect["DateAffectation"]);
            var date = db.Mission.Find(affec.IdMission).DateDebutMission;
            if(date > affec.DateAffectation)
            {
                ViewBag.SMSError = "Date invalide";
                ViewData["smserror"] = "La date d'affectation doit inférieur à la date de début de mission";

                model.categorieMission = db.CategorieMission.ToList();
                model.typeMission = db.TypeMission.ToList();
                model.mission = db.Mission.ToList();
                model.affectation = db.Affectation.ToList();
                model.Vehicule = db.Vehicule.ToList();
                model.conducteur = db.Conducteur.ToList();
                model.affectationP = affec;

                return View("Index", model);
            }

            if (affec.IdConducteur != null)
            {
                DAL.Affectation affectation = new DAL.Affectation
                {
                    IdMission = affec.IdMission,
                    IdVehicule = affec.IdVehicule,
                    IdConducteur = affec.IdConducteur,
                    DateAffectation = affec.DateAffectation
                };
                db.Affectation.Add(affectation);

                Vehicule veh = db.Vehicule.Find(affec.IdVehicule);
                veh.EnMission = true;
                db.Entry(veh).State = EntityState.Modified;
                Conducteur cond = db.Conducteur.Find(affec.IdConducteur);
                cond.EnMission = true;
                db.Entry(cond).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                Mission miss = db.Mission.Find(affec.IdMission);
                nvelActions.LibelleAction = "Ajout d'une nouvelle affectaion pour la mission: "+ miss.LibelleMission;
                db.Action.Add(nvelActions);

                db.SaveChanges();
            }
            else
            {
                DAL.Affectation affectation = new DAL.Affectation
                {
                    IdMission = affec.IdMission,
                    IdVehicule = affec.IdVehicule,
                    IdConducteur =  null,
                    DateAffectation = affec.DateAffectation
                };
                db.Affectation.Add(affectation);

                Vehicule veh = db.Vehicule.Find(affec.IdVehicule);
                veh.EnMission = true;
                db.Entry(veh).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                Mission miss = db.Mission.Find(affec.IdMission);
                nvelActions.LibelleAction = "Ajout d'une nouvelle affectaion pour la mission: " + miss.LibelleMission;
                db.Action.Add(nvelActions);

                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(DAL.Affectation tmp1)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tmp1).State = EntityState.Modified;
                //db.Affectation.AddOrUpdate(tmp1);

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                Mission miss = db.Mission.Find(tmp1.IdMission);
                nvelActions.LibelleAction = "Modification de l'affectaion pour la mission: " + miss.LibelleMission;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        // GET: Mission/Missions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Mission mission = await db.Mission.FindAsync(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // POST: Mission/Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DAL.Affectation affectation = await db.Affectation.FindAsync(id);

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            Mission miss = db.Mission.Find(affectation.IdMission);
            nvelActions.LibelleAction = "Suppression de l'affectaion pour la mission: " + miss.LibelleMission;
            db.Action.Add(nvelActions);

            db.Affectation.Remove(affectation);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ExportListA()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Affectation/CRAffect.rpt")));
            var e = db.Affectation.Select(p => new
            {
                Id = p.IdAffectation == 0 ? 0 : p.IdAffectation,
                Intitule = p.Mission.LibelleMission == null ? " " : p.Mission.LibelleMission,
                Vehicule = p.Vehicule.MarqueVehicule.LibelleMarque +" "+ p.Vehicule.NomVehicule +" "+ p.Vehicule.Immatriculation == null ? " " : p.Vehicule.MarqueVehicule.LibelleMarque + " " + p.Vehicule.NomVehicule + " " + p.Vehicule.Immatriculation,
                Conducteur = p.Conducteur.NomConducteur +" "+ p.Conducteur.PrenomConducteur == null ? "-------" : p.Conducteur.NomConducteur +" "+ p.Conducteur.PrenomConducteur,
                //Date = (p.DateAffectation).ToString() == null ? "" : p.DateAffectation.ToString(),
                Date = p.DateAffectation ?? new DateTime(),
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
        }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des Affectations par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeAffectation.pdf");


        }
    }
}