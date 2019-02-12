using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using APP.Models;

namespace APP.Controllers
{
    public class HomeController : Controller
    {
        public KontrolParkEntities db = new KontrolParkEntities();
        public ViewsModels model = new ViewsModels();

        public ActionResult Index()
        {
            model.conducteur = db.Conducteur.ToList();
            model.mission = db.Mission.ToList();
            model.vehicule = db.Vehicule.ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}