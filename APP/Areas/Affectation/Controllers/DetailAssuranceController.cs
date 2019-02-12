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
    public class DetailAssuranceController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.Vehicule = db.Vehicule.ToList();
            model.assurance = db.Assurance.ToList();
            model.detailAssurance = db.DetailAssurance.ToList();
            model.DetAss = new DetailAssurance
            {
                DataDebutCouverture = null,
                DateFinCouverture = null
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Ajouter(DetailAssurance detass)
        {
            if (DateTime.Now > detass.DataDebutCouverture)
            {
                ViewData["CompareDate"] = "La date de début doit être supérieur à la date d'aujourd'hui";
                model.Vehicule = db.Vehicule.ToList();
                model.assurance = db.Assurance.ToList();
                model.detailAssurance = db.DetailAssurance.ToList();
                model.DetAss = detass;

                return View("Index", model);
            }
            if (detass.DataDebutCouverture > detass.DateFinCouverture)
            {
                ViewData["CompareDate"] = "La date de début doit être supérieur à la date de fin";
                model.Vehicule = db.Vehicule.ToList();
                model.assurance = db.Assurance.ToList();
                model.detailAssurance = db.DetailAssurance.ToList();
                model.DetAss = detass;

                return View("Index", model);
            }
            else
            {
                int id = db.DetailAssurance.Count();
                DetailAssurance det = new DetailAssurance
                {
                    IdDetailAssurance = id+1,
                    IdAssurance = detass.IdAssurance,
                    IdVehicule = detass.IdVehicule,
                    DataDebutCouverture = detass.DataDebutCouverture,
                    DateFinCouverture = detass.DateFinCouverture,
                };
                db.DetailAssurance.Add(det);

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                Assurance ass = db.Assurance.Find(det.IdAssurance);
                Vehicule Veh = db.Vehicule.Find(det.IdVehicule);
                nvelActions.LibelleAction = "Ajout de l'assurance " + ass.LibelleAssurance + " du véhicule immatriculé" + Veh.Immatriculation;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Editer(DetailAssurance tmp1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tmp1).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                Assurance ass = db.Assurance.Find(tmp1.IdAssurance);
                Vehicule Veh = db.Vehicule.Find(tmp1.IdVehicule);
                nvelActions.LibelleAction = "Modification de l'assurance " + ass.LibelleAssurance + " du véhicule immatriculé" + Veh.Immatriculation;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        // GET: Mission/HistoriqueAspects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailAssurance detailAssurance = await db.DetailAssurance.FindAsync(id);
            if (detailAssurance == null)
            {
                return HttpNotFound();
            }
            return View(detailAssurance);
        }

        // POST: Mission/HistoriqueAspects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DetailAssurance detailAssurance = await db.DetailAssurance.FindAsync(id);
            db.DetailAssurance.Remove(detailAssurance);

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Suppression de l'assurance " + detailAssurance.Assurance.LibelleAssurance + " du véhicule immatriculé" + detailAssurance.Vehicule.Immatriculation;
            db.Action.Add(nvelActions);

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

        public ActionResult ExportListC()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/DetailAssurance/CRDetAss.rpt")));
            var e = db.DetailAssurance.Select(p => new
            {
                Assurance = p.Assurance.LibelleAssurance == null ? " " : p.Assurance.LibelleAssurance,
                Vehicule = p.Vehicule.MarqueVehicule.LibelleMarque + " " + p.Vehicule.NomVehicule + " " + p.Vehicule.Immatriculation ?? " ",
                DateDeb = p.DataDebutCouverture ?? new DateTime(),
                DateFin = p.DateFinCouverture ?? new DateTime(),
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des véhicules et leur assurance par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeVehiculeAssurance.pdf");
        }
    }
}