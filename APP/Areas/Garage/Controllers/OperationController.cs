using APP.Areas.Garage.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Garage.Controllers
{
    public class OperationController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        // GET: Garage/Operation
        //public ActionResult Index()
        //{
        //    model.carburant = db.Carburant.ToList();
        //    model.entreeCarburant = db.EntreeCarburant.ToList();
        //    model.sortieCarburant = db.SortieCarburant.ToList();

        //    model.huile = db.Huile.ToList();
        //    model.entreeHuile = db.EntreeHuile.ToList();
        //    model.sortieHuile = db.SortieHuile.ToList();

        //    model.Mecanicien = db.Mecanicien.ToList();
        //    model.Vehicule = db.Vehicule.ToList();

        //    return View(model);
        //}

        public ActionResult IndexCarb()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            string name = Server.HtmlEncode(aCookie.Value);
            Utilisateur actif = db.Utilisateur.Single(p => p.Identifiant == name);
            if (actif.IdProfil != 7 && actif.IdProfil != 9)
            {
                return View("AccesInterdit");
            }

            model.carburant = db.Carburant.Where(p => p.Supprimer == false).ToList();
            model.entreeCarburant = db.EntreeCarburant.ToList();
            model.sortieCarburant = db.SortieCarburant.ToList();

            model.Mecanicien = db.Mecanicien.ToList();
            model.Vehicule = db.Vehicule.Where(p => p.EnActivite == true).ToList();

            return View(model);
        }

        public ActionResult IndexHuil()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            string name = Server.HtmlEncode(aCookie.Value);
            Utilisateur actif = db.Utilisateur.Single(p => p.Identifiant == name);
            if (actif.IdProfil != 7 && actif.IdProfil != 9)
            {
                return View("AccesInterdit");
            }

            model.huile = db.Huile.Where(p => p.Supprimer == false).ToList();
            model.entreeHuile = db.EntreeHuile.ToList();
            model.sortieHuile = db.SortieHuile.ToList();

            model.Mecanicien = db.Mecanicien.ToList();
            model.Vehicule = db.Vehicule.Where(p => p.EnActivite == true).ToList();

            return View(model);
        }

        public ActionResult EntreeCarb(EntreeCarburant ent)
        {
            EntreeCarburant entc = new EntreeCarburant
            {
                IdCarburant = ent.IdCarburant,
                IdMecanicien = ent.IdMecanicien,
                QuantiteEntreeCarb = ent.QuantiteEntreeCarb,
                DateEntreeCarburant = ent.DateEntreeCarburant,
                Supprimer = false,
                DateSupprime = DateTime.Now
            };
            db.EntreeCarburant.Add(entc);

            Carburant carb = db.Carburant.Find(entc.IdCarburant);
            carb.VolumeCarburant = carb.VolumeCarburant + entc.QuantiteEntreeCarb;
            db.Entry(carb).State = EntityState.Modified;

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Ajout de " + entc.QuantiteEntreeCarb + "l à " + carb.LibelleCarburant;
            db.Action.Add(nvelActions);

            db.SaveChanges();
            return RedirectToAction("Indexcarb");
        }

        public ActionResult EntreeHuil(EntreeHuile ent)
        {
            EntreeHuile enth = new EntreeHuile
            {
                IdHuile = ent.IdHuile,
                IdMecanicien = ent.IdMecanicien,
                QuantiteEntreeHuile = ent.QuantiteEntreeHuile,
                DateEntreeHuile = ent.DateEntreeHuile,
                Supprimer = false,
                DateSupprime = DateTime.Now
            };
            db.EntreeHuile.Add(enth);

            Huile huile = db.Huile.Find(enth.IdHuile);
            huile.VolumeHuile = huile.VolumeHuile + enth.QuantiteEntreeHuile;
            db.Entry(huile).State = EntityState.Modified;

            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();
            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Ajout de " + enth.QuantiteEntreeHuile + "l à " + huile.LibelleHuile;
            db.Action.Add(nvelActions);

            db.SaveChanges();
            return RedirectToAction("IndexHuil");
        }

        public ActionResult SortieCarb(SortieHuile sor)
        {
            Vehicule veh = db.Vehicule.Find(sor.IdVehicule);

            Carburant carb = db.Carburant.Find(sor.IdHuile);

            if (carb.VolumeCarburant > sor.VolumeSortieHuile)
            {
                SortieCarburant sorh = new SortieCarburant
                {
                    IdCarburant = sor.IdHuile,
                    IdMecanicien = sor.IdMecanicien,
                    IdVehicule = sor.IdVehicule,
                    VolumeSortieCarburant = sor.VolumeSortieHuile,
                    DateSortieCarburant = sor.DateSortieHuile,
                    Supprimer = false,
                    DateSupprime = DateTime.Now
                };
                db.SortieCarburant.Add(sorh);

                int un = 2, de = 8;
                int trois = de - un;
                carb.VolumeCarburant = carb.VolumeCarburant - sorh.VolumeSortieCarburant;
                db.Entry(carb).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout de " + sorh.VolumeSortieCarburant + "l de " + carb.LibelleCarburant + " au véhicule " + veh.MarqueVehicule.LibelleMarque + veh.NomVehicule + veh.Immatriculation;
                db.Action.Add(nvelActions);
            }
            else
            {
                var reste = sor.VolumeSortieHuile - carb.VolumeCarburant;
                SortieCarburant sorh = new SortieCarburant
                {
                    IdCarburant = sor.IdHuile,
                    IdMecanicien = sor.IdMecanicien,
                    IdVehicule = sor.IdVehicule,
                    VolumeSortieCarburant = reste,
                    DateSortieCarburant = sor.DateSortieHuile,
                    Supprimer = false,
                    DateSupprime = DateTime.Now
                };
                db.SortieCarburant.Add(sorh);

                carb.VolumeCarburant = carb.VolumeCarburant - reste;
                db.Entry(carb).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout de " + reste + "l de " + carb.LibelleCarburant + " au véhicule " + veh.MarqueVehicule.LibelleMarque + veh.NomVehicule + veh.Immatriculation;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Indexcarb");
        }

        public ActionResult SortieHuil(SortieCarburant sor, int choix = 0)
        {
            Vehicule veh = db.Vehicule.Find(sor.IdVehicule);

            Huile huile = db.Huile.Find(sor.IdCarburant);

            if (huile.VolumeHuile > sor.VolumeSortieCarburant)
            {
                SortieHuile sorh = new SortieHuile
                {
                    IdHuile = sor.IdCarburant,
                    IdMecanicien = sor.IdMecanicien,
                    IdVehicule = sor.IdVehicule,
                    VolumeSortieHuile = sor.VolumeSortieCarburant,
                    DateSortieHuile = sor.DateSortieCarburant,
                    Supprimer = false,
                    DateSupprime = DateTime.Now
                };
                db.SortieHuile.Add(sorh);

                huile.VolumeHuile = huile.VolumeHuile - sorh.VolumeSortieHuile;
                db.Entry(huile).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout de " + sorh.VolumeSortieHuile + "l de " + huile.LibelleHuile + " au véhicule " + veh.MarqueVehicule.LibelleMarque + veh.NomVehicule + veh.Immatriculation;
                db.Action.Add(nvelActions);
            }
            else
            {
                var reste = sor.VolumeSortieCarburant - huile.VolumeHuile;
                SortieHuile sorh = new SortieHuile
                {
                    IdHuile = sor.IdCarburant,
                    IdMecanicien = sor.IdMecanicien,
                    IdVehicule = sor.IdVehicule,
                    VolumeSortieHuile = reste,
                    DateSortieHuile = sor.DateSortieCarburant,
                    Supprimer = false,
                    DateSupprime = DateTime.Now
                };
                db.SortieHuile.Add(sorh);

                huile.VolumeHuile = huile.VolumeHuile - reste;
                db.Entry(huile).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Ajout de " + reste + "l de " + huile.LibelleHuile + " au véhicule " + veh.MarqueVehicule.LibelleMarque + veh.NomVehicule + veh.Immatriculation;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("IndexHuil");
        }


    }
}