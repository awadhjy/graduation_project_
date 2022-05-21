using graduation_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace graduation_project.Controllers
{
    public class LoginController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();
        // GET: Login
        public ActionResult Index()
        {
            Person person = new Person();
            return View(person);
        }public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "email,password")] Person person)
        {
            person.password = Encrypt.GetMD5Hash(person.password);
            Person user = db.People.Where(u => u.email == person.email).FirstOrDefault();

            if (user.active && person.email==user.email && person.password==user.password)
            {
                List<string> userRoleIDs = new List<string>();
                foreach (PersonRole role in user.PersonRoles)
                    userRoleIDs.Add(role.Role.name.ToLower());
                Session["userID"] = user.ID;
                Session["userRoles"] = userRoleIDs;
                


            }
            return View();
        }

        // GET: Login/Create
        
    }
}
