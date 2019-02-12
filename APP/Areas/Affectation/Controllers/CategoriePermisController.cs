using APP.Areas.Affectation.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Affectation.Controllers
{
    public class CategoriePermisController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.categoriePermis = db.CategoriePermis.ToList();

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
        public ActionResult Ajouter(CategoriePermis catp)
        {
            CategoriePermis categoriePermis = new CategoriePermis
            {
                LibelleCategoriePermis = catp.LibelleCategoriePermis,
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
            nvelActions.LibelleAction = "Création d'une catégorie de permis: " + categoriePermis.LibelleCategoriePermis;
            db.Action.Add(nvelActions);

            db.CategoriePermis.Add(categoriePermis);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(CategoriePermis tmp1)
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
                nvelActions.LibelleAction = "modification d'une catégorie de permis: " + tmp1.LibelleCategoriePermis;
                db.Action.Add(nvelActions);

                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            CategoriePermis categoriePermis = db.CategoriePermis.Find(id);

            if (categoriePermis.Supprimer == true)
            {
                categoriePermis.Supprimer = false;
                categoriePermis.DateSupprimer = DateTime.Now;
                db.Entry(categoriePermis).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration d'une catégorie de permis: " + categoriePermis.LibelleCategoriePermis;
                db.Action.Add(nvelActions);
            }
            else
            {
                categoriePermis.Supprimer = true;
                categoriePermis.DateSupprimer = DateTime.Now;
                db.Entry(categoriePermis).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression d'une catégorie de permis: " + categoriePermis.LibelleCategoriePermis;
                db.Action.Add(nvelActions);
            }

            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Mission/CategoriePermis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriePermis categoriePermis = await db.CategoriePermis.FindAsync(id);
            if (categoriePermis == null)
            {
                return HttpNotFound();
            }
            return View(categoriePermis);
        }

        // POST: Mission/CategoriePermis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategoriePermis categoriePermis = await db.CategoriePermis.FindAsync(id);
            categoriePermis.Supprimer = true;
            categoriePermis.DateSupprimer = DateTime.Now;
            db.Entry(categoriePermis).State = EntityState.Modified;
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
    }
}