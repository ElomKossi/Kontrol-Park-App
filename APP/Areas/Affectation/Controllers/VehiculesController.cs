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
    public class VehiculesController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.aspectVehicule = db.AspectVehicule.ToList();
            model.assurance = db.Assurance.ToList();
            model.categorieMission = db.CategorieMission.ToList();
            model.categoriePermis = db.CategoriePermis.ToList();
            model.conducteur = db.Conducteur.ToList();
            model.conducteurPermis = db.ConducteurPermis.ToList();
            model.fournisseur = db.Fournisseur.ToList();
            model.marqueVehicule = db.MarqueVehicule.ToList();
            model.mission = db.Mission.ToList();
            model.typeCarburant = db.TypeCarburant.ToList();
            model.typeMission = db.TypeMission.ToList();
            model.typeVehicule = db.TypeVehicule.ToList();
            model.Vehicule = db.Vehicule.ToList();
            model.VehiculeM = new Vehicule
            {
                DateAcquisition = null,
                DateExpireGarantie = null
            };
            model.DetAss = new DetailAssurance
            {
                DataDebutCouverture = null,
                DateFinCouverture = null
            };

            model.action = db.Action.ToList();

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
        public ActionResult Ajouter(Vehicule veh)
        {
            foreach (var item in db.Vehicule.ToList())
            {
                if (veh.NumChassis == item.NumChassis)
                {
                    ViewData["Compare"] = "Le numero de châssis est déjà utilisés";
                    model.aspectVehicule = db.AspectVehicule.ToList();
                    model.assurance = db.Assurance.ToList();
                    model.categorieMission = db.CategorieMission.ToList();
                    model.categoriePermis = db.CategoriePermis.ToList();
                    model.conducteur = db.Conducteur.ToList();
                    model.conducteurPermis = db.ConducteurPermis.ToList();
                    model.fournisseur = db.Fournisseur.ToList();
                    model.marqueVehicule = db.MarqueVehicule.ToList();
                    model.mission = db.Mission.ToList();
                    model.typeCarburant = db.TypeCarburant.ToList();
                    model.typeMission = db.TypeMission.ToList();
                    model.typeVehicule = db.TypeVehicule.ToList();
                    model.Vehicule = db.Vehicule.ToList();
                    model.VehiculeM = new Vehicule
                    {
                        DateAcquisition = null,
                        DateExpireGarantie = null
                    };

                    return View("Index", model);
                }
                if (veh.Immatriculation == item.Immatriculation)
                {
                    ViewData["Compare"] = "Le numero d'immatriculation est déjà utilisés";
                    model.aspectVehicule = db.AspectVehicule.ToList();
                    model.assurance = db.Assurance.ToList();
                    model.categorieMission = db.CategorieMission.ToList();
                    model.categoriePermis = db.CategoriePermis.ToList();
                    model.conducteur = db.Conducteur.ToList();
                    model.conducteurPermis = db.ConducteurPermis.ToList();
                    model.fournisseur = db.Fournisseur.ToList();
                    model.marqueVehicule = db.MarqueVehicule.ToList();
                    model.mission = db.Mission.ToList();
                    model.typeCarburant = db.TypeCarburant.ToList();
                    model.typeMission = db.TypeMission.ToList();
                    model.typeVehicule = db.TypeVehicule.ToList();
                    model.Vehicule = db.Vehicule.ToList();
                    model.VehiculeM = new Vehicule
                    {
                        DateAcquisition = null,
                        DateExpireGarantie = null
                    };

                    return View("Index", model);
                }
            }

            if (DateTime.Now > veh.DateAcquisition)
            {
                ViewData["Compare"] = "La date d'acquisition ne doit pas dépasser la date d'aujourdhui";
                model.aspectVehicule = db.AspectVehicule.ToList();
                model.assurance = db.Assurance.ToList();
                model.categorieMission = db.CategorieMission.ToList();
                model.categoriePermis = db.CategoriePermis.ToList();
                model.conducteur = db.Conducteur.ToList();
                model.conducteurPermis = db.ConducteurPermis.ToList();
                model.fournisseur = db.Fournisseur.ToList();
                model.marqueVehicule = db.MarqueVehicule.ToList();
                model.mission = db.Mission.ToList();
                model.typeCarburant = db.TypeCarburant.ToList();
                model.typeMission = db.TypeMission.ToList();
                model.typeVehicule = db.TypeVehicule.ToList();
                model.Vehicule = db.Vehicule.ToList();
                model.VehiculeM = new Vehicule
                {
                    DateAcquisition = null,
                    DateExpireGarantie = null
                };

                return View("Index", model);
            }
            else if (veh.DateAcquisition > veh.DateExpireGarantie)
            {
                ViewData["Compare"] = "La date d'acquisition ne doit pas être supérieur à la date d'expiration de la garantie";
                model.aspectVehicule = db.AspectVehicule.ToList();
                model.assurance = db.Assurance.ToList();
                model.categorieMission = db.CategorieMission.ToList();
                model.categoriePermis = db.CategoriePermis.ToList();
                model.conducteur = db.Conducteur.ToList();
                model.conducteurPermis = db.ConducteurPermis.ToList();
                model.fournisseur = db.Fournisseur.ToList();
                model.marqueVehicule = db.MarqueVehicule.ToList();
                model.mission = db.Mission.ToList();
                model.typeCarburant = db.TypeCarburant.ToList();
                model.typeMission = db.TypeMission.ToList();
                model.typeVehicule = db.TypeVehicule.ToList();
                model.Vehicule = db.Vehicule.ToList();
                model.VehiculeM = new Vehicule
                {
                    DateAcquisition = null,
                    DateExpireGarantie = null
                };

                return View("Index", model);
            }
            else
            {
                Vehicule vehicule = new Vehicule
                {
                    NumChassis = veh.NumChassis,
                    NomVehicule = veh.NomVehicule,
                    Immatriculation = veh.Immatriculation,
                    DateAcquisition = veh.DateAcquisition,
                    DateExpireGarantie = veh.DateExpireGarantie,
                    IdTypeCarb = veh.IdTypeCarb,
                    IdFournisseur = veh.IdFournisseur,
                    IdMarque = veh.IdMarque,
                    IdType = veh.IdType,
                    EnActivite = true,
                    EnMission = false,
                    DateDesactivation = DateTime.Now,
                };
                db.Vehicule.Add(vehicule);

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                MarqueVehicule mark = db.MarqueVehicule.Find(veh.IdMarque);
                nvelActions.LibelleAction = "Ajout du véhicule " + mark.IdMarque + " " + veh.NomVehicule + " " + veh.Immatriculation;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Editer(Vehicule tmp1)
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
                MarqueVehicule mark = db.MarqueVehicule.Find(tmp1.IdMarque);
                nvelActions.LibelleAction = "Modification du véhicule " + mark.LibelleMarque + " " + tmp1.NomVehicule + " " + tmp1.Immatriculation;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            Vehicule veh = db.Vehicule.Find(id);

            if (veh.EnActivite == true)
            {
                veh.EnActivite = false;
                veh.DateDesactivation = DateTime.Now;
                db.Entry(veh).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                MarqueVehicule mark = db.MarqueVehicule.Find(veh.IdMarque);
                nvelActions.LibelleAction = "Remise en activité du véhicule " + mark.LibelleMarque +" "+ veh.NomVehicule + " " + veh.Immatriculation;
                db.Action.Add(nvelActions);
            }
            else
            {
                veh.EnActivite = true;
                veh.DateDesactivation = DateTime.Now;
                db.Entry(veh).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                MarqueVehicule mark = db.MarqueVehicule.Find(veh.IdMarque);
                nvelActions.LibelleAction = "Mise hors activité du véhicule " + mark.LibelleMarque + " " + veh.NomVehicule + " " + veh.Immatriculation;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Mission/Vehicules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicule vehicule = await db.Vehicule.FindAsync(id);
            if (vehicule == null)
            {
                return HttpNotFound();
            }
            return View(vehicule);
        }

        // POST: Mission/Vehicules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vehicule vehicule = await db.Vehicule.FindAsync(id);
            vehicule.EnActivite = false;
            vehicule.DateDesactivation = DateTime.Now;
            vehicule.EnMission = false;
            db.Entry(vehicule).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult ExportListV()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Vehicule/CRVeh.rpt")));
            var e = db.Vehicule.Select(p => new
            {
                Chassi = p.NumChassis == null ? " " : p.NumChassis,
                Type = p.TypeVehicule.LibelleType == null ? " " : p.TypeVehicule.LibelleType,
                Libelle = p.MarqueVehicule.LibelleMarque + p.NomVehicule == null ? " " : p.MarqueVehicule.LibelleMarque +" "+ p.NomVehicule,
                Matricule = p.Immatriculation == null ? " " : p.Immatriculation,
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser == null ? " " : t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des Véhicules par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeVéhicule.pdf");
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