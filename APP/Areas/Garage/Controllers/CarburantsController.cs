using APP.Areas.Garage.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Garage.Controllers
{
    public class CarburantsController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.carburant = db.Carburant.ToList();

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
        public ActionResult Ajouter(Carburant carb)
        {
            Carburant carburant = new Carburant
            {
                LibelleCarburant = carb.LibelleCarburant,
                VolumeCarburant = 0,
                Supprimer = false,
                DateSupprimer = DateTime.Now,
            };
            db.Carburant.Add(carburant);

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Ajout du carburant " + carb.LibelleCarburant;
            db.Action.Add(nvelActions);

            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Editer(Carburant tmp1)
        {
            if (ModelState.IsValid)
            {
                var l = db.Carburant.Find(tmp1.IdCarburant);
                l.LibelleCarburant = tmp1.LibelleCarburant;
                db.Entry(l).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Modification du carburant " + tmp1.LibelleCarburant;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            Carburant typeC = db.Carburant.Find(id);

            if (typeC.Supprimer == true)
            {
                typeC.Supprimer = false;
                typeC.DateSupprimer = DateTime.Now;
                db.Entry(typeC).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration du carburant " + typeC.LibelleCarburant;
                db.Action.Add(nvelActions);
            }
            else
            {
                typeC.Supprimer = true;
                typeC.DateSupprimer = DateTime.Now;
                db.Entry(typeC).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression du carburant " + typeC.LibelleCarburant;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carburant carburant = await db.Carburant.FindAsync(id);
            if (carburant == null)
            {
                return HttpNotFound();
            }
            return View(carburant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Carburant carburant = await db.Carburant.FindAsync(id);
            carburant.Supprimer = true;
            carburant.DateSupprimer = DateTime.Now;
            db.Entry(carburant).State = EntityState.Modified;
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