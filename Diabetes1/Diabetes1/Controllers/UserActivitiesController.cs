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
    public class UserActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserActivities
        public ActionResult Index()
        {
            return View(db.UserActivities.ToList());
        }

        // GET: UserActivities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserActivity userActivity = db.UserActivities.Find(id);
            if (userActivity == null)
            {
                return HttpNotFound();
            }
            return View(userActivity);
        }

        // GET: UserActivities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ActivityId")] UserActivity userActivity)
        {
            if (ModelState.IsValid)
            {
                db.UserActivities.Add(userActivity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userActivity);
        }

        // GET: UserActivities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserActivity userActivity = db.UserActivities.Find(id);
            if (userActivity == null)
            {
                return HttpNotFound();
            }
            return View(userActivity);
        }

        // POST: UserActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ActivityId")] UserActivity userActivity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userActivity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userActivity);
        }

        // GET: UserActivities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserActivity userActivity = db.UserActivities.Find(id);
            if (userActivity == null)
            {
                return HttpNotFound();
            }
            return View(userActivity);
        }

        // POST: UserActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserActivity userActivity = db.UserActivities.Find(id);
            db.UserActivities.Remove(userActivity);
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
