using graduation_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace graduation_project.Controllers
{
    public class ControlController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();
        int userID;
        string isAuthorized()
        {
            TempData["questions"] = null;
            TempData["reservation"] = null;
            TempData["control"] = "True";
            var UserRoles = Session["userRoles"];
            if (UserRoles != null)
            {
                userID = int.Parse(Session["userID"].ToString());
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
        // GET: Control
        public ActionResult Index()
        {
            ViewBag.pageTitle = "لوحة التحكم";
            TempData["control"] = "True";
            if (isAuthorized()=="admin")
            {
                return View();
            }
            return View("~/Views/Shared/noAccess.cshtml");

        }
    }
}