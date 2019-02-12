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
    public class MarqueVehiculesController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        // GET: Mission/AspectVehicules
        public ActionResult Index()
        {
            model.marqueVehicule = db.MarqueVehicule.ToList();

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
        public ActionResult Ajouter(string LibelleMarque)
        {
            MarqueVehicule marque = new MarqueVehicule
            {
                LibelleMarque = LibelleMarque,
                Supprimer = false,
                DateSupprimer = DateTime.Now,
            };
            if (ModelState.IsValid)
            {
                db.MarqueVehicule.Add(marque);

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout de la marque " + LibelleMarque;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("index");
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(MarqueVehicule tmp1)
        {
            MarqueVehicule mark = db.MarqueVehicule.Find(tmp1.IdMarque);
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
                nvelActions.LibelleAction = "Modification du nom de la marque " + mark.LibelleMarque + " en "+ tmp1.LibelleMarque;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            MarqueVehicule mark = db.MarqueVehicule.Find(id);

            if (mark.Supprimer == true)
            {
                mark.Supprimer = false;
                mark.DateSupprimer = DateTime.Now;
                db.Entry(mark).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration de la marque " + mark.LibelleMarque;
                db.Action.Add(nvelActions);
            }
            else
            {
                mark.Supprimer = true;
                mark.DateSupprimer = DateTime.Now;
                db.Entry(mark).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression de la marque " + mark.LibelleMarque;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Mission/MarqueVehicules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarqueVehicule marqueVehicule = await db.MarqueVehicule.FindAsync(id);
            if (marqueVehicule == null)
            {
                return HttpNotFound();
            }
            return View(marqueVehicule);
        }

        // POST: Mission/MarqueVehicules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MarqueVehicule marqueVehicule = await db.MarqueVehicule.FindAsync(id);
            marqueVehicule.Supprimer = true;
            marqueVehicule.DateSupprimer = DateTime.Now;
            db.Entry(marqueVehicule).State = EntityState.Modified;
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