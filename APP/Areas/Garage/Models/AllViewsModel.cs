using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Areas.Garage.Models
{
    public class AllViewsModel
    {
        public List<Carburant> carburant { get; set; }
        public List<DetatilIntervention> detatilIntervention { get; set; }
        public List<EntreeCarburant> entreeCarburant { get; set; }
        public List<EntreeHuile> entreeHuile { get; set; }
        public List<Huile> huile { get; set; }
        public List<Intervention> intervention { get; set; }
        public Intervention inter { get; set; }
        public List<Piece> piece { get; set; }
        public List<int> IdPiece { get; set; }
        public List<SortieCarburant> sortieCarburant { get; set; }
        public List<SortieHuile> sortieHuile { get; set; }
        public List<MecaIntervention> mecaIntervention { get; set; }
        public List<Mecanicien> Mecanicien { get; set; }
        public Mecanicien Meca { get; set; }
        public int IdMeca { get; set; }
        public List<TypeIntervention> typeIntervention { get; set; }
        public List<Vehicule> Vehicule { get; set; }
        public Vehicule VehiculeM { get; set; }
        public List<MarqueVehicule> marque { get; set; }
    }
}