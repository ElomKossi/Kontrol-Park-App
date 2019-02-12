using APP.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace APP.Controllers
{
    public class LoginController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();
        private UserViewModel model = new UserViewModel();
        public int Tentative = 0;

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string ReturnUrl = "")
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("~/Home/Index");
            List<Connexion> list = new List<Connexion>();
            foreach (var connexion in db.Connexion)
                list.Add(connexion);
            model.Connexions = list;
            ViewBag.r = ReturnUrl;

            return View();
        }

        public ActionResult Block()
        {
            return View("block");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Connexion(Utilisateur logUtilisateur, string ReturnUrl)
        {
            Tentative++;
            if (Tentative >= 4)
            {
                return RedirectToAction("Block");
            }
            try
            {
                db.Utilisateur.Single(p => p.EmailUser == logUtilisateur.EmailUser).Tentative++;
                db.SaveChanges();
            }
            catch (Exception x)
            {
                ViewBag.erreur = x.Message;
            }
            logUtilisateur.MotDePasse = Crypte.Crypter(logUtilisateur.MotDePasse);

            List<Profil> profils = db.Profil.ToList();
            try
            {
                model.User = db.Utilisateur.FirstOrDefault(u => u.EmailUser == logUtilisateur.EmailUser && u.MotDePasse == logUtilisateur.MotDePasse);
            }
            catch (Exception)
            {
                ViewBag.erreur = "Identifiant ou Mot de passe incorrect";
                return View("Index");
            }
            if (model.User == null)
            {
                ViewBag.erreur = "Identifiant ou Mot de passe incorrect";
                return View("Index");
            }
            else
            {
                if (model.User.EnActivite == true)
                {
                    // FormsAuthentication.SetAuthCookie(model.User.NomUser,false);
                    Response.Cookies["username"].Value = model.User.NomUser;
                    Response.Cookies["username"].Expires = DateTime.Now.AddDays(1);

                    Response.Cookies["usersurname"].Value = model.User.PrenomUser;
                    Response.Cookies["usersurname"].Expires = DateTime.Now.AddDays(1);

                    Response.Cookies["ident"].Value = model.User.Identifiant;
                    Response.Cookies["ident"].Expires = DateTime.Now.AddDays(1);

                    Response.Cookies["profil"].Value = model.User.Profil.LibelleProfil;
                    Response.Cookies["profil"].Expires = DateTime.Now.AddDays(1);

                    FormsAuthentication.SetAuthCookie(model.User.Identifiant, false);
                    if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        return Redirect(ReturnUrl);
                    Connexion connexion = new Connexion
                    {
                        IdUser = model.User.IdUser,
                        DebutConnexion = DateTime.Now
                    };
                    Debug.Assert(Request.UserHostAddress != null, "Request.UserHostAddress != null");
                    connexion.AdresseIP = Request.ServerVariables["REMOTE_ADDR"];
                    connexion.Navigateur = Request.Browser.Browser;
                    connexion.Machine = Server.MachineName;
                    connexion.AdresseMac = Request.UserHostName;
                    connexion.SystemeOS = "";
                    db.Connexion.Add(connexion);
                    db.SaveChanges();

                    DAL.Action nvellActions = new DAL.Action();
                    HttpCookie fCookie = Request.Cookies["ident"];
                    fCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(fCookie);
                    var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(fCookie.Value) && p.FinConnexion == null);
                    var t = c.Last();
                    nvellActions.HeureAction = DateTime.UtcNow;
                    nvellActions.LibelleAction = "Connexion ";
                    nvellActions.IdConnexion = t.IdConnexion;
                    db.Action.Add(nvellActions);
                    db.SaveChanges();

                    if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        return Redirect(ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.erreur = " Compte désactivé veuillez contacter l'Administration";
                    return View("Index");
                }
            }
        }

        [Authorize]
        public ActionResult Deconnexion()
        {
            HttpCookie aCookie = Request.Cookies["ident"];
            var c = db.Connexion.ToList().Where(p => p.Utilisateur.Identifiant == Server.HtmlEncode(aCookie.Value) && p.FinConnexion == null);
            try
            {
                var t = c.Last();
                t.FinConnexion = DateTime.Now;
                db.Connexion.AddOrUpdate(t);
                db.SaveChanges();

                //DAL.Action nvellActions = new DAL.Action();
                //nvellActions.HeureAction = DateTime.UtcNow;
                //nvellActions.LibelleAction = "Déconnexion";
                //nvellActions.IdConnexion = t.IdConnexion;
                //db.Action.Add(nvellActions);
                //db.SaveChanges();
                
                FormsAuthentication.SignOut();
            }
            catch (Exception)
            {
                FormsAuthentication.SignOut();
            }
            return Redirect("~/");
        }
    }
}