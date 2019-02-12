using APP.Areas.Administraton.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Administraton.Controllers
{
    public class SuiviController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AdminViewModel model = new AdminViewModel();

        // GET: Administraton/Suivi
        public ActionResult Index()
        {
            model.categorieMission = db.CategorieMission.ToList();
            model.typeMission = db.TypeMission.ToList();
            model.mission = db.Mission.ToList();
            model.affectation = db.Affectation.ToList();
            model.Vehicule = db.Vehicule.ToList();
            model.conducteur = db.Conducteur.ToList();

            model.aspectVehicule = db.AspectVehicule.ToList();
            model.assurance = db.Assurance.ToList();
            model.categoriePermis = db.CategoriePermis.ToList();
            model.conducteurPermis = db.ConducteurPermis.ToList();
            model.fournisseur = db.Fournisseur.ToList();
            model.marqueVehicule = db.MarqueVehicule.ToList();
            model.typeCarburant = db.TypeCarburant.ToList();
            model.typeVehicule = db.TypeVehicule.ToList();
            model.Vehicule = db.Vehicule.ToList();

            model.intervention = db.Intervention.ToList();
            model.mecanicien = db.Mecanicien.ToList();
            model.detatilIntervention = db.DetatilIntervention.ToList();
            model.piece = db.Piece.ToList();
            model.typeIntervention = db.TypeIntervention.ToList();

            model.utilisateur = db.Utilisateur.ToList();
            model.profil = db.Profil.ToList();
            model.droit = db.Droit.ToList();
            model.avoirDroit = db.AvoirDroit.ToList();
            model.connexion = db.Connexion.ToList();
            model.action = db.Action.ToList();

            return View(model);
        }
    }
}