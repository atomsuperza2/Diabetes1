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
    public class UserFoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserFoods
        public ActionResult Index()
        {
            return View(db.UserFoods.ToList());
        }

        // GET: UserFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFood userFood = db.UserFoods.Find(id);
            if (userFood == null)
            {
                return HttpNotFound();
            }
            return View(userFood);
        }

        // GET: UserFoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FoodId")] UserFood userFood)
        {
            if (ModelState.IsValid)
            {
                db.UserFoods.Add(userFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userFood);
        }

        // GET: UserFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFood userFood = db.UserFoods.Find(id);
            if (userFood == null)
            {
                return HttpNotFound();
            }
            return View(userFood);
        }

        // POST: UserFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FoodId")] UserFood userFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userFood);
        }

        // GET: UserFoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFood userFood = db.UserFoods.Find(id);
            if (userFood == null)
            {
                return HttpNotFound();
            }
            return View(userFood);
        }

        // POST: UserFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserFood userFood = db.UserFoods.Find(id);
            db.UserFoods.Remove(userFood);
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
