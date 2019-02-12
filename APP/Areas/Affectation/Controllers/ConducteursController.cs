using APP.Areas.Affectation.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace APP.Areas.Affectation.Controllers
{
    public class ConducteursController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private AllViewsModel model = new AllViewsModel();

        public ActionResult Index()
        {
            model.conducteur = db.Conducteur.ToList();
            model.categoriePermis = db.CategoriePermis.ToList();
            model.conducteurPermis = db.ConducteurPermis.ToList();
            model.conducteurM = new DAL.Conducteur
            {
                DateNaissanceConducteur = null,
                DateEmbaucheConducteur = null,
                DateExpire = null
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
        public ActionResult Ajouter(Conducteur cond, List<int> IdCategoriePermis)
        {
            foreach(var item in db.Conducteur.ToList())
            {
                if(cond.NumCNIConducteur == item.NumCNIConducteur)
                {
                    
                    ViewData["CompareAge"] = "Ce numero de Carte Nationnal d'Identité est déjà utilisé";
                    model.conducteur = db.Conducteur.ToList();
                    model.categoriePermis = db.CategoriePermis.ToList();
                    model.conducteurPermis = db.ConducteurPermis.ToList();
                    model.conducteurM = cond;
                    model.IdCondPer = IdCategoriePermis;

                    return View("Index", model);
                }
                else if (cond.NumPermis == item.NumPermis)
                {
                    ViewData["CompareAge"] = "Ce numero de permis est déjà utilisé";
                    model.conducteur = db.Conducteur.ToList();
                    model.categoriePermis = db.CategoriePermis.ToList();
                    model.conducteurPermis = db.ConducteurPermis.ToList();
                    model.conducteurM = cond;
                    model.IdCondPer = IdCategoriePermis;

                    return View("Index", model);
                }
            }

            DateTime date1 = cond.DateNaissanceConducteur ?? new DateTime();
            DateTime date2 = cond.DateEmbaucheConducteur ?? new DateTime();
            DateTime date3 = cond.DateExpire ?? new DateTime();

            int diff = Math.Abs(date1.Month - DateTime.Now.Month);
            diff += Math.Abs((date1.Year - DateTime.Now.Year) * 12);

            int diff1 = Math.Abs(date2.Month - date1.Month);
            diff1 += Math.Abs((date2.Year - date1.Year) * 12);

            if (diff < 216)
            {
                ViewData["CompareAge"] = "L'age du conducteur doit être inférieur à 18ans";
                model.conducteur = db.Conducteur.ToList();
                model.categoriePermis = db.CategoriePermis.ToList();
                model.conducteurPermis = db.ConducteurPermis.ToList();
                model.conducteurM = cond;
                model.IdCondPer = IdCategoriePermis;

                return View("Index", model);
            }
            else
            {
                if (diff1 < 216)
                {
                    ViewData["CompareAge"] = "Veuillez vérifier la date d'embauche";
                    model.conducteur = db.Conducteur.ToList();
                    model.categoriePermis = db.CategoriePermis.ToList();
                    model.conducteurPermis = db.ConducteurPermis.ToList();
                    model.conducteurM = cond;
                    model.IdCondPer = IdCategoriePermis;

                    return View("Index", model);
                }
                else if (cond.DateEmbaucheConducteur > DateTime.Now)
                {
                    ViewData["CompareAge"] = "Veuillez vérifier la date d'embauche";
                    model.conducteur = db.Conducteur.ToList();
                    model.categoriePermis = db.CategoriePermis.ToList();
                    model.conducteurPermis = db.ConducteurPermis.ToList();
                    model.conducteurM = cond;
                    model.IdCondPer = IdCategoriePermis;

                    return View("Index", model);
                }
                else if(cond.DateExpire < DateTime.Now)
                {
                    ViewData["CompareAge"] = "Le permis n'est plus valide";
                    model.conducteur = db.Conducteur.ToList();
                    model.categoriePermis = db.CategoriePermis.ToList();
                    model.conducteurPermis = db.ConducteurPermis.ToList();
                    model.conducteurM = cond;
                    model.IdCondPer = IdCategoriePermis;

                    return View("Index", model);
                }
                else
                {
                    Conducteur conducteur = new Conducteur
                    {
                        NumCNIConducteur = cond.NumCNIConducteur,
                        NomConducteur = cond.NomConducteur,
                        PrenomConducteur = cond.PrenomConducteur,
                        DateNaissanceConducteur = cond.DateNaissanceConducteur,
                        LieuxNaissanceConducteur = cond.LieuxNaissanceConducteur,
                        SexeConducteur = cond.SexeConducteur,
                        AdresseConducteur = cond.AdresseConducteur,
                        EmailConducteur = cond.EmailConducteur,
                        TelConducteur = cond.TelConducteur,
                        DateEmbaucheConducteur = cond.DateEmbaucheConducteur,
                        NumPermis = cond.NumPermis,
                        DateExpire = cond.DateExpire,
                        EnActivite = true,
                        EnMission = false,
                        DateDesactivation = DateTime.Now,
                    };
                    db.Conducteur.Add(conducteur);
                    //db.SaveChanges();

                    foreach (var i in IdCategoriePermis)
                    {
                        ConducteurPermis condP = new ConducteurPermis
                        {
                            IdCategoriePermis = i,
                            IdConducteur = conducteur.IdConducteur,
                            Supprimer = false,
                            DateSupprimer = DateTime.Now,
                        };
                        db.ConducteurPermis.Add(condP);
                    }

                    HttpCookie aCookie = Request.Cookies["ident"];
                    DAL.Action nvelActions = new DAL.Action();
                    var c =
                        db.Connexion.ToList()
                            .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                    var t = c.Last();
                    nvelActions.HeureAction = DateTime.UtcNow;
                    nvelActions.IdConnexion = t.IdConnexion;
                    nvelActions.LibelleAction = "Ajout du conducteur " + cond.NomConducteur + " " + cond.PrenomConducteur;
                    db.Action.Add(nvelActions);

                    db.SaveChanges();

                    return RedirectToAction("index");
                }
            }
        }

        [HttpPost]
        public ActionResult Editer(Conducteur tmp1, List<int> IdCategoriePermis)
        {
            if (ModelState.IsValid)
            {
                Conducteur con = db.Conducteur.Find(tmp1.IdConducteur);
                tmp1.EnActivite = con.EnActivite;
                tmp1.EnMission = con.EnMission;
                tmp1.DateDesactivation = con.DateDesactivation;

                db.Conducteur.AddOrUpdate(tmp1);
                //db.Entry(tmp1).State = EntityState.Modified;

                var cond = db.ConducteurPermis.Where(p => p.IdConducteur == tmp1.IdConducteur);
                foreach(var i in cond)
                {
                    db.ConducteurPermis.Remove(i);
                }

                foreach (var i in IdCategoriePermis)
                {
                    ConducteurPermis condP = new ConducteurPermis
                    {
                        IdCategoriePermis = i,
                        IdConducteur = tmp1.IdConducteur,
                        Supprimer = false,
                        DateSupprimer = DateTime.Now,
                    };
                    db.ConducteurPermis.Add(condP);
                }

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Modification du conducteur " + tmp1.NomConducteur + " " + tmp1.PrenomConducteur;
                db.Action.Add(nvelActions);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Etat(int id)
        {
            Conducteur cond = db.Conducteur.Find(id);

            if (cond.EnActivite == true)
            {
                cond.EnActivite = false;
                cond.DateDesactivation = DateTime.Now;
                db.Entry(cond).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Remise en activité du conducteur " + cond.NomConducteur + " " + cond.PrenomConducteur;
                db.Action.Add(nvelActions);
            }
            else
            {
                cond.EnActivite = true;
                cond.DateDesactivation = DateTime.Now;
                db.Entry(cond).State = EntityState.Modified;

                HttpCookie aCookie = Request.Cookies["ident"];
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = "Mise hors activité du conducteur "+ cond.NomConducteur + " " + cond.PrenomConducteur;
                db.Action.Add(nvelActions);
            }

            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Mission/Conducteurs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conducteur conducteur = await db.Conducteur.FindAsync(id);
            if (conducteur == null)
            {
                return HttpNotFound();
            }
            return View(conducteur);
        }

        // GET: Mission/Conducteurs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conducteur conducteur = await db.Conducteur.FindAsync(id);
            if (conducteur == null)
            {
                return HttpNotFound();
            }
            return View(conducteur);
        }

        // POST: Mission/Conducteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Conducteur conducteur = await db.Conducteur.FindAsync(id);
            conducteur.EnActivite = false;
            conducteur.DateDesactivation = DateTime.Now;
            conducteur.EnMission = false;
            db.Entry(conducteur).State = EntityState.Modified;
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

        //[HttpPost]
        //public ActionResult AjouterP(ConducteurPermis condPer, int? catp)
        //{
        //    ConducteurPermis conducteurPermis = new ConducteurPermis
        //    {
        //        IdConducteur = condPer.IdConducteur,
        //        IdCategoriePermis = condPer.IdCategoriePermis,
        //        NumPermis = condPer.NumPermis,
        //        DateExpire = condPer.DateExpire,
        //        Supprimer = false,
        //    };

        //    HttpCookie aCookie = Request.Cookies["ident"];
        //    DAL.Action nvelActions = new DAL.Action();
        //    var c =
        //        db.Connexion.ToList()
        //            .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
        //    var t = c.Last();
        //    nvelActions.HeureAction = DateTime.UtcNow;
        //    nvelActions.IdConnexion = t.IdConnexion;
        //    Conducteur condu = db.Conducteur.Find(condPer.IdConducteur);
        //    nvelActions.LibelleAction = "Ajout du permis " + condPer.NumPermis + " de " + condu.NomConducteur + " " + condu.PrenomConducteur;
        //    db.Action.Add(nvelActions);

        //    db.ConducteurPermis.Add(conducteurPermis);
        //    db.SaveChanges();
        //    return RedirectToAction("index", "Conducteurs");
        //}

        //[HttpPost]
        //public ActionResult EditerP(ConducteurPermis tmp1)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tmp1).State = EntityState.Modified;

        //        HttpCookie aCookie = Request.Cookies["ident"];
        //        DAL.Action nvelActions = new DAL.Action();
        //        var c =
        //            db.Connexion.ToList()
        //                .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
        //        var t = c.Last();
        //        nvelActions.HeureAction = DateTime.UtcNow;
        //        nvelActions.IdConnexion = t.IdConnexion;
        //        nvelActions.LibelleAction = "Modification du permis " + tmp1.NumPermis + " de " + tmp1.Conducteur.NomConducteur + " " + tmp1.Conducteur.PrenomConducteur;
        //        db.Action.Add(nvelActions);

        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("index");
        //}

        //public ActionResult EtatP(int id)
        //{
        //    ConducteurPermis conducteurPermis = db.ConducteurPermis.Find(id);

        //    if (conducteurPermis.Supprimer == true)
        //    {
        //        conducteurPermis.Supprimer = false;
        //        conducteurPermis.DateSupprimer = DateTime.Now;
        //        db.Entry(conducteurPermis).State = EntityState.Modified;

        //        HttpCookie aCookie = Request.Cookies["ident"];
        //        DAL.Action nvelActions = new DAL.Action();
        //        var c =
        //            db.Connexion.ToList()
        //                .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
        //        var t = c.Last();
        //        nvelActions.HeureAction = DateTime.UtcNow;
        //        nvelActions.IdConnexion = t.IdConnexion;
        //        nvelActions.LibelleAction = "Restauration du permis " + conducteurPermis.NumPermis + " de " + conducteurPermis.Conducteur.NomConducteur + " " + conducteurPermis.Conducteur.PrenomConducteur;
        //        db.Action.Add(nvelActions);
        //    }
        //    else
        //    {
        //        conducteurPermis.Supprimer = true;
        //        conducteurPermis.DateSupprimer = DateTime.Now;
        //        db.Entry(conducteurPermis).State = EntityState.Modified;

        //        HttpCookie aCookie = Request.Cookies["ident"];
        //        DAL.Action nvelActions = new DAL.Action();
        //        var c =
        //            db.Connexion.ToList()
        //                .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
        //        var t = c.Last();
        //        nvelActions.HeureAction = DateTime.UtcNow;
        //        nvelActions.IdConnexion = t.IdConnexion;
        //        nvelActions.LibelleAction = "Suppression du permis " + conducteurPermis.NumPermis + " de " + conducteurPermis.Conducteur.NomConducteur + " " + conducteurPermis.Conducteur.PrenomConducteur;
        //        db.Action.Add(nvelActions);
        //    }

        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Modif(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Conducteur cond = db.Conducteur.Find(id);
            //List<ConducteurPermis> cond = db.ConducteurPermis.Where(p => p.IdConducteur == id).ToList();
            Conducteur cond = db.Conducteur.Find(id);
            if (cond == null)
            {
                return HttpNotFound();
            }
            return View(cond);
        }

        static string Lib(int id)
        {
            KontrolParkEntities bd = new KontrolParkEntities();
            string resultat = "";
            foreach (var c in bd.ConducteurPermis.Where(f => f.IdConducteur == id))
            {
                resultat = resultat + c.CategoriePermis.LibelleCategoriePermis + " ";
            }
            return resultat;
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
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Conducteur/CRCond.rpt")));
            var e = db.Conducteur.Select(p => new
            {
                NumCNI = p.NumCNIConducteur == null ? " " : p.NumCNIConducteur,
                NumPermi = p.NumPermis == null ? " " : p.NumPermis,
                NomPrenoms = p.NomConducteur + " " + p.PrenomConducteur == null ? " " : p.NomConducteur + " " + p.PrenomConducteur,
                Sexe = p.SexeConducteur == null ? " " : p.SexeConducteur,
                //DateNaiss = (p.DateNaissanceConducteur).ToString() == null ? "" : p.DateNaissanceConducteur.ToString(),
                DateNaiss = p.DateNaissanceConducteur ?? new DateTime(),
                Adresse = p.AdresseConducteur == null ? " " : p.AdresseConducteur,
                Tel = p.TelConducteur == null ? " " : p.TelConducteur,
                Printer = t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser
            }).ToList();
            rd.SetDataSource(e);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            nvelActions.HeureAction = DateTime.UtcNow;
            nvelActions.IdConnexion = t.IdConnexion;
            nvelActions.LibelleAction = "Impression de la liste des Conducteurs par " + t.Utilisateur.NomUser + " " + t.Utilisateur.PrenomUser;
            db.Action.Add(nvelActions);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListeVéhicule.pdf");
        }

        // POST: Administration/Adherents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modif(/*[Bind(Include = "IdAdherent,Nom,Prenom,DateNaissance,Email,Telephone,Adresse,IdPolice")] */Conducteur cond, ConducteurPermis condP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cond).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cond);
        }
    }
}