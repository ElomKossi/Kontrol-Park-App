using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace APP.Areas.Affectation.Controllers
{
    public class AffectationsController : Controller
    {
        private KontrolParkEntities db = new KontrolParkEntities();

        // GET: Affectation/Affectations
        public ActionResult Index()
        {
            var affectation = db.Affectation.Include(a => a.Conducteur).Include(a => a.Mission).Include(a => a.Vehicule);
            return View(affectation.ToList());
        }

        // GET: Affectation/Affectations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Affectation affectation = db.Affectation.Find(id);
            if (affectation == null)
            {
                return HttpNotFound();
            }
            return View(affectation);
        }

        // GET: Affectation/Affectations/Create
        public ActionResult Create()
        {
            ViewBag.IdConducteur = new SelectList(db.Conducteur, "IdConducteur", "NumCNIConducteur");
            ViewBag.IdMission = new SelectList(db.Mission, "IdMission", "LibelleMission");
            ViewBag.IdVehicule = new SelectList(db.Vehicule, "IdVehicule", "NomVehicule");
            return View();
        }

        // POST: Affectation/Affectations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAffectation,IdMission,IdVehicule,IdConducteur,DateAffectation")] DAL.Affectation affectation)
        {
            if (ModelState.IsValid)
            {
                db.Affectation.Add(affectation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConducteur = new SelectList(db.Conducteur, "IdConducteur", "NumCNIConducteur", affectation.IdConducteur);
            ViewBag.IdMission = new SelectList(db.Mission, "IdMission", "LibelleMission", affectation.IdMission);
            ViewBag.IdVehicule = new SelectList(db.Vehicule, "IdVehicule", "NomVehicule", affectation.IdVehicule);
            return View(affectation);
        }

        // GET: Affectation/Affectations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Affectation affectation = db.Affectation.Find(id);
            if (affectation == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConducteur = new SelectList(db.Conducteur, "IdConducteur", "NumCNIConducteur", affectation.IdConducteur);
            ViewBag.IdMission = new SelectList(db.Mission, "IdMission", "LibelleMission", affectation.IdMission);
            ViewBag.IdVehicule = new SelectList(db.Vehicule, "IdVehicule", "NomVehicule", affectation.IdVehicule);
            return View(affectation);
        }

        // POST: Affectation/Affectations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAffectation,IdMission,IdVehicule,IdConducteur,DateAffectation")] DAL.Affectation affectation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(affectation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConducteur = new SelectList(db.Conducteur, "IdConducteur", "NumCNIConducteur", affectation.IdConducteur);
            ViewBag.IdMission = new SelectList(db.Mission, "IdMission", "LibelleMission", affectation.IdMission);
            ViewBag.IdVehicule = new SelectList(db.Vehicule, "IdVehicule", "NomVehicule", affectation.IdVehicule);
            return View(affectation);
        }

        // GET: Affectation/Affectations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Affectation affectation = db.Affectation.Find(id);
            if (affectation == null)
            {
                return HttpNotFound();
            }
            return View(affectation);
        }

        // POST: Affectation/Affectations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAL.Affectation affectation = db.Affectation.Find(id);
            db.Affectation.Remove(affectation);
            db.SaveChanges();
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
    }
}
