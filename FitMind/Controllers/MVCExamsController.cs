using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitMind.Models;

namespace FitMind.Controllers
{
    public class MVCExamsController : Controller
    {
        private fitMindDbEntities1 db = new fitMindDbEntities1();

        // GET: MVCExams
        public ActionResult Index()
        {
            var exams = db.Exams.Include(e => e.Subject);
            return View(exams.ToList());
        }

        // GET: MVCExams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: MVCExams/Create
        public ActionResult Create()
        {
            ViewBag.subjectId = new SelectList(db.Subjects, "subjectId", "name");
            return View();
        }

        // POST: MVCExams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "examId,points,name,q1,q2,q3,q4,a1,a2,a3,a4,d1,d2,d3,d4,subjectId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exams.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.subjectId = new SelectList(db.Subjects, "subjectId", "name", exam.subjectId);
            return View(exam);
        }

        // GET: MVCExams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.subjectId = new SelectList(db.Subjects, "subjectId", "name", exam.subjectId);
            return View(exam);
        }

        // POST: MVCExams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "examId,points,name,q1,q2,q3,q4,a1,a2,a3,a4,d1,d2,d3,d4,subjectId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.subjectId = new SelectList(db.Subjects, "subjectId", "name", exam.subjectId);
            return View(exam);
        }

        // GET: MVCExams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: MVCExams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam exam = db.Exams.Find(id);
            db.Exams.Remove(exam);
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
