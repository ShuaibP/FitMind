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
    public class MVCGoodiesController : Controller
    {
        private fitMindDbEntities1 db = new fitMindDbEntities1();

        // GET: MVCGoodies
        public ActionResult Index()
        {
            var goodies = db.Goodies.Include(g => g.Sponsor);
            return View(goodies.ToList());
        }

        // GET: MVCGoodies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goodie goodie = db.Goodies.Find(id);
            if (goodie == null)
            {
                return HttpNotFound();
            }
            return View(goodie);
        }

        // GET: MVCGoodies/Create
        public ActionResult Create()
        {
            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId");
            return View();
        }

        // POST: MVCGoodies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "goodieId,description,pointsNeeded,category,sponsorId")] Goodie goodie)
        {
            if (ModelState.IsValid)
            {
                db.Goodies.Add(goodie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId", goodie.sponsorId);
            return View(goodie);
        }

        // GET: MVCGoodies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goodie goodie = db.Goodies.Find(id);
            if (goodie == null)
            {
                return HttpNotFound();
            }
            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId", goodie.sponsorId);
            return View(goodie);
        }

        // POST: MVCGoodies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "goodieId,description,pointsNeeded,category,sponsorId")] Goodie goodie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goodie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sponsorId = new SelectList(db.Sponsors, "sponsorId", "sponsorId", goodie.sponsorId);
            return View(goodie);
        }

        // GET: MVCGoodies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goodie goodie = db.Goodies.Find(id);
            if (goodie == null)
            {
                return HttpNotFound();
            }
            return View(goodie);
        }

        // POST: MVCGoodies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goodie goodie = db.Goodies.Find(id);
            db.Goodies.Remove(goodie);
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
