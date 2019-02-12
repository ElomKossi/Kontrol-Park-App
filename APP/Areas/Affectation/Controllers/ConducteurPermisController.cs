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
    public class ConducteurPermisController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.conducteur = db.Conducteur.ToList();
            model.conducteurPermis = db.ConducteurPermis.ToList();
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
        public ActionResult Ajouter(ConducteurPermis condPer, int? catp)
        {
            ConducteurPermis conducteurPermis = new ConducteurPermis
            {
                IdConducteur = condPer.IdConducteur,
                IdCategoriePermis = condPer.IdCategoriePermis,
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
            Conducteur condu = db.Conducteur.Find(condPer.IdConducteur);
            //nvelActions.LibelleAction = "Ajout du permis " + condPer.NumPermis +" de " + condu.NomConducteur + " " + condu.PrenomConducteur;
            db.Action.Add(nvelActions);

            db.ConducteurPermis.Add(conducteurPermis);
            db.SaveChanges();
            return RedirectToAction("index", "Conducteurs");
        }

        [HttpPost]
        public ActionResult Editer(ConducteurPermis tmp1)
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
                //nvelActions.LibelleAction = "Modification du permis " + tmp1.NumPermis + " de " + tmp1.Conducteur.NomConducteur + " " + tmp1.Conducteur.PrenomConducteur;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            ConducteurPermis conducteurPermis = db.ConducteurPermis.Find(id);

            if (conducteurPermis.Supprimer == true)
            {
                conducteurPermis.Supprimer = false;
                conducteurPermis.DateSupprimer = DateTime.Now;
                db.Entry(conducteurPermis).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                //nvelActions.LibelleAction = "Restauration du permis " + conducteurPermis.NumPermis + " de " + conducteurPermis.Conducteur.NomConducteur + " " + conducteurPermis.Conducteur.PrenomConducteur;
                db.Action.Add(nvelActions);
            }
            else
            {
                conducteurPermis.Supprimer = true;
                conducteurPermis.DateSupprimer = DateTime.Now;
                db.Entry(conducteurPermis).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                //nvelActions.LibelleAction = "Suppression du permis " + conducteurPermis.NumPermis + " de " + conducteurPermis.Conducteur.NomConducteur + " " + conducteurPermis.Conducteur.PrenomConducteur;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Mission/ConducteurPermis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConducteurPermis conducteurPermis = await db.ConducteurPermis.FindAsync(id);
            if (conducteurPermis == null)
            {
                return HttpNotFound();
            }
            return View(conducteurPermis);
        }

        // POST: Mission/ConducteurPermis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConducteurPermis conducteurPermis = await db.ConducteurPermis.FindAsync(id);
            conducteurPermis.Supprimer = true;
            db.Entry(conducteurPermis).State = EntityState.Modified;
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