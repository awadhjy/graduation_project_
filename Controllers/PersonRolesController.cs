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
    public class PersonRolesController : Controller
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
        // GET: PersonRoles
        public ActionResult Index()
        {
            if (isAuthorized())
            {
                var personRoles = db.PersonRoles.Include(p => p.Person).Include(p => p.Role);
            return View(personRoles.ToList());
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // GET: PersonRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonRole personRole = db.PersonRoles.Find(id);
            if (personRole == null)
            {
                return HttpNotFound();
            }
            return View(personRole);
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // GET: PersonRoles/Create
        public ActionResult Create()
        {
            if (isAuthorized())
            {
                ViewBag.personID = new SelectList(db.People, "ID", "name");
            ViewBag.roleID = new SelectList(db.Roles, "ID", "name");
            return View();
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // POST: PersonRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "personID,roleID")] PersonRole personRole)
        {
            if (ModelState.IsValid)
            {
                db.PersonRoles.Add(personRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.personID = new SelectList(db.People, "ID", "name", personRole.personID);
            ViewBag.roleID = new SelectList(db.Roles, "ID", "name", personRole.roleID);
            return View(personRole);
        }

        // GET: PersonRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonRole personRole = db.PersonRoles.Find(id);
            if (personRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.personID = new SelectList(db.People, "ID", "name", personRole.personID);
            ViewBag.roleID = new SelectList(db.Roles, "ID", "name", personRole.roleID);
            return View(personRole);
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // POST: PersonRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,personID,roleID")] PersonRole personRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.personID = new SelectList(db.People, "ID", "name", personRole.personID);
            ViewBag.roleID = new SelectList(db.Roles, "ID", "name", personRole.roleID);
            return View(personRole);
        }

        // GET: PersonRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonRole personRole = db.PersonRoles.Find(id);
            if (personRole == null)
            {
                return HttpNotFound();
            }
            return View(personRole);
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // POST: PersonRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isAuthorized())
            {
                PersonRole personRole = db.PersonRoles.Find(id);
            db.PersonRoles.Remove(personRole);
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
