using APP.Areas.Affectation.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Affectation.Controllers
{
    public class MissionsController : Controller
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
            model.conducteur = db.Conducteur.ToList();
            model.mission = db.Mission.ToList();
            model.typeMission = db.TypeMission.ToList();
            model.Vehicule = db.Vehicule.ToList();
            model.affectation = db.Affectation.ToList();
            model.missionM = new DAL.Mission
            {
                DateDebutMission = null,
                DateFinMission = null,
                DateCloturer = null
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Ajouter(DAL.Mission miss)
        {
            DateTime date1 = miss.DateDebutMission ?? new DateTime();
            DateTime date2 = miss.DateFinMission ?? new DateTime();

            if(DateTime.Now > miss.DateDebutMission )
            {
                ViewData["CompareDate"] = "La date de début doit être supérieur à la date d'aujourd'hui";
                model.categorieMission = db.CategorieMission.ToList();
                model.conducteur = db.Conducteur.ToList();
                model.mission = db.Mission.ToList();
                model.typeMission = db.TypeMission.ToList();
                model.Vehicule = db.Vehicule.ToList();
                model.affectation = db.Affectation.ToList();
                model.missionM = miss;

                return View("Index", model);
            }
            if (miss.DateDebutMission > miss.DateFinMission)
            {
                ViewData["CompareDate"] = "La date de début doit être supérieur à la date de fin";
                model.categorieMission = db.CategorieMission.ToList();
                model.conducteur = db.Conducteur.ToList();
                model.mission = db.Mission.ToList();
                model.typeMission = db.TypeMission.ToList();
                model.Vehicule = db.Vehicule.ToList();
                model.affectation = db.Affectation.ToList();
                model.missionM = miss;

                return View("Index", model);
            }
            else
            {
                int diff = Math.Abs(date1.Month - date2.Month);
                diff += Math.Abs((date1.Year - date2.Year) * 12);
                if(diff > 3)
                {
                    miss.IdTypeMission = 1;
                }
                else
                {
                    miss.IdTypeMission = 2;
                }

                DAL.Mission mission = new DAL.Mission
                {
                    LibelleMission = miss.LibelleMission,
                    DateDebutMission = miss.DateDebutMission,
                    DateFinMission = miss.DateFinMission,
                    IdCategorieMission = miss.IdCategorieMission,
                    IdTypeMission = miss.IdTypeMission,
                    Supprimer = false,
                    Cloturer = false,
                    DateCloturer = DateTime.Now,
                    DateSupprimer = DateTime.Now
                };
                db.Mission.Add(miss);

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout de la mission " + miss.LibelleMission;
                db.Action.Add(nvelActions);

                db.SaveChanges();

                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Cloturer(int id)
        {
            DAL.Mission mission = db.Mission.Find(id);
            mission.Cloturer = true;
            mission.DateCloturer = DateTime.Now;
            db.Entry(mission).State = EntityState.Modified;

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Clôture de la mission " + mission.LibelleMission;
            db.Action.Add(nvelActions);

            db.SaveChanges();

            List<DAL.Affectation> aff = db.Affectation.ToList();

            foreach (var item in aff.Where(p => p.IdMission == id))
            {
                item.Conducteur.EnMission = false;
                item.Vehicule.EnMission = false;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Editer(DAL.Mission tmp1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tmp1).State = EntityState.Modified;
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
            DAL.Mission mission = await db.Mission.FindAsync(id);

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Suppression de la mission " + mission.LibelleMission;
            db.Action.Add(nvelActions);

            mission.Supprimer = true;
            mission.Cloturer = true;
            mission.DateCloturer = DateTime.Now;
            mission.DateSupprimer = DateTime.Now;
            db.Entry(mission).State = EntityState.Modified;

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

        public ActionResult ExportListM()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Mission/CRMiss.rpt")));
            var e = db.Mission.Select(p => new
            {
                Nom = p.LibelleMission == null ? " " : p.LibelleMission,
                Categorie = p.CategorieMission.LibelleCategorieMission == null ? " " : p.CategorieMission.LibelleCategorieMission,
                Type = p.TypeMission.LibelleTypeMission == null ? " " : p.TypeMission.LibelleTypeMission,
                DateDeb = p.DateDebutMission ?? new DateTime(),
                DateFin = p.DateFinMission ?? new DateTime(),
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste mission par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeMission.pdf");
        }
    }
}