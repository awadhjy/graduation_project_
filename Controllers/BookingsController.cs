using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using graduation_project.Models;

namespace graduation_project.Controllers
{
    public class BookingsController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();
        IQueryable<Booking> bookings;
        int userID;
        string isAuthorized()
        {
            TempData["reservation"] = "True";
            TempData["questions"] = null;
            TempData["control"] = null;
            var UserRoles = Session["userRoles"];
            if (UserRoles != null)
            {
                 this.userID = int.Parse(Session["userID"].ToString());
                List<string> userRole = Session["userRoles"] as List<string>;
                if (userRole.Contains("super") || userRole.Contains("admin"))
                    return "admin";
                if (userRole.Contains("doctor"))
                    return "doctor";
                if (userRole.Contains("reviewer"))
                    return "reviewer";
            }
            return null;

        }
        public ActionResult Confirmed()
        {
            string role = isAuthorized();
            if ( role== "reviewer")
            {
                this.bookings = db.Bookings.Include(b => b.Clinic).Include(b => b.Doctor).Include(b => b.Person).Where(b => b.personID == this.userID&&b.active==true);
                return View(this.bookings.ToList());
            }
            else if(role == "admin")
            {
                this.bookings = db.Bookings.Include(b => b.Clinic).Include(b => b.Doctor).Include(b => b.Person).Where(b => b.active == true);
                ViewBag.isAdmin = true;
                return View(this.bookings.ToList());
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }   
        public ActionResult Confirm()
        {
            string role = isAuthorized();
            
            if(role == "admin")
            {
                this.bookings = db.Bookings.Include(b => b.Clinic).Include(b => b.Doctor).Include(b => b.Person).Where(b => b.active == null);
                
                return View(this.bookings.ToList());
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }
        [HttpPost]
        public ActionResult Confirm(int id)
        {
            db.Bookings.Find(id).active = true;
            db.SaveChanges();

            ViewBag.pageTitle = "تأكيد حجز";
            string role = isAuthorized();

            if (role == "admin")
            {
                this.bookings = db.Bookings.Include(b => b.Clinic).Include(b => b.Doctor).Include(b => b.Person).Where(b => b.active == null);

                return View(this.bookings.ToList());
            }
            return View("~/Views/Shared/noAccess.cshtml");
        }
        // GET: Bookings
        public ActionResult Index()
        {
            ViewBag.pageTitle = "الحجوزات";

            TempData["btn1"] = "Bookings"+","+"Create"+","+"حجز جديد";
            TempData["btn2"] = "Bookings"+","+ "Confirmed" + ","+"الحجوز المؤكدة";



            switch (isAuthorized())
            {
                case "reviewer":
                     this.bookings = db.Bookings.Include(b => b.Clinic).Include(b => b.Doctor).Include(b => b.Person).Where(b=>b.personID==this.userID );
                    return View(this.bookings.ToList());
                case "doctor":
                     this.bookings = db.Bookings.Include(b => b.Clinic).Include(b => b.Doctor).Include(b => b.Person).Where(b=>b.Doctor.personID== this.userID);
                    return View(this.bookings.ToList());
                case "admin":
                     this.bookings = db.Bookings.Include(b => b.Clinic).Include(b => b.Doctor).Include(b => b.Person);
                    ViewBag.isAdmin = true;
                    return View(this.bookings.ToList());
                default:
                    return View("~/Views/Shared/noAccess.cshtml");


            }
            
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            

            if (booking == null)
            {
                return HttpNotFound();
            }
            if ( isAuthorized()=="admin"||booking.personID == this.userID)
                return View(booking);
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {if (isAuthorized() == "reviewer")
            {
                var data = from d in db.Doctors
                       from p in db.People
                       where p.ID == d.personID
                       select new SelectListItem { Value =d.ID.ToString(), Text = p.name };

            ViewBag.clinicID = new SelectList(db.Clinics.Where(c=>c.active==true), "ID", "name");
            ViewBag.doctorID = new SelectList(data, "Value", "Text");
                return View();
            }
        
            return View("~/Views/Shared/noAccess.cshtml");
    }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clinicID,doctorID,date,note")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _ = isAuthorized();
                booking.personID = this.userID;
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clinicID = new SelectList(db.Clinics.Where(c=>c.active==true), "ID", "name", booking.clinicID);
            ViewBag.doctorID = new SelectList(db.Doctors, "ID", "ID", booking.doctorID);
            //ViewBag.personID = new SelectList(db.People, "ID", "name", booking.personID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            
            var data = from d in db.Doctors
                       from p in db.People
                       where p.ID == d.personID
                       select new SelectListItem { Value =d.ID.ToString(), Text = p.name };

            ViewBag.clinicID = new SelectList(db.Clinics.Where(c=>c.active==true), "ID", "name", booking.clinicID);
            ViewBag.doctorID = new SelectList(data, "Value", "Text");

            _ = isAuthorized() == "admin" ? ViewBag.isAdmin = true : ViewBag.isAdmin = false;
            if (booking.personID == this.userID|| isAuthorized()=="admin")
                return View(booking);
            return View("~/Views/Shared/noAccess.cshtml");
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,clinicID,doctorID,personID,date,note")] Booking booking ,String active)
        {
            
            if (ModelState.IsValid)
            {
                
                Booking oldbooking = db.Bookings.Find(booking.ID);
                oldbooking.date = booking.date;
                oldbooking.clinicID = booking.clinicID;
                oldbooking.doctorID = booking.doctorID;
                switch (active)
                {
                    case "null":
                        oldbooking.active = null;
                        break;
                    case "1":
                        oldbooking.active = true;
                        break;
                    case "0":
                        oldbooking.active = false;
                        break;
                }
                oldbooking.note = booking.note;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clinicID = new SelectList(db.Clinics, "ID", "name", booking.clinicID);
            ViewBag.doctorID = new SelectList(db.Doctors, "ID", "ID", booking.doctorID);
           // ViewBag.personID = new SelectList(db.People, "ID", "name", booking.personID);
            
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            booking.active = false;
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
