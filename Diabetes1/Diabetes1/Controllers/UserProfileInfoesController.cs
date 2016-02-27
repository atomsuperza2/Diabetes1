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
    public class UserProfileInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserProfileInfoes
        public ActionResult Index()
        {
            return View(db.UserProfileInfo.ToList());
        }

        // GET: UserProfileInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfileInfo userProfileInfo = db.UserProfileInfo.Find(id);
            if (userProfileInfo == null)
            {
                return HttpNotFound();
            }
            return View(userProfileInfo);
        }

        // GET: UserProfileInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FirstName,LastName,Gender,Age,DiabetesType,Height,Weight,localImage")] UserProfileInfo userProfileInfo)
        {
            if (ModelState.IsValid)
            {
                db.UserProfileInfo.Add(userProfileInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userProfileInfo);
        }

        // GET: UserProfileInfoes/Edit/5
        public ActionResult Edit()
        {
            string username = User.Identity.Name;
            UserProfileInfo userProfileInfo = db.UserProfileInfo.FirstOrDefault(u => u.UserName.Equals(username));
            UserProfileInfo model = new UserProfileInfo();
            model.FirstName = userProfileInfo.FirstName;
            model.LastName = userProfileInfo.LastName;
            model.Gender = userProfileInfo.Gender;
            model.Age = userProfileInfo.Age;
            model.Height = userProfileInfo.Height;
            model.Weight = userProfileInfo.Weight;

            return View(model);
        }

        // POST: UserProfileInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(UserProfileInfo userprofile)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                // Get the userprofile
                UserProfileInfo userProfileInfo = db.UserProfileInfo.FirstOrDefault(u => u.UserName.Equals(username));

                // Update fields
                userProfileInfo.FirstName = userprofile.FirstName;
                userProfileInfo.LastName = userprofile.LastName;             
                userProfileInfo.Gender = userprofile.Gender;
                userProfileInfo.Age = userprofile.Age;
                userProfileInfo.Height = userprofile.Height;
                userProfileInfo.Weight = userprofile.Weight; 

                db.Entry(userProfileInfo).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home"); // or whatever
            }

            return View(userprofile);
        }

        // GET: UserProfileInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfileInfo userProfileInfo = db.UserProfileInfo.Find(id);
            if (userProfileInfo == null)
            {
                return HttpNotFound();
            }
            return View(userProfileInfo);
        }

        // POST: UserProfileInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfileInfo userProfileInfo = db.UserProfileInfo.Find(id);
            db.UserProfileInfo.Remove(userProfileInfo);
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
