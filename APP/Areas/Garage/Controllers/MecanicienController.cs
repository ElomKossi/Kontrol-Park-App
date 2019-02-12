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
    public class MecanicienController : Controller
    {
        // GET: Garage/Technicien
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.Mecanicien = db.Mecanicien.ToList();
            model.Meca = new DAL.Mecanicien
            {
                DateNaissanceMecanicien = null,
                DateEmbaucheMecanicien = null
            };

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
        public ActionResult Ajouter(Mecanicien tech)
        {
            foreach (var item in db.Mecanicien.ToList())
            {
                if (tech.NumCNIMecanicien == item.NumCNIMecanicien)
                {
                    ViewData["CompareAge"] = "Ce numero de Carte Nationnal d'Identité est déjà utilisé";
                    model.Mecanicien = db.Mecanicien.ToList();
                    model.Meca = new DAL.Mecanicien
                    {
                        DateNaissanceMecanicien = null,
                        DateEmbaucheMecanicien = null
                    };

                    return View("Index", model);
                }
            }

            DateTime date1 = tech.DateNaissanceMecanicien ?? new DateTime();
            DateTime date2 = tech.DateEmbaucheMecanicien ?? new DateTime();

            int diff = Math.Abs(date1.Month - DateTime.Now.Month);
            diff += Math.Abs((date1.Year - DateTime.Now.Year) * 12);

            int diff1 = Math.Abs(date2.Month - date1.Month);
            diff1 += Math.Abs((date2.Year - date1.Year) * 12);

            if (diff < 216)
            {
                ViewData["CompareAge"] = "L'age du mécanicien doit être inférieur à 18ans";
                model.Mecanicien = db.Mecanicien.ToList();
                model.Meca = new DAL.Mecanicien
                {
                    DateNaissanceMecanicien = null,
                    DateEmbaucheMecanicien = null
                };

                return View("Index", model);
            }
            else
            {
                if (diff1 < 216)
                {
                    ViewData["CompareAge"] = "Veuillez vérifier la date d'embauche";
                    model.Mecanicien = db.Mecanicien.ToList();
                    model.Meca = new DAL.Mecanicien
                    {
                        DateNaissanceMecanicien = null,
                        DateEmbaucheMecanicien = null
                    };

                    return View("Index", model);
                }
                else if (tech.DateEmbaucheMecanicien > DateTime.Now)
                {
                    ViewData["CompareAge"] = "Veuillez vérifier la date d'embauche";
                    model.Mecanicien = db.Mecanicien.ToList();
                    model.Meca = new DAL.Mecanicien
                    {
                        DateNaissanceMecanicien = null,
                        DateEmbaucheMecanicien = null
                    };

                    return View("Index", model);
                }
                else
                {
                    Mecanicien mecanicien = new Mecanicien
                    {
                        NumCNIMecanicien = tech.NumCNIMecanicien,
                        NomMecanicien = tech.NomMecanicien,
                        PrenomMecanicien = tech.PrenomMecanicien,
                        DateNaissanceMecanicien = tech.DateNaissanceMecanicien,
                        LieuxNaissanceMecanicien = tech.LieuxNaissanceMecanicien,
                        SexeMecanicien = tech.SexeMecanicien,
                        AdresseMecanicien = tech.AdresseMecanicien,
                        EmailMecanicien = tech.EmailMecanicien,
                        TelMecanicien = tech.TelMecanicien,
                        DateEmbaucheMecanicien = tech.DateEmbaucheMecanicien,
                        EnActivite = true,
                        DateDesactivation = DateTime.Now
                    };
                    db.Mecanicien.Add(mecanicien);
                    db.SaveChanges();

                    HttpCookie aCookie = Request.Cookies["ident"];
                    DAL.Action nvelActions = new DAL.Action();
                    var c =
                        db.Connexion.ToList()
                            .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                    var t = c.Last();
                    nvelActions.HeureAction = DateTime.UtcNow;
                    nvelActions.IdConnexion = t.IdConnexion;
                    nvelActions.LibelleAction = "Ajout du Mecanicien " + tech.NomMecanicien + " " + tech.PrenomMecanicien;
                    db.Action.Add(nvelActions);

                    db.SaveChanges();

                    return RedirectToAction("index");
                }
            }
        }

        [HttpPost]
        public ActionResult Editer(Mecanicien tmp1)
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
                nvelActions.LibelleAction = "Modification du Mecanicien " + tmp1.NomMecanicien + " " + tmp1.PrenomMecanicien;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            Mecanicien tech = db.Mecanicien.Find(id);

            if (tech.EnActivite == true)
            {
                tech.EnActivite = false;
                tech.DateDesactivation = DateTime.Now;
                db.Entry(tech).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "REmise en activité du Mecanicien " + tech.NumCNIMecanicien + " " + tech.PrenomMecanicien;
                db.Action.Add(nvelActions);
            }
            else
            {
                tech.EnActivite = true;
                tech.DateDesactivation = DateTime.Now;
                db.Entry(tech).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Mise hors activité du Mecanicien " + tech.NomMecanicien + " " + tech.PrenomMecanicien;
                db.Action.Add(nvelActions);
            }

            db.SaveChangesAsync();
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
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Mecanicien/CRMeca.rpt")));
            var e = db.Mecanicien.Select(p => new
            {
                NumCNI = p.NumCNIMecanicien == null ? " " : p.NumCNIMecanicien,
                NomPrenoms = p.NomMecanicien + " " + p.PrenomMecanicien == null ? " " : p.NomMecanicien + " " + p.PrenomMecanicien,
                Sexe = p.SexeMecanicien == null ? " " : p.SexeMecanicien,
                //DateNaiss = (p.DateNaissanceMecanicien).ToString() == null ? "" : p.DateNaissanceMecanicien.ToString(),
                DateNaiss = p.DateNaissanceMecanicien ?? new DateTime(),
                Adresse = p.AdresseMecanicien == null ? " " : p.AdresseMecanicien,
                Tel = p.TelMecanicien == null ? " " : p.TelMecanicien,
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser == null ? " " : t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des Mécaniciens par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeVéhicule.pdf");
        }
    }
}