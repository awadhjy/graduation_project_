using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using graduation_project.Models;
using System.Data.Entity;

namespace graduation_project.Controllers
{
    public class DoctorsController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();

        // GET: Doctors
        public ActionResult Index()
        {
            var doctors = db.Doctors.Include(d => d.Person).Include(d => d.Specialization).Where(d=>d.Person.Doctors.FirstOrDefault().ID>-1);
            return View(doctors.ToList());
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            ViewBag.Specialtie = new SelectList(db.Specializations, "ID", "name");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Specialtie")] Doctor doctor,string personName,string personEmail,string personPassword)
        {
            
            if (ModelState.IsValid)
            {
                Person person = new Person();
                person.email = personEmail;
                person.active = true;
                if (!(personPassword == "" || personPassword == null))
                    person.password = Encrypt.GetMD5Hash(personPassword);
                person.name = personName;

                db.People.Add(person);
                db.SaveChanges();

                doctor.personID = person.ID;
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Specialtie = new SelectList(db.Specializations, "ID", "name", doctor.Specialtie);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            doctor.Person = db.People.Find(doctor.personID);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Specialtie = new SelectList(db.Specializations, "ID", "name", doctor.Specialtie);
            return View(doctor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Specialtie")] Doctor doctor, string personName, string personEmail, string personPassword)
        {
            if (ModelState.IsValid)
            {
               
                Person person = db.Doctors.Find(doctor.ID).Person;
                    person.email = personEmail;
                if (!(personPassword == "" || personPassword == null))
                    person.password = Encrypt.GetMD5Hash(personPassword);
                    person.name = personName;

                    Doctor doctor1= db.Doctors.Find(doctor.ID);
                doctor1.Specialtie = doctor.Specialtie;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.personID = new SelectList(db.People, "ID", "name", doctor.personID);
            ViewBag.Specialtie = new SelectList(db.Specializations, "ID", "name", doctor.Specialtie);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            Person person = db.Doctors.Find(id).Person;
            db.Doctors.Remove(doctor);
            person.active = false;
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
