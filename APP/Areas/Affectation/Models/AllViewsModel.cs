using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Areas.Affectation.Models
{
    public class AllViewsModel
    {
        public List<DAL.Affectation> affectation { get; set; }
        public DAL.Affectation affectationP { get; set; }
        public List<AspectVehicule> aspectVehicule { get; set; }
        public List<Assurance> assurance { get; set; }
        public List<Carburant> carburant { get; set; }
        public List<CategorieMission> categorieMission { get; set; }
        public List<CategoriePermis> categoriePermis { get; set; }
        public List<Conducteur> conducteur { get; set; }
        public Conducteur conducteurM { get; set; }
        public List<ConducteurPermis> conducteurPermis { get; set; }
        public List<int> IdCondPer { get; set; }

        public ConducteurPermis conducteurPermisP { get; set; }
        public Conducteur cond { get; set; }

        public List<DetailAssurance> detailAssurance { get; set; }
        public DetailAssurance DetAss { get; set; }

        public List<Fournisseur> fournisseur { get; set; }
        public List<HistoriqueAspect> historiqueAspect { get; set; }
        public List<MarqueVehicule> marqueVehicule { get; set; }
        public List<Mission> mission { get; set; }
        public Mission missionM { get; set; }
        public List<TypeCarburant> typeCarburant { get; set; }
        public List<TypeMission> typeMission { get; set; }
        public List<TypeVehicule> typeVehicule { get; set; }
        public List<Vehicule> Vehicule { get; set; }
        public Vehicule VehiculeM { get; set; }

        public List<DAL.Action> action { get; set; }

        public virtual Conducteur Conducteur { get; set; }
        public virtual Mission Mission { get; set; }
        public virtual Vehicule Vehicules { get; set; }
    }
}