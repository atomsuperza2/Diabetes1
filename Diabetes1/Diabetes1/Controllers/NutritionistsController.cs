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
    public class NutritionistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Nutritionists
        public ActionResult Index()
        {
            return View(db.Nutritionists.ToList());
        }

        // GET: Nutritionists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutritionists nutritionists = db.Nutritionists.Find(id);
            if (nutritionists == null)
            {
                return HttpNotFound();
            }
            return View(nutritionists);
        }

        // GET: Nutritionists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nutritionists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_nutritionists,UserName,Password,FirstName,LastName")] Nutritionists nutritionists)
        {
            if (ModelState.IsValid)
            {
                db.Nutritionists.Add(nutritionists);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nutritionists);
        }

        // GET: Nutritionists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutritionists nutritionists = db.Nutritionists.Find(id);
            if (nutritionists == null)
            {
                return HttpNotFound();
            }
            return View(nutritionists);
        }

        // POST: Nutritionists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_nutritionists,UserName,Password,FirstName,LastName")] Nutritionists nutritionists)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutritionists).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nutritionists);
        }

        // GET: Nutritionists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutritionists nutritionists = db.Nutritionists.Find(id);
            if (nutritionists == null)
            {
                return HttpNotFound();
            }
            return View(nutritionists);
        }

        // POST: Nutritionists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nutritionists nutritionists = db.Nutritionists.Find(id);
            db.Nutritionists.Remove(nutritionists);
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
