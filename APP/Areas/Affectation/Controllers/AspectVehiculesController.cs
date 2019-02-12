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
    public class AspectVehiculesController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        // GET: Mission/AspectVehicules
        public ActionResult Index()
        {
            model.aspectVehicule = db.AspectVehicule.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Ajouter(string LibelleAspect)
        {
            AspectVehicule aspect = new AspectVehicule
            {
                LibelleAspect = LibelleAspect,
                Supprimer = false,
                DateSupprimer = DateTime.Now,
            };
            if (ModelState.IsValid)
            {
                db.AspectVehicule.Add(aspect);

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout d'un état: " + LibelleAspect;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("index");
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(AspectVehicule tmp1)
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
                nvelActions.LibelleAction = "Modification d'un état: " + tmp1.LibelleAspect;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            AspectVehicule asp = db.AspectVehicule.Find(id);

            if (asp.Supprimer == true)
            {
                asp.Supprimer = false;
                asp.DateSupprimer = DateTime.Now;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration d'un état de véhicule: " + asp.LibelleAspect;
                db.Action.Add(nvelActions);
            }
            else
            {
                asp.Supprimer = true;
                asp.DateSupprimer = DateTime.Now;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression d'un état de véhicule: " + asp.LibelleAspect;
                db.Action.Add(nvelActions);
            }

            db.Entry(asp).State = EntityState.Modified;
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Mission/AspectVehicules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspectVehicule aspectVehicule = await db.AspectVehicule.FindAsync(id);
            if (aspectVehicule == null)
            {
                return HttpNotFound();
            }
            return View(aspectVehicule);

        }


        // POST: Mission/AspectVehicules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            AspectVehicule aspectVehicule = await db.AspectVehicule.FindAsync(id);
            aspectVehicule.Supprimer = false;
            aspectVehicule.DateSupprimer = DateTime.Now;
            db.Entry(aspectVehicule).State = EntityState.Modified;
            //db.AspectVehicule.Remove(aspectVehicule);

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Suppression d'un état: " + aspectVehicule.LibelleAspect;
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
    }
}