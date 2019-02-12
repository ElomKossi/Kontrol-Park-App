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
    public class HistoriqueAspectsController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.Vehicule = db.Vehicule.ToList();
            model.aspectVehicule = db.AspectVehicule.ToList();
            model.historiqueAspect = db.HistoriqueAspect.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Ajouter(HistoriqueAspect hiasp)
        {
            HistoriqueAspect histo = new HistoriqueAspect
            {
                IdAspect = hiasp.IdAspect,
                IdVehicule = hiasp.IdVehicule,
                DateMAJ = hiasp.DateMAJ,
            };
            db.HistoriqueAspect.Add(histo);

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            AspectVehicule ass = db.AspectVehicule.Find(hiasp.IdAspect);
            Vehicule Veh = db.Vehicule.Find(hiasp.IdVehicule);
            nvelActions.LibelleAction = "Ajout de l'aspect " + ass.LibelleAspect + " du véhicule immatriculé" + Veh.Immatriculation;
            db.Action.Add(nvelActions);

            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(HistoriqueAspect tmp1)
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
                nvelActions.LibelleAction = "Modification de l'aspect " + tmp1.AspectVehicule.LibelleAspect + " du véhicule immatriculé" + tmp1.Vehicule.Immatriculation;
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
            HistoriqueAspect historiqueAspect = await db.HistoriqueAspect.FindAsync(id);
            if (historiqueAspect == null)
            {
                return HttpNotFound();
            }
            return View(historiqueAspect);
        }

        // POST: Mission/HistoriqueAspects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HistoriqueAspect historiqueAspect = await db.HistoriqueAspect.FindAsync(id);
            db.HistoriqueAspect.Remove(historiqueAspect);

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Suppression de l'aspect " + historiqueAspect.AspectVehicule.LibelleAspect + " du véhicule immatriculé" +historiqueAspect.Vehicule.Immatriculation;
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
            rd.Load(Path.Combine(Server.MapPath("~/Reports/HistoriqueAspects/CRHist.rpt")));
            var e = db.HistoriqueAspect.Select(p => new
            {
                Aspect = p.AspectVehicule.LibelleAspect == null ? " " : p.AspectVehicule.LibelleAspect,
                Vehicule = p.Vehicule.MarqueVehicule.LibelleMarque + " " + p.Vehicule.NomVehicule + " " + p.Vehicule.Immatriculation ?? " ",
                Date = p.DateMAJ ?? new DateTime()
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des états des véhicules par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeHistorique.pdf");
        }
    }
}