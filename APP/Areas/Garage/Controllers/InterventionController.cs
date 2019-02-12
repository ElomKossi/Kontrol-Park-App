using APP.Areas.Garage.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Garage.Controllers
{
    public class InterventionController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        // GET: Garage/Intervention
        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            string name = Server.HtmlEncode(aCookie.Value);
            Utilisateur actif = db.Utilisateur.Single(p => p.Identifiant == name);
            if (actif.IdProfil != 7 && actif.IdProfil != 9)
            {
                return View("AccesInterdit");
            }

            model.intervention = db.Intervention.ToList();
            model.Mecanicien = db.Mecanicien.ToList();
            model.Vehicule = db.Vehicule.ToList();
            model.marque = db.MarqueVehicule.ToList();
            model.detatilIntervention = db.DetatilIntervention.ToList();
            model.piece = db.Piece.ToList();
            model.typeIntervention = db.TypeIntervention.ToList();
            model.inter = new Intervention
            {
                DateDebut = null,
                DateFin = null
            };
            model.IdMeca = 0;
            model.IdPiece = null;

            return View(model);
        }

        [HttpPost]
        public ActionResult Ajouter(Intervention inter, List<int> IdPiece, int IdMecanicien)
        {
            if (DateTime.Now > inter.DateDebut)
            {
                ViewData["CompareDate"] = "La date de début doit être supérieur à la date d'aujourd'hui";
                model.intervention = db.Intervention.ToList();
                model.Mecanicien = db.Mecanicien.ToList();
                model.Vehicule = db.Vehicule.ToList();
                model.marque = db.MarqueVehicule.ToList();
                model.detatilIntervention = db.DetatilIntervention.ToList();
                model.piece = db.Piece.ToList();
                model.typeIntervention = db.TypeIntervention.ToList();
                model.inter = inter;
                model.IdMeca = IdMecanicien;
                model.IdPiece = IdPiece;

                return View("Index", model);
            }
            if (inter.DateDebut > inter.DateFin)
            {
                ViewData["CompareDate"] = "La date de début doit être supérieur à la date de fin";
                model.intervention = db.Intervention.ToList();
                model.Mecanicien = db.Mecanicien.ToList();
                model.Vehicule = db.Vehicule.ToList();
                model.marque = db.MarqueVehicule.ToList();
                model.detatilIntervention = db.DetatilIntervention.ToList();
                model.piece = db.Piece.ToList();
                model.typeIntervention = db.TypeIntervention.ToList();
                model.IdMeca = IdMecanicien;
                model.inter = inter;
                model.IdPiece = IdPiece;

                return View("Index", model);
            }
            else
            {
                Intervention intervention = new Intervention
                {
                    LibelleIdIntervention = inter.LibelleIdIntervention,
                    DescriptionIdIntervention = inter.DescriptionIdIntervention,
                    IdTypeIntervention = inter.IdTypeIntervention,
                    IdVehicule = inter.IdVehicule,
                    DateDebut = inter.DateDebut,
                    DateFin = inter.DateFin,
                    Supprimer = false,
                    DateSupprimer = DateTime.Now,
                };
                db.Intervention.Add(inter);
            
                DetatilIntervention detail = new DetatilIntervention();
                foreach (var i in IdPiece)
                {
                    var di = db.Piece.Single(p => p.IdPiece == i);
                    detail.IdPiece = di.IdPiece;
                    detail.IdIntervention = inter.IdIntervention;
                    db.DetatilIntervention.Add(detail);
                }

                MecaIntervention tech = new MecaIntervention();
                var dip = db.Mecanicien.Single(p => p.IdMecanicien == IdMecanicien);
                tech.IdMecanicien = dip.IdMecanicien;
                tech.IdIntervention = inter.IdIntervention;
                db.MecaIntervention.Add(tech);

                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Création de l'intervention " + inter.LibelleIdIntervention;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);

                db.SaveChanges();

                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Editer(Intervention tmp1, List<int> IdPiece, List<int> IdTechnicien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tmp1).State = EntityState.Modified;

                var past = db.DetatilIntervention.Where(p => p.IdIntervention == tmp1.IdIntervention);
                foreach(var i in past)
                {
                    db.DetatilIntervention.Remove(i);
                }
                DetatilIntervention detail = new DetatilIntervention();
                foreach (var i in IdPiece)
                {
                    var dip = db.Piece.Single(p => p.IdPiece == i);
                    detail.IdPiece = dip.IdPiece;
                    detail.IdIntervention = tmp1.IdIntervention;
                    db.DetatilIntervention.Add(detail);
                }

                var past1 = db.MecaIntervention.Where(p => p.IdIntervention == tmp1.IdIntervention);
                foreach (var i in past)
                {
                    db.DetatilIntervention.Remove(i);
                }
                MecaIntervention detail1 = new MecaIntervention();
                foreach (var i in IdTechnicien)
                {
                    var dip = db.Mecanicien.Single(p => p.IdMecanicien == i);
                    detail1.IdMecanicien = dip.IdMecanicien;
                    detail1.IdIntervention = tmp1.IdIntervention;
                    db.MecaIntervention.Add(detail1);
                }

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Modification de " + tmp1.LibelleIdIntervention;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            Intervention inter = db.Intervention.Find(id);

            if (inter.Supprimer == true)
            {
                inter.Supprimer = false;
                inter.DateSupprimer = DateTime.Now;
                db.Entry(inter).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Restauration du " + inter.LibelleIdIntervention;
                db.Action.Add(nvelActions);
            }
            else
            {
                inter.Supprimer = true;
                inter.DateSupprimer = DateTime.Now;
                db.Entry(inter).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Suppression du " + inter.LibelleIdIntervention;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportListC()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            DAL.Action nvelActions = new DAL.Action();
            var c =
                db.Connexion.ToList()
                    .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            var t = c.Last();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Intervention/CRInter.rpt")));
            var e = db.Intervention.Select(p => new
            {
                Libelle = p.LibelleIdIntervention == null ? " " : p.LibelleIdIntervention,
                Vehicule = p.Vehicule.MarqueVehicule.LibelleMarque+" "+p.Vehicule.NomVehicule+" "+p.Vehicule.Immatriculation == null ? " " : p.Vehicule.MarqueVehicule.LibelleMarque + " " + p.Vehicule.NomVehicule + " " + p.Vehicule.Immatriculation,
                Mecanicien = db.MecaIntervention.FirstOrDefault(j => j.IdIntervention == p.IdIntervention).Mecanicien.NomMecanicien+" "+ db.MecaIntervention.FirstOrDefault(j => j.IdIntervention == p.IdIntervention).Mecanicien.PrenomMecanicien == null ? " " : db.MecaIntervention.FirstOrDefault(j => j.IdIntervention == p.IdIntervention).Mecanicien.NomMecanicien + " " + db.MecaIntervention.FirstOrDefault(j => j.IdIntervention == p.IdIntervention).Mecanicien.PrenomMecanicien,
                DateDeb = p.DateDebut ?? new DateTime(),
                DateFin = p.DateFin ?? new DateTime(),
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser == null ? " " : t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des Interventions par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeVéhicule.pdf");
        }
    }
}