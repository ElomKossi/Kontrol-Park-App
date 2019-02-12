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
    public class FournisseursController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.fournisseur = db.Fournisseur.ToList();

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
        public ActionResult Ajouter(Fournisseur four)
        {
            Fournisseur fournisseur = new Fournisseur
            {
                LibelleFournisseur = four.LibelleFournisseur,
                AdresseFournisseur = four.AdresseFournisseur,
                EmailFournisseur = four.EmailFournisseur,
                TelFournisseur = four.TelFournisseur,
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
            nvelActions.LibelleAction = "Ajout d'une fournisseur: " + four.LibelleFournisseur;
            db.Action.Add(nvelActions);

            db.Fournisseur.Add(fournisseur);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(Fournisseur tmp1)
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
                nvelActions.LibelleAction = "Modification d'une fournisseur: " + tmp1.LibelleFournisseur;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            Fournisseur fournisseur = db.Fournisseur.Find(id);

            if (fournisseur.Supprimer == true)
            {
                fournisseur.Supprimer = false;
                fournisseur.DateSupprimer = DateTime.Now;
                db.Entry(fournisseur).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration d'un fournisseur: " + fournisseur.LibelleFournisseur;
                db.Action.Add(nvelActions);
            }
            else
            {
                fournisseur.Supprimer = true;
                fournisseur.DateSupprimer = DateTime.Now;
                db.Entry(fournisseur).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression d'un fournisseur: " + fournisseur.LibelleFournisseur;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Mission/Fournisseurs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fournisseur fournisseur = await db.Fournisseur.FindAsync(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }

        // POST: Mission/Fournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fournisseur fournisseur = await db.Fournisseur.FindAsync(id);
            fournisseur.Supprimer = true;
            fournisseur.DateSupprimer = DateTime.Now;
            db.Entry(fournisseur).State = EntityState.Modified;
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
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Fournisseur/CRFour.rpt")));
            var e = db.Fournisseur.Select(p => new
            {
                Nom = p.LibelleFournisseur ?? " ",
                Adresse = p.AdresseFournisseur == null ? " " : p.AdresseFournisseur,
                Tel = p.TelFournisseur == null ? " " : p.TelFournisseur,
                Email = p.EmailFournisseur == null ? " " : p.EmailFournisseur,
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des Fournisseurs par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeFournisseurs.pdf");
        }
    }
}