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
    public class PeopleController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();

        // GET: People
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
        public ActionResult Index()
        
        {
            if (isAuthorized())
                    return View(db.People.Where(p => p.active == true).ToList());
            
            return Content("you have not any access to this part");
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Person person = db.People.Find(id);
                if (person == null)
                {
                    return HttpNotFound();
                }
                return View(person);
            }
            return Content("you have not any access to this part");
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,email,password")] Person person)
        {
            if (ModelState.IsValid)
            {
                if (!(person.password == "" || person.password == null))
                    person.password= Encrypt.GetMD5Hash(person.password);
                person.active = true;
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (isAuthorized()) { 
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            person.password = "";
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person); }
            return Content("you have not any access to this part");
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,email,password")] Person person)
        {
            if (ModelState.IsValid)
            {
                if (!(person.password == ""|| person.password == null))
                person.password = Encrypt.GetMD5Hash(person.password);
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (isAuthorized())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Person person = db.People.Find(id);
                if (person == null)
                {
                    return HttpNotFound();
                }
                return View(person);
            }
            return Content("you have not any access to this part");
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isAuthorized())
            {
                Person person = db.People.Find(id);
            person.active = false;
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            return Content("you have not any access to this part");
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
