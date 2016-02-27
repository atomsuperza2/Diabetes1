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
    public class UserGlycemicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserGlycemics
        public ActionResult Index()
        {
            return View(db.UserGlycemics.ToList());
        }

        // GET: UserGlycemics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGlycemic userGlycemic = db.UserGlycemics.Find(id);
            if (userGlycemic == null)
            {
                return HttpNotFound();
            }
            return View(userGlycemic);
        }

        // GET: UserGlycemics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserGlycemics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Value,Date")] UserGlycemic userGlycemic)
        {
            if (ModelState.IsValid)
            {
                db.UserGlycemics.Add(userGlycemic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userGlycemic);
        }

        // GET: UserGlycemics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGlycemic userGlycemic = db.UserGlycemics.Find(id);
            if (userGlycemic == null)
            {
                return HttpNotFound();
            }
            return View(userGlycemic);
        }

        // POST: UserGlycemics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Value,Date")] UserGlycemic userGlycemic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGlycemic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userGlycemic);
        }

        // GET: UserGlycemics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGlycemic userGlycemic = db.UserGlycemics.Find(id);
            if (userGlycemic == null)
            {
                return HttpNotFound();
            }
            return View(userGlycemic);
        }

        // POST: UserGlycemics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserGlycemic userGlycemic = db.UserGlycemics.Find(id);
            db.UserGlycemics.Remove(userGlycemic);
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
