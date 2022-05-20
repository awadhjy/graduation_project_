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

        // GET: PersonRoles
        public ActionResult Index()
        {
            var personRoles = db.PersonRoles.Include(p => p.Person).Include(p => p.Role);
            return View(personRoles.ToList());
        }

        // GET: PersonRoles/Details/5
        public ActionResult Details(int? id)
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

        // GET: PersonRoles/Create
        public ActionResult Create()
        {
            ViewBag.personID = new SelectList(db.People, "ID", "name");
            ViewBag.roleID = new SelectList(db.Roles, "ID", "name");
            return View();
        }

        // POST: PersonRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,personID,roleID")] PersonRole personRole)
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

        // POST: PersonRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonRole personRole = db.PersonRoles.Find(id);
            db.PersonRoles.Remove(personRole);
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
