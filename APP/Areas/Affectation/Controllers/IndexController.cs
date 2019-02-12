using APP.Areas.Affectation.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Affectation.Controllers
{
    public class IndexController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        // GET: Mission/Index
        public ActionResult Index()
        {
            model.aspectVehicule = db.AspectVehicule.ToList();
            model.assurance = db.Assurance.ToList();
            model.carburant = db.Carburant.ToList();
            model.categorieMission = db.CategorieMission.ToList();
            model.categoriePermis = db.CategoriePermis.ToList();
            model.conducteur = db.Conducteur.ToList();
            model.conducteurPermis = db.ConducteurPermis.ToList();
            model.fournisseur = db.Fournisseur.ToList();
            model.marqueVehicule = db.MarqueVehicule.ToList();
            model.mission = db.Mission.ToList();
            model.typeMission = db.TypeMission.ToList();
            model.typeVehicule = db.TypeVehicule.ToList();

            return View(model);
        }

        public ActionResult IndexVehicule()
        {
            model.aspectVehicule = db.AspectVehicule.ToList();
            model.assurance = db.Assurance.ToList();
            model.carburant = db.Carburant.ToList();
            model.categorieMission = db.CategorieMission.ToList();
            model.categoriePermis = db.CategoriePermis.ToList();
            model.conducteur = db.Conducteur.ToList();
            model.conducteurPermis = db.ConducteurPermis.ToList();
            model.fournisseur = db.Fournisseur.ToList();
            model.marqueVehicule = db.MarqueVehicule.ToList();
            model.mission = db.Mission.ToList();
            model.typeMission = db.TypeMission.ToList();
            model.typeVehicule = db.TypeVehicule.ToList();
            model.Vehicule = db.Vehicule.ToList();

            HttpCookie aCookie = Request.Cookies["ident"];
            string name = Server.HtmlEncode(aCookie.Value);
            Utilisateur actif = db.Utilisateur.Single(p => p.Identifiant == name);
            if (actif.IdProfil != 6 && actif.IdProfil != 9)
            {
                return View("AccesInterdit");
            }

            return View(model);
        }
    }
}