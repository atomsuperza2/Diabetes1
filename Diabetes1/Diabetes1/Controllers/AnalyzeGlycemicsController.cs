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
    public class AnalyzeGlycemicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnalyzeGlycemics
        public ActionResult Index()
        {
            return View(db.AnalyzeGlycemics.ToList());
        }

        // GET: AnalyzeGlycemics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalyzeGlycemic analyzeGlycemic = db.AnalyzeGlycemics.Find(id);
            if (analyzeGlycemic == null)
            {
                return HttpNotFound();
            }
            return View(analyzeGlycemic);
        }

        // GET: AnalyzeGlycemics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalyzeGlycemics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,symptom,cause,TakeCare")] AnalyzeGlycemic analyzeGlycemic)
        {
            if (ModelState.IsValid)
            {
                db.AnalyzeGlycemics.Add(analyzeGlycemic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(analyzeGlycemic);
        }

        // GET: AnalyzeGlycemics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalyzeGlycemic analyzeGlycemic = db.AnalyzeGlycemics.Find(id);
            if (analyzeGlycemic == null)
            {
                return HttpNotFound();
            }
            return View(analyzeGlycemic);
        }

        // POST: AnalyzeGlycemics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,symptom,cause,TakeCare")] AnalyzeGlycemic analyzeGlycemic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analyzeGlycemic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(analyzeGlycemic);
        }

        // GET: AnalyzeGlycemics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalyzeGlycemic analyzeGlycemic = db.AnalyzeGlycemics.Find(id);
            if (analyzeGlycemic == null)
            {
                return HttpNotFound();
            }
            return View(analyzeGlycemic);
        }

        // POST: AnalyzeGlycemics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnalyzeGlycemic analyzeGlycemic = db.AnalyzeGlycemics.Find(id);
            db.AnalyzeGlycemics.Remove(analyzeGlycemic);
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
