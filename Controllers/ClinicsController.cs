using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using graduation_project.Models;

namespace graduation_project.Controllers
{
    public class ClinicsController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();
        bool isAuthorized()
        {
            var UserRoles = Session["userRoles"];
            if (UserRoles != null)
            {
                List<string> userRole = Session["userRoles"] as List<string>;
                if (userRole.Contains("super"))
                    return true;
            }
            return false;

        }
        // GET: Clinics
        public ActionResult Index()
        {
            if (isAuthorized())
            {
                return View(db.Clinics.Where(c=>c.active==true).ToList());
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // GET: Clinics/Details/5
        public ActionResult Details(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // GET: Clinics/Create
        public ActionResult Create()
        {
            if (isAuthorized())
            {
                return View();
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,address,description")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                clinic.active = true;
                db.Clinics.Add(clinic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,address,description")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                clinic.active = true;
                db.Entry(clinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isAuthorized())
            {
                Clinic clinic = db.Clinics.Find(id);
            clinic.active = false;
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View("~/Views/Shared/noAccess.cshtml");
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
