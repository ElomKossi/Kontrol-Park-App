using APP.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace APP.Controllers
{
    public class UtilisateurController : Controller
    {
        public static string pass = "phantomPanda";
        public static string monmail = "Roger24Kt@gmail.com";
        private KontrolParkEntities db = new KontrolParkEntities();
        private UserViewModel model = new UserViewModel();

        // GET: /Utilisateur/
        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            var id = Server.HtmlEncode(aCookie.Value);

            //var c = db.Utilisateur.Single(p => p.Identifiant == Server.HtmlEncode(aCookie.Value));

            model.User = db.Utilisateur.Single(p => p.Identifiant == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Parametre(Utilisateur J)
        {
            var tmp = db.Utilisateur.Single(p => p.IdUser == J.IdUser);
            tmp.Identifiant = J.Identifiant;
            tmp.EmailUser = J.EmailUser;
            tmp.TelUser = J.TelUser;
            db.Utilisateur.AddOrUpdate(tmp);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Motdepasse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Motdepasse(string motdepasse, string motdepasse1, string confirm)
        {
            Utilisateur courant = db.Utilisateur.Single(p => p.Identifiant == HttpContext.User.Identity.Name);
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
            msg.To.Add(new MailAddress(courant.EmailUser));

            if (courant.MotDePasse == Crypte.Crypter(motdepasse))
            {
                if (motdepasse1 == confirm)
                {
                    courant.MotDePasse = Crypte.Crypter(motdepasse1);

                    msg.Subject = "IDS|| GESTCOM Modification du Mot de passe";
                    msg.IsBodyHtml = true;
                    msg.Body = string.Format("<html><head></head><body>" + "<p>" +
                                                "<b>Bonjour </b>" + courant.Identifiant + "</p>" +
                                                "<p>" +
                                                "VOTRE MOT DE PASSE A ETE CHANGE! <br/><hr/>" +
                                                "=======================" +
                                                "Mail : " + courant.EmailUser + "<br/>" +
                                                "Identifiant : " + courant.Identifiant + "<br/>" +
                                                "Profil : " + courant.Profil.LibelleProfil + "<br/>" +
                                                "Ancien Mot de Passe : " + motdepasse + "<br/>" +
                                                "Nouveau Mot de Passe : " + motdepasse1 + "<br/>" +
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

                }
            }
            else
            {
                msg.Subject = "IDS|| GESTCOM Modification du Mot de passe";
                msg.IsBodyHtml = true;
                msg.Body = string.Format("<html><head></head><body>" + "<p>" +
                                            "<b>Bonjour </b>" + courant.Identifiant + "</p>" +
                                            "<p>" +
                                            "TENTATIVE DE MODIFCATION DE VOTRE MOT DE PASSE ! <br/><hr/>" +
                                            "=======================" +

                                            " UNE TENTATIVE DE MODIFICATION DE VOTRE MOT DE PASSE A ETE DETECTE SI VOUS N'ETES PAS L'AUTEUR CONTACTEZ VOTRE ADMINISTRATEUR" +
                                            " Mot de Passe entré : " + motdepasse1 + "<br/>" +
                                            "Votre Mot de passe reste Inchangé" +
                                            "Pour des Raison de sécurité votre compte est désactivé contacté l'administrateur" +
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
            }
            return View();
        }
    }
}