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
    public class TodayFoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TodayFoods
        public ActionResult Index()
        {
            return View(db.TodayFoods.ToList());
        }

        // GET: TodayFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodayFood todayFood = db.TodayFoods.Find(id);
            if (todayFood == null)
            {
                return HttpNotFound();
            }
            return View(todayFood);
        }

        // GET: TodayFoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodayFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FoodId,Date")] TodayFood todayFood)
        {
            if (ModelState.IsValid)
            {
                db.TodayFoods.Add(todayFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todayFood);
        }

        // GET: TodayFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodayFood todayFood = db.TodayFoods.Find(id);
            if (todayFood == null)
            {
                return HttpNotFound();
            }
            return View(todayFood);
        }

        // POST: TodayFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FoodId,Date")] TodayFood todayFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todayFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todayFood);
        }

        // GET: TodayFoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodayFood todayFood = db.TodayFoods.Find(id);
            if (todayFood == null)
            {
                return HttpNotFound();
            }
            return View(todayFood);
        }

        // POST: TodayFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodayFood todayFood = db.TodayFoods.Find(id);
            db.TodayFoods.Remove(todayFood);
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
