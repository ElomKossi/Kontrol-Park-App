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
    public class AssurancesController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.assurance = db.Assurance.ToList();

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
        public ActionResult Ajouter(Assurance assur)
        {

            Assurance assurance = new Assurance
            {
                LibelleAssurance = assur.LibelleAssurance,
                AdresseAssurance = assur.AdresseAssurance,
                EmailAssurance = assur.EmailAssurance,
                TelAssurance = assur.TelAssurance,
                Supprimer = false,
                DateSupprimer = DateTime.Now,
            };

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Ajout d'une assurance: " + assurance.LibelleAssurance;
            db.Action.Add(nvelActions);

            db.Assurance.Add(assurance);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(Assurance tmp1)
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
                nvelActions.LibelleAction = "Modification d'une assurance: " + tmp1.LibelleAssurance;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            Assurance assurance = db.Assurance.Find(id);

            if (assurance.Supprimer == true)
            {
                assurance.Supprimer = false;
                assurance.DateSupprimer = DateTime.Now;
                db.Entry(assurance).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration d'une assurance: " + assurance.LibelleAssurance;
                db.Action.Add(nvelActions);
            }
            else
            {
                assurance.Supprimer = true;
                assurance.DateSupprimer = DateTime.Now;
                db.Entry(assurance).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression d'une assurance: " + assurance.LibelleAssurance;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Mission/AspectVehicules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = await db.Assurance.FindAsync(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
            return View(assurance);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Assurance assurance = await db.Assurance.FindAsync(id);
            assurance.Supprimer = true;
            assurance.DateSupprimer = DateTime.Now;
            db.Entry(assurance).State = EntityState.Modified;
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
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Assurance/CRAssur.rpt")));
            var e = db.Assurance.Select(p => new
            {
                Nom = p.LibelleAssurance ?? " ",
                Adresse = p.AdresseAssurance == null ? " " : p.AdresseAssurance,
                Tel = p.TelAssurance == null ? " " : p.TelAssurance,
                Email = p.EmailAssurance == null ? " " : p.EmailAssurance,
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des Assurances par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeAssurances.pdf");
        }
    }
}