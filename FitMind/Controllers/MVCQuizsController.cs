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
    public class MVCQuizsController : Controller
    {
        private fitMindDbEntities1 db = new fitMindDbEntities1();

        // GET: MVCQuizs
        public ActionResult Index()
        {
            var quizs = db.Quizs.Include(q => q.Sponsor);
            return View(quizs.ToList());
        }

        // GET: MVCQuizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: MVCQuizs/Create
        public ActionResult Create()
        {
            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId");
            return View();
        }

        // POST: MVCQuizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "quizId,points,name,startDate,endDate,q1,q2,q3,q4,a1,a2,a3,a4,sponsorId")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Quizs.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId", quiz.sponsorId);
            return View(quiz);
        }

        // GET: MVCQuizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId", quiz.sponsorId);
            return View(quiz);
        }

        // POST: MVCQuizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "quizId,points,name,startDate,endDate,q1,q2,q3,q4,a1,a2,a3,a4,sponsorId")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId", quiz.sponsorId);
            return View(quiz);
        }

        // GET: MVCQuizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: MVCQuizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quizs.Find(id);
            db.Quizs.Remove(quiz);
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
