using APP.Areas.Administraton.Models;
using APP.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace APP.Areas.Administraton.Controllers
{
    public class AdminController : Controller
    {
        public static string pass = "phantomPanda";
        public static string monmail = "roger24Kt@gmail.com";
        private KontrolParkEntities db = new KontrolParkEntities();
        private AdminViewModel model = new AdminViewModel();

        // GET: Administration/Admin
        public ActionResult Index()
        {
            model.utilisateur = db.Utilisateur.ToList();
            model.UtilisateursActifs = new List<Utilisateur>();
            model.profil = db.Profil.ToList();
            model.droit = db.Droit.ToList();
            model.avoirDroit = db.AvoirDroit.ToList();
            model.connexion = db.Connexion.ToList();
            model.action = db.Action.ToList();
            model.U = new Utilisateur
            {
                DateNaissanceUser = null,
                DateEmbaucheUser = null
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

        public ActionResult EnregisterUser(Utilisateur user)
        {
            if (user != null)
            {
                if (!model.UtilisateurExiste(user))
                {
                    user.EmailUser = user.EmailUser.Trim();
                    user.TelUser = user.TelUser.Trim();
                    user.DateCreationCompteUser = DateTime.Now;
                    user.DateModificationPass = DateTime.Now;
                    user.MotDePasse = Crypte.Crypter(user.MotDePasse);
                    user.EnActivite = true;
                    user.DateDesactivation = DateTime.Now;
                    model.utilisateur = db.Utilisateur.ToList();
                    //user.IdUser = model.utilisateur.Count == 0 ? 1 : model.utilisateur.Max(p => p.IdProfil) + 1;
                    db.Utilisateur.Add(user);
                    db.SaveChanges();
                    try
                    {
                        HttpCookie aCookie = Request.Cookies["ident"];
                        DAL.Action nvelActions = new DAL.Action();
                        var c =
                            db.Connexion.ToList()
                                .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                        var t = c.Last();
                        nvelActions.HeureAction = DateTime.UtcNow;
                        nvelActions.IdConnexion = t.IdConnexion;
                        nvelActions.LibelleAction = " CREATION D'UN NOUVEL UTILISATEUR" + user.Identifiant;
                        db.Action.Add(nvelActions);
                    }
                    catch (Exception)
                    {
                        //IG
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }

                    SmtpClient client = new SmtpClient();
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    // setup Smtp authentication
                    NetworkCredential credentials =
                        new NetworkCredential(monmail, pass);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;


                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(monmail);
                    msg.To.Add(new MailAddress(user.EmailUser));
                    msg.Subject = "Kontrol ParK || Création de Compte";
                    msg.IsBodyHtml = true;
                    msg.Body = string.Format("<html><head></head><body>" + "<p>" +
                                             "<b>Bonjour </b>" + user.Identifiant + "</p>" +
                                             "<p>" +
                                             "COMPTE CREE AVEC SUCCES! <br/><hr/>" +
                                             "=======================" +
                                             "Mail : " + user.EmailUser + "<br/>" +
                                             "Identifiant : " + user.Identifiant + "<br/>" +
                                             "Profil : " + user.Profil.LibelleProfil + "<br/>" +
                                             "Mot de Passe : " + user.TelUser + "<br/>" +
                                             "=======================" +
                                             "</p>" +
                                             "</body>");

                    try
                    {
                        client.Send(msg);

                    }
                    catch (Exception)
                    {
                        // ignored
                    }


                    model.profil = db.Profil.ToList();
                    model.utilisateur = db.Utilisateur.ToList();
                    model.connexion = db.Connexion.ToList();
                    model.droit = db.Droit.ToList();
                    model.UtilisateursActifs = new List<Utilisateur>();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult EnregisterUserPlus(Utilisateur user)
        {
            foreach (var item in db.Utilisateur.ToList())
            {
                if (user.NumCNIUser == item.NumCNIUser)
                {
                    ViewData["CompareAge"] = "Ce numero de Carte Nationnal d'Identité est déjà utilisé";
                    model.utilisateur = db.Utilisateur.ToList();
                    model.UtilisateursActifs = new List<Utilisateur>();
                    model.profil = db.Profil.ToList();
                    model.droit = db.Droit.ToList();
                    model.avoirDroit = db.AvoirDroit.ToList();
                    model.connexion = db.Connexion.ToList();
                    model.action = db.Action.ToList();
                    model.utilisateur = db.Utilisateur.ToList();
                    model.U = user;

                    return View("Index", model);
                }
            }

            DateTime date1 = user.DateNaissanceUser ?? new DateTime();
            DateTime date2 = user.DateEmbaucheUser ?? new DateTime();

            int diff = Math.Abs(date1.Month - DateTime.Now.Month);
            diff += Math.Abs((date1.Year - DateTime.Now.Year) * 12);

            int diff1 = Math.Abs(date2.Month - date1.Month);
            diff1 += Math.Abs((date2.Year - date1.Year) * 12);

            if (diff < 216)
            {
                ViewData["CompareAge"] = "L'age du mécanicien doit être inférieur à 18ans";
                model.utilisateur = db.Utilisateur.ToList();
                model.UtilisateursActifs = new List<Utilisateur>();
                model.profil = db.Profil.ToList();
                model.droit = db.Droit.ToList();
                model.avoirDroit = db.AvoirDroit.ToList();
                model.connexion = db.Connexion.ToList();
                model.action = db.Action.ToList();
                model.utilisateur = db.Utilisateur.ToList();
                model.U = user;

                return View("Index", model);
            }
            else
            {
                if (diff1 < 216)
                {
                    ViewData["CompareAge"] = "Veuillez vérifier la date d'embauche";
                    model.utilisateur = db.Utilisateur.ToList();
                    model.UtilisateursActifs = new List<Utilisateur>();
                    model.profil = db.Profil.ToList();
                    model.droit = db.Droit.ToList();
                    model.avoirDroit = db.AvoirDroit.ToList();
                    model.connexion = db.Connexion.ToList();
                    model.action = db.Action.ToList();
                    model.utilisateur = db.Utilisateur.ToList();
                    model.U = user;

                    return View("Index", model);
                }
                else if (user.DateEmbaucheUser > DateTime.Now)
                {
                    ViewData["CompareAge"] = "Veuillez vérifier la date d'embauche";
                    model.utilisateur = db.Utilisateur.ToList();
                    model.UtilisateursActifs = new List<Utilisateur>();
                    model.profil = db.Profil.ToList();
                    model.droit = db.Droit.ToList();
                    model.avoirDroit = db.AvoirDroit.ToList();
                    model.connexion = db.Connexion.ToList();
                    model.action = db.Action.ToList();
                    model.utilisateur = db.Utilisateur.ToList();
                    model.U = user;

                    return View("Index", model);
                }
                else
                {
                    user.EmailUser = user.EmailUser.Trim();
                    user.TelUser = user.TelUser.Trim();
                    user.DateCreationCompteUser = DateTime.Now;
                    user.DateModificationPass = DateTime.Now;
                    user.MotDePasse = Crypte.Crypter(user.MotDePasse);
                    user.EnActivite = true;
                    user.DateDesactivation = DateTime.Now;
                    model.utilisateur = db.Utilisateur.ToList();
                    db.Utilisateur.Add(user);
                    db.SaveChanges();
                    try
                    {
                        HttpCookie aCookie = Request.Cookies["ident"];
                        DAL.Action nvelActions = new DAL.Action();
                        var c =
                            db.Connexion.ToList()
                                .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                        var t = c.Last();
                        nvelActions.HeureAction = DateTime.UtcNow;
                        nvelActions.IdConnexion = t.IdConnexion;
                        nvelActions.LibelleAction = " CREATION D'UN NOUVEL UTILISATEUR" + user.Identifiant;
                        db.Action.Add(nvelActions);
                    }
                    catch (Exception)
                    {
                        //IG
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                    SmtpClient client = new SmtpClient();
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    // setup Smtp authentication
                    NetworkCredential credentials =
                        new NetworkCredential(monmail, pass);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(monmail);
                    msg.To.Add(new MailAddress(user.EmailUser));
                    msg.Subject = "Kontrol ParK || Création de Compte";
                    msg.IsBodyHtml = true;
                    msg.Body = string.Format("<html><head></head><body>" + "<p>" +
                                                "<b>Bonjour </b>" + user.Identifiant + "</p>" +
                                                "<p>" +
                                                "COMPTE CREE AVEC SUCCES! <br/><hr/>" +
                                                "=======================" +
                                                "Mail : " + user.EmailUser + "<br/>" +
                                                "Identifiant : " + user.Identifiant + "<br/>" +
                                                "Profil : " + user.Profil.LibelleProfil + "<br/>" +
                                                "Mot de Passe : " + user.TelUser + "<br/>" +
                                                "=======================" +
                                                "</p>" +
                                                "</body>");

                    try
                    {
                        client.Send(msg);

                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    model.profil = db.Profil.ToList();
                    model.utilisateur = db.Utilisateur.ToList();
                    model.connexion = db.Connexion.ToList();
                    model.droit = db.Droit.ToList();
                    model.UtilisateursActifs = new List<Utilisateur>();

                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(int id)
        {
            Utilisateur tmpUtilisateur = db.Utilisateur.Single(p => p.IdUser == id);
            HttpCookie aCookie = Request.Cookies["ident"];
            if (tmpUtilisateur.EnActivite == true)
            {
                tmpUtilisateur.EnActivite = false;
                model.utilisateur = db.Utilisateur.ToList();
                DAL.Action nvelActions = new DAL.Action();
                var c =
                    db.Connexion.ToList()
                        .Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = " Désactivation de l'Utilisateur" + tmpUtilisateur.Identifiant;
                db.Action.Add(nvelActions);
            }
            else
            {
                tmpUtilisateur.EnActivite = true;
                model.utilisateur = db.Utilisateur.ToList();
                DAL.Action nvelActions = new DAL.Action();
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvelActions.HeureAction = DateTime.UtcNow;
                nvelActions.IdConnexion = t.IdConnexion;
                nvelActions.LibelleAction = " Réactivation de l'Utilisateur" + tmpUtilisateur.Identifiant;
                db.Action.Add(nvelActions);
            }

            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public JsonResult Verif(string id)
        {

            Utilisateur tmpUtilisateur = db.Utilisateur.Single(p => p.Identifiant == id);

            if (tmpUtilisateur == null)
            {

                return Json(new { Verification = "OK" });
            }
            else
            {
                return Json(new { Verification = "Echec" });
            }

        }
        [HttpPost]
        public ActionResult Edit(Utilisateur tmp1)
        {
            Utilisateur tmp = db.Utilisateur.FirstOrDefault(p => p.IdUser == tmp1.IdUser);
            if (tmp != null)
            {
                string avt = tmp.Profil.LibelleProfil;
                tmp.IdProfil = tmp1.IdProfil;
                db.Utilisateur.AddOrUpdate(tmp);

                DAL.Action nvellActions = new DAL.Action();
                HttpCookie aCookie = Request.Cookies["ident"];
                var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
                var t = c.Last();
                nvellActions.HeureAction = DateTime.UtcNow;
                nvellActions.LibelleAction = "Changement de Profil de " + avt + " à " + tmp.Profil.LibelleProfil + " de l'utilisateur " + tmp.Identifiant;
                nvellActions.IdConnexion = t.IdConnexion;
                db.Action.Add(nvellActions);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}