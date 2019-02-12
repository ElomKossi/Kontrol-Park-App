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
    public class CategorieMissionsController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.categorieMission = db.CategorieMission.ToList();

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
        public ActionResult Ajouter(CategorieMission catm)
        {
            //int idMax = db.CategorieMission.Count();

            CategorieMission categorieMission = new CategorieMission
            {
                //IdCategorieMission = idMax + 1,
                LibelleCategorieMission = catm.LibelleCategorieMission,
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
            nvelActions.LibelleAction = "Création d'une catégorie de mission: " + catm.LibelleCategorieMission;
            db.Action.Add(nvelActions);

            db.CategorieMission.Add(categorieMission);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(CategorieMission tmp1)
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
                nvelActions.LibelleAction = "Modification d'une catégorie de mission: " + tmp1.LibelleCategorieMission;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            CategorieMission categorieMission = db.CategorieMission.Find(id);

            if (categorieMission.Supprimer == true)
            {
                categorieMission.Supprimer = false;
                categorieMission.DateSupprimer = DateTime.Now;
                db.Entry(categorieMission).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration d'une catégorie de mission: " + categorieMission.LibelleCategorieMission;
                db.Action.Add(nvelActions);
            }
            else
            {
                categorieMission.Supprimer = true;
                categorieMission.DateSupprimer = DateTime.Now;
                db.Entry(categorieMission).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression d'une catégorie de mission: " + categorieMission.LibelleCategorieMission;
                db.Action.Add(nvelActions);
            }

            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Mission/CategorieMissions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieMission categorieMission = await db.CategorieMission.FindAsync(id);
            if (categorieMission == null)
            {
                return HttpNotFound();
            }
            return View(categorieMission);
        }

        // GET: Mission/CategorieMissions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieMission categorieMission = await db.CategorieMission.FindAsync(id);
            if (categorieMission == null)
            {
                return HttpNotFound();
            }
            return View(categorieMission);
        }

        // POST: Mission/CategorieMissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategorieMission categorieMission = await db.CategorieMission.FindAsync(id);
            categorieMission.Supprimer = true;
            categorieMission.DateSupprimer = DateTime.Now;
            db.Entry(categorieMission).State = EntityState.Modified;
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