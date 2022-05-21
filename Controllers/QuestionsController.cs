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
    public class QuestionsController : Controller
    {
        private clinicEntities1 db = new clinicEntities1();
        int userID;
        string isAuthorized()
        {
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
    
        // GET: Questions
        public ActionResult Index()
        {
            return View(db.Questions.Where(q=>q.active==true).ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            QAvm qAvm = new QAvm();
            qAvm.answers= db.Answers.Where(a => a.questionID == id).ToList();
            qAvm.question = question;
            if (question == null)
            {
                return HttpNotFound();
            }
            _ = isAuthorized() == "doctor" ? ViewBag.isDoctor = true : ViewBag.isDoctor = false;
            return View(qAvm);
        }
        [HttpPost]
        public ActionResult Details(QAvm qAvm)
        {
            
            qAvm.answer.answerBy= 11;// ----------
            qAvm.answer.date= DateTime.Now;// ----------
            qAvm.answer.questionID= qAvm.question.ID;// ----------
            qAvm.answer.answer1= qAvm.answer.answer1;// ----------

            db.Answers.Add(qAvm.answer);
            db.SaveChanges();
            return RedirectToAction("Details",qAvm.question.ID);
        }

            // GET: Questions/Create
            public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,title,question1,date")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.date = DateTime.Now;
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,title,question1,date")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.date = DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            question.active = false;
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
