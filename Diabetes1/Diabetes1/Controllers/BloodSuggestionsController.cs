using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diabetes1.Models;

namespace Diabetes1.Controllers
{
    public class BloodSuggestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BloodSuggestions
        public ActionResult Index()
        {
            return View(db.BloodSuggestions.ToList());
        }

        // GET: BloodSuggestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodSuggestion bloodSuggestion = db.BloodSuggestions.Find(id);
            if (bloodSuggestion == null)
            {
                return HttpNotFound();
            }
            return View(bloodSuggestion);
        }

        // GET: BloodSuggestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BloodSuggestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Bloodvalue,Discription,Cause,Management")] BloodSuggestion bloodSuggestion)
        {
            if (ModelState.IsValid)
            {
                db.BloodSuggestions.Add(bloodSuggestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bloodSuggestion);
        }

        // GET: BloodSuggestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodSuggestion bloodSuggestion = db.BloodSuggestions.Find(id);
            if (bloodSuggestion == null)
            {
                return HttpNotFound();
            }
            return View(bloodSuggestion);
        }

        // POST: BloodSuggestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Bloodvalue,Discription,Cause,Management")] BloodSuggestion bloodSuggestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodSuggestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bloodSuggestion);
        }

        // GET: BloodSuggestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodSuggestion bloodSuggestion = db.BloodSuggestions.Find(id);
            if (bloodSuggestion == null)
            {
                return HttpNotFound();
            }
            return View(bloodSuggestion);
        }

        // POST: BloodSuggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodSuggestion bloodSuggestion = db.BloodSuggestions.Find(id);
            db.BloodSuggestions.Remove(bloodSuggestion);
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
