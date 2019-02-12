using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Areas.Administraton.Models
{
    public class AdminViewModel
    {
        public List<DAL.AspectVehicule> aspectVehicule { get; set; }
        public List<Assurance> assurance { get; set; }
        public List<Carburant> carburant { get; set; }
        public List<CategorieMission> categorieMission { get; set; }
        public List<CategoriePermis> categoriePermis { get; set; }
        public List<Conducteur> conducteur { get; set; }
        public List<ConducteurPermis> conducteurPermis { get; set; }
        public List<DAL.Affectation> affectation { get; set; }
        public List<Fournisseur> fournisseur { get; set; }
        public List<HistoriqueAspect> historiqueAspect { get; set; }
        public List<MarqueVehicule> marqueVehicule { get; set; }
        public List<DAL.Mission> mission { get; set; }
        public List<TypeMission> typeMission { get; set; }
        public List<TypeVehicule> typeVehicule { get; set; }
        public List<TypeCarburant> typeCarburant { get; set; }
        public List<Vehicule> Vehicule { get; set; }

        public List<SortieCarburant> sortieCarburant { get; set; }
        public List<SortieHuile> sortieHuile { get; set; }
        public List<DetatilIntervention> detatilIntervention { get; set; }
        public List<Huile> huile { get; set; }
        public List<Intervention> intervention { get; set; }
        public List<EntreeCarburant> entreeCarburant { get; set; }
        public List<EntreeHuile> entreeHuile { get; set; }
        public List<MecaIntervention> mecaIntervention { get; set; }
        public List<Mecanicien> mecanicien { get; set; }
        public List<Piece> piece { get; set; }
        public List<TypeIntervention> typeIntervention { get; set; }

        public List<DAL.Action> action { get; set; }
        public List<AvoirDroit> avoirDroit { get; set; }
        public List<Connexion> connexion { get; set; }
        public List<Utilisateur> utilisateur { get; set; }
        public List<Utilisateur> UtilisateursActifs { get; set; }
        public Utilisateur UserActif { get; set; }
        public Utilisateur U { get; set; }
        public List<Droit> droit { get; set; }
        public List<Profil> profil { get; set; }

        public AdminViewModel()
        {
            utilisateur = new List<Utilisateur>();
            droit = new List<Droit>();
        }

        public bool Pofilexiste(Profil nouveauProfil)
        {
            if (profil != null)
            {
                return
                    profil.Any(
                        p => string.Compare(p.LibelleProfil, nouveauProfil.LibelleProfil, StringComparison.CurrentCultureIgnoreCase) == 0);

            }
            return false;
        }

        public bool Droitexiste(Droit Newdroit)
        {
            if (droit != null)
            {
                return
                    droit.Any(
                        p => string.Compare(p.LibelleDroit, Newdroit.LibelleDroit, StringComparison.CurrentCultureIgnoreCase) == 0);

            }
            return false;
        }

        public bool UtilisateurExiste(Utilisateur u)
        {
            if (utilisateur.Count != 0)
                return
                    utilisateur.Any(
                        utilisateur =>
                            string.Compare(utilisateur.EmailUser, u.EmailUser, StringComparison.CurrentCultureIgnoreCase) == 0) ||
                    utilisateur.Any(
                        utilisateur =>
                            string.Compare(utilisateur.Identifiant, u.Identifiant, StringComparison.CurrentCultureIgnoreCase) ==
                            0)
                    ||
                    utilisateur.Any(
                        utilisateur =>
                            string.Compare(utilisateur.TelUser, u.TelUser, StringComparison.CurrentCultureIgnoreCase) == 0);
            return false;
        }
    }
}