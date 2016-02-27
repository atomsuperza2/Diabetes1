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
    public class TodayActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TodayActivities
        public ActionResult Index()
        {
            return View(db.TodayActivities.ToList());
        }

        // GET: TodayActivities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodayActivity todayActivity = db.TodayActivities.Find(id);
            if (todayActivity == null)
            {
                return HttpNotFound();
            }
            return View(todayActivity);
        }

        // GET: TodayActivities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodayActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ActivityId,Date")] TodayActivity todayActivity)
        {
            if (ModelState.IsValid)
            {
                db.TodayActivities.Add(todayActivity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todayActivity);
        }

        // GET: TodayActivities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodayActivity todayActivity = db.TodayActivities.Find(id);
            if (todayActivity == null)
            {
                return HttpNotFound();
            }
            return View(todayActivity);
        }

        // POST: TodayActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ActivityId,Date")] TodayActivity todayActivity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todayActivity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todayActivity);
        }

        // GET: TodayActivities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodayActivity todayActivity = db.TodayActivities.Find(id);
            if (todayActivity == null)
            {
                return HttpNotFound();
            }
            return View(todayActivity);
        }

        // POST: TodayActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodayActivity todayActivity = db.TodayActivities.Find(id);
            db.TodayActivities.Remove(todayActivity);
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
