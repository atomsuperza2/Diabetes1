﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Diabetes1.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Configuration;


namespace Diabetes1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationDbContext db;
        public AccountController()
        {
            this.db = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();
            if (profile == null)
            {
                return Redirect("/");
            }
            ViewData["Medicines"] = db.Medicines.Where(m => m.UserId == profile.id).ToList();
            ViewData["Food"] = db.Foods.ToList();
            ViewData["UserFood"] = db.UserFoods.Join(db.Foods, t1 => t1.FoodId, t2 => t2.id_food, (t1, t2) => new { UserFood = t1, Food = t2 }).Select(s => new LFood { food_name = s.Food.food_name, id_food = s.Food.id_food }).ToList();
            ViewData["Activity"] = db.Activities.ToList();
            ViewData["UserActivity"] = db.UserActivities.Join(db.Activities, a1 => a1.ActivityId, a2 => a2.id_activity, (a1, a2) => new { UserActivity = a1, Activity = a2 }).Select(s => new LActivity { activity_name = s.Activity.activity_name, id_activity = s.Activity.id_activity }).ToList();

            ViewData["TodayActivity"] = db.TodayActivities.Where(a => a.UserId == profile.id).Where(b => b.Date.Day == DateTime.Now.Day).ToList();
            ViewData["TodayFood"] = db.TodayFoods.Where(f => f.UserId == profile.id).Where(b => b.Date.Day == DateTime.Now.Day).ToList();

            double sumActs = 0;
            int today = 0;

            //var acts = db.TodayActivities.Where(a => a.UserId == profile.id).OrderBy(a => a.Id).ToList();
            //List<double> graphAct = new List<double>();
            //foreach (var act in acts)
            //{
            //    Activity temp = db.Activities.Find(act.ActivityId);
            //    if (today != act.Date.Day)
            //    {
            //        if (sumActs != 0)
            //        {
            //            graphAct.Add(sumActs);
            //            sumActs = 0;
            //        }
            //        if (sumActs == 0)
            //        {
            //            today = act.Date.Day;
            //        }
            //        sumActs += temp.activity_calories;
            //    }
            //    else {
            //        today = act.Date.Day;
            //        sumActs += temp.activity_calories;
            //    }

            //}
            //graphAct.Add(sumActs);
            //ViewData["GraphActivity"] = graphAct;

            sumActs = 0;
            today = 0;
            double sumGly = 0;
            var foods = db.TodayFoods.Where(a => a.UserId == profile.id).OrderBy(a => a.Id).ToList();
            List<double> graphFood = new List<double>();
            List<double> graphGlycemic = new List<double>();
            foreach (var food in foods)
            {
                Food temp = db.Foods.Find(food.FoodId);
                if (today != food.Date.Day)
                {
                    if (sumActs != 0)
                    {
                        graphFood.Add(sumActs);
                        graphGlycemic.Add(sumGly);
                        sumActs = 0;
                    }
                    if (sumActs == 0)
                    {
                        today = food.Date.Day;
                    }
                    sumActs += temp.food_Calories;
                    sumGly += temp.food_GlycemicIndex;
                }
                else {
                    today = food.Date.Day;
                    sumActs += temp.food_Calories;
                    sumGly += temp.food_GlycemicIndex;
                }
            }
            graphFood.Add(sumActs);
            graphGlycemic.Add(sumGly);
            ViewData["GraphFood"] = graphFood;
            ViewData["GraphGlycemic"] = graphGlycemic;

            //sumActs = 0;
            //today = 0;
            //var glycemics = db.UserGlycemics.Where(a => a.UserId == profile.id).OrderBy(a => a.Id).ToList();
            //List<double> graphGlycemic = new List<double>();
            //foreach (var glycemic in glycemics)
            //{
            //    if (today != glycemic.Date.Day)
            //    {
            //        if (sumActs != 0)
            //        {
            //            graphGlycemic.Add(sumActs);
            //            sumActs = 0;
            //        }
            //        if (sumActs == 0)
            //        {
            //            today = glycemic.Date.Day;
            //        }
            //        sumActs += glycemic.Value;
            //    }
            //    else {
            //        today = glycemic.Date.Day;
            //        sumActs += glycemic.Value;
            //    }
            //}
            //graphGlycemic.Add(sumActs);
            //ViewData["GraphGlycemic"] = graphGlycemic;

            List<DateTime> dates = db.TodayActivities.Where(a => a.UserId == profile.id).OrderBy(a => a.Id).Select(a => a.Date).Distinct().ToList();
            ViewData["Date"] = dates;
            return View();
        }
        [HttpPost]
        public virtual ActionResult AddTodayFood(int value = 0)
        {
            if (value != 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();

                addTodayfood(profile, value);
            }
            return Redirect("/Account/");
        }

        public virtual TodayFood addTodayfood(UserProfileInfo profile, int value = 0)
        {
            try
            {
                TodayFood food = new TodayFood();
                food.UserId = profile.id;
                food.FoodId = value;
                food.Date = DateTime.Now;
                db.TodayFoods.Add(food);
                db.SaveChanges();
                return food;
            }
            catch (Exception e) {
                return null;
            }
        }


        //[HttpPost]
        public virtual ActionResult RemoveTodayFood(int value = 0)
        {
            if (value != 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();

                removeTodayFoodData(value);
                //try {

                //    TodayFood food = db.TodayFoods.Find(value);
                //    db.TodayFoods.Remove(food);
                //    db.SaveChanges();
                    
                //}catch (Exception e)
                //{
                //    return null;
                //}
            }
            return Redirect("/Account/");
        }

        public virtual TodayFood removeTodayFoodData (int value = 0)
        {
            try
            {

                TodayFood food = db.TodayFoods.Find(value);
                db.TodayFoods.Remove(food);
                db.SaveChanges();
                return food;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    
        [HttpPost]
        public ActionResult AddTodayActivity(int value = 0)
        {
            if (value != 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();

                TodayActivity activity = new TodayActivity();
                activity.UserId = profile.id;
                activity.ActivityId = value;
                activity.Date = DateTime.Now;
                db.TodayActivities.Add(activity);
                db.SaveChanges();
            }
            return Redirect("/Account/");
        }

        [AllowAnonymous]
        public ActionResult DisplayProfile()
        {
            ViewData["Medicines"] = db.Medicines.ToList();
            //ViewData["UMedicine"] = null;
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            //currentUser.UserProfileInfo.FirstName = ViewBag.FirstName;
            //currentUser.UserProfileInfo.LastName = ViewBag.LastName;
            //currentUser.UserProfileInfo.Gender = ViewBag.Gender;

            return View();
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // Create the Admin account using setting in Web.Config (if needed)
            CreateAdminIfNeeded();

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        /*Health Plan*/
        [AllowAnonymous]
        public ActionResult HealthPlan()
        {
            ViewData["Medicines"] = db.Medicines.ToList();
            ViewData["Foods"] = db.Foods.ToList();
            ViewData["Activities"] = db.Activities.ToList();
            ViewData["BMI"] = null;
            ViewData["UserFoods"] = null;
            ViewData["UserActivities"] = null;
            ViewData["UserGlycemic"] = null;
            ViewData["Calories"] = null;
            ViewData["CaloriesA"] = null;
            ViewData["Glycemic"] = null;

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();
            if (profile != null)
            {
                var BMRM = (66 + ((13.7 * profile.Weight) + (5 * profile.Height) - (6.8 * profile.Age)));
                ViewData["BMRM"] = BMRM;

                var BMRF = (665 + ((9.6 * profile.Weight) + (1.8 * profile.Height) - (4.7 * profile.Age)));
                ViewData["BMRF"] = BMRF;

                var BMI = profile.Weight / ((profile.Height / 100) * (profile.Height / 100));
                ViewData["BMI"] = BMI;

                var foods = db.UserFoods.Where(f => f.UserId == profile.id).ToList();
                ViewData["UserFoods"] = foods;

                var activities = db.UserActivities.Where(a => a.UserId == profile.id).ToList();
                ViewData["UserActivities"] = activities;

                var glycemics = db.UserGlycemics.Where(g => g.UserId == profile.id).ToList();
                ViewData["UserGlycemic"] = glycemics;

                double calories = 0;
                double glycemic = 0;
                foreach (var food in foods)
                {
                    var objFood = db.Foods.Where(f => f.id_food == food.FoodId).FirstOrDefault();
                    if (objFood != null)
                    {
                        calories += objFood.food_Calories;
                        glycemic += objFood.food_GlycemicIndex;
                    }
                }

                double caloriesA = 0;
                foreach (var activity in activities)
                {
                    var objActivity = db.Activities.Where(f => f.id_activity == activity.ActivityId).FirstOrDefault();
                    if (objActivity != null)
                    {
                        caloriesA += objActivity.activity_calories;
                    }
                }

                //calories /= foods.Count;
                glycemic /= foods.Count;
                //caloriesA /= activities.Count;
                ViewData["Calories"] = calories;
                ViewData["CaloriesA"] = caloriesA;
                ViewData["Glycemic"] = glycemic;
            }
            return View();
        }
        [HttpPost]
        public virtual ActionResult AddGlycemic(int value = 0)
        {
            if (value != 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();

                addUserGlycemic(profile, value);
                //UserGlycemic glycemic = new UserGlycemic();
                //glycemic.UserId = profile.id;
                //glycemic.Value = value;
                //glycemic.Date = DateTime.Now;
                //db.UserGlycemics.Add(glycemic);
                //db.SaveChanges();

            }
            return Redirect("/Account/HealthPlan/");
        }
        public virtual UserGlycemic addUserGlycemic (UserProfileInfo profile, int value = 0)
        {
            try {
                UserGlycemic glycemic = new UserGlycemic();
                glycemic.UserId = profile.id;
                glycemic.Value = value;
                glycemic.Date = DateTime.Now;
                db.UserGlycemics.Add(glycemic);
                db.SaveChanges();
                return glycemic;
            }
            catch (Exception e)
            {
                return null;
            }
            }
        [HttpPost]
        public virtual ActionResult AddFood(int id = 0)
        {
            if (id != 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();

                addUSerFood(profile, id);

                //UserFood food = new UserFood();
                //food.UserId = profile.id;
                //food.FoodId = id;
                //db.UserFoods.Add(food);
                //db.SaveChanges();
            }
            return Redirect("/Account/HealthPlan/");
        }
        public virtual UserFood addUSerFood(UserProfileInfo profile, int id = 0)
        {
            try{ 
            UserFood food = new UserFood();
            food.UserId = profile.id;
            food.FoodId = id;
            db.UserFoods.Add(food);
            db.SaveChanges();
                return food;
        }catch (Exception e)
            { return null; }
        }

        [HttpPost]
        public ActionResult AddActivity(int id = 0)
        {
            if (id != 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();

                UserActivity activity = new UserActivity();
                activity.UserId = profile.id;
                activity.ActivityId = id;
                db.UserActivities.Add(activity);
                db.SaveChanges();
            }
            return Redirect("/Account/HealthPlan/");
        }
        [HttpPost]
        public virtual ActionResult DeleteFood(int id = 0)
        {
            if (id != 0)
            {
                deleteFoodData(id);
                //UserFood food = db.UserFoods.Find(id);
                //db.UserFoods.Remove(food);
                //db.SaveChanges();
            }
            return Redirect("/Account/HealthPlan/");
        }
        public virtual UserFood deleteFoodData(int id = 0)
        {
            try
            {
                UserFood food = db.UserFoods.Find(id);
                db.UserFoods.Remove(food);
                db.SaveChanges();
                return food;
            }
            catch(Exception e)
            {
                return null;
            }
        } 
        [HttpPost]
        public ActionResult DeleteActivity(int id = 0)
        {
            if (id != 0)
            {
                UserActivity activity = db.UserActivities.Find(id);
                db.UserActivities.Remove(activity);
                db.SaveChanges();
            }
            return Redirect("/Account/HealthPlan/");
        }
        /*End Health Plan*/

        // part of medicine

        [HttpPost]
        public virtual ActionResult AddMedicine(string name = null)
        {
            if (name != null)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var profile = db.UserProfileInfo.Where(u => u.UserName == currentUser.UserName).FirstOrDefault();

                addMedicineData(profile, name);
                //Medicine medicine = new Medicine();
                //medicine.UserId = profile.id;
                //medicine.MedicineName = name;

                //db.Medicines.Add(medicine);
                //db.SaveChanges();
            }
            return Redirect("/Account/DisplayProfile/");
        }
        public virtual Medicine addMedicineData (UserProfileInfo profile , string name = null)
        {
            try
            {
                Medicine medicine = new Medicine();
                medicine.UserId = profile.id;
                medicine.MedicineName = name;
               
                db.Medicines.Add(medicine);
                db.SaveChanges();
                
                return medicine;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        [HttpPost]
        public virtual ActionResult DeleteMedicine(int id = 0)
        {
            if (id != 0)
            {
                deleteMedicineData(id);
                //Medicine medicine = db.Medicines.Find(id);
                //db.Medicines.Remove(medicine);
                //db.SaveChanges();
            }
            return Redirect("/Account/DisplayProfile/");
        }
        public virtual Medicine deleteMedicineData(int id = 0)
        {
            try
            {
                Medicine medicine = db.Medicines.Find(id);
                db.Medicines.Remove(medicine);
                db.SaveChanges();
                return medicine;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        [HttpPost]
        public virtual ActionResult UpdateMedicine(int[] medicine = null)
        {
            if (medicine != null)
            {
                foreach (var med in medicine)
                {
                    Medicine pill = db.Medicines.Find(med);
                    pill.Today = DateTime.Now;
                    pill.Take = 1;
                    db.SaveChanges();
                }
            }
            return Redirect("/Account/");
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Redirect("/Account/Index");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //// GET: /Account/EditUserProfile
        public ActionResult EditUserProfile()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.FirstName = currentUser.UserProfileInfo.FirstName;
            ViewBag.LastName = currentUser.UserProfileInfo.LastName;
            ViewBag.Gender = currentUser.UserProfileInfo.Gender;
            ViewBag.Age = currentUser.UserProfileInfo.Age;
            ViewBag.Height = currentUser.UserProfileInfo.Height;
            ViewBag.Weight = currentUser.UserProfileInfo.Weight;
            //ViewBag.Address = currentUser.UserAddress.Address;
            //ViewBag.City = currentUser.UserAddress.City;
            //ViewBag.Zipcode = currentUser.UserAddress.ZipCode;
            return View(currentUser.UserProfileInfo);
        }
        //// Post: /Account/EditUserProfile
        [HttpPost]
        public ActionResult EditUserProfile(UserProfileInfo userprofile, UserAddress useraddress)
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var manager = new UserManager<ApplicationUser>(userStore);
                var user = manager.FindById(User.Identity.GetUserId());

                user.UserProfileInfo.FirstName = userprofile.FirstName;
                user.UserProfileInfo.LastName = userprofile.LastName;
                user.UserProfileInfo.Gender = userprofile.Gender;
                user.UserProfileInfo.Age = userprofile.Age;
                user.UserProfileInfo.Height = userprofile.Height;
                user.UserProfileInfo.Weight = userprofile.Weight;
                //user.UserAddress.Address = useraddress.Address;
                //user.UserAddress.City = useraddress.City;
                //user.UserAddress.ZipCode = useraddress.ZipCode;
                    manager.Update(user);

                return Redirect("/Manage/Index"); // or whatever
            }

            return View(userprofile);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

      
       

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,    

                    UserAddress = new UserAddress
                    {
                        Address = model.Address,
                        City = model.City,
                        ZipCode = model.ZipCode
                    },

                    UserProfileInfo = new UserProfileInfo
                    {
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Gender = model.Gender,
                        Age = model.Age,
                        Height = model.Height,
                        Weight = model.Weight,
                        DiabetesType = model.DiabetesType,
                        StartTreatment = model.StartTreatment
                    },
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user.UserProfileInfo.addressId = user.UserAddress.id;
                    user.UserAddress.UserId = user.UserProfileInfo.id;
                    var save = await UserManager.UpdateAsync(user);

                    // However, it always succeeds inspite of not updating the database
                    if (!save.Succeeded)
                    {
                        AddErrors(save);
                    }
                    db.SaveChanges();

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion


        // Utility

        // Add RoleManager
        #region public ApplicationRoleManager RoleManager
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        // Add CreateAdminIfNeeded
        #region private void CreateAdminIfNeeded()
        private void CreateAdminIfNeeded()
        {
            // Get Admin Account
            string AdminUserName = ConfigurationManager.AppSettings["AdminUserName"];
            string AdminPassword = ConfigurationManager.AppSettings["AdminPassword"];

            // See if Admin exists
            var objAdminUser = UserManager.FindByEmail(AdminUserName);

            if (objAdminUser == null)
            {
                //See if the Admin role exists
                if (!RoleManager.RoleExists("Administrator"))
                {
                    // Create the Admin Role (if needed)
                    IdentityRole objAdminRole = new IdentityRole("Administrator");
                    RoleManager.Create(objAdminRole);
                }


                // Create Admin user
                var objNewAdminUser = new ApplicationUser { UserName = AdminUserName, Email = AdminUserName };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUser, AdminPassword);
                // Put user in Admin role
                UserManager.AddToRole(objNewAdminUser.Id, "Administrator");
            }
        }
        #endregion
        public ActionResult Suggestion()
        {
            return View(db.Exercises.ToList());
        }
        public ActionResult BloodSuggestion()
        {
            return View(db.BloodSuggestions.ToList());
        }

        public ActionResult BloodAnalyze()
        {
            return View(db.AnalyzeGlycemics.ToList());
        }
    }
}