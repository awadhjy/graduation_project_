using graduation_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace graduation_project.Controllers
{
    public class HomeController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();
        public ActionResult Index()
        {

            if (db.People.FirstOrDefault() == null)
            {
                
                Role super = new Role();
                super.name = "super";
                db.Roles.Add(super);
                Role admin = new Role();
                admin.name = "admin";
                db.Roles.Add(admin);
                Role doctor = new Role();
                doctor.name = "doctor";
                db.Roles.Add(doctor);
                Role reviewer = new Role();
                reviewer.name = "reviewer";
                db.Roles.Add(reviewer);
                db.SaveChanges();
                Person first = new Person();
                first.active = true;
                first.name = "super";
                first.password = Encrypt.GetMD5Hash("super");
                first.email = "super@super.super";
                db.People.Add(first);
               db.SaveChanges();
                PersonRole firstRole = new PersonRole();
                firstRole.personID = first.ID;
                firstRole.roleID = super.ID;
                db.PersonRoles.Add(firstRole);
                db.SaveChanges();

            }
           return RedirectToAction("Index", "Questions");
        }

        
    }
}