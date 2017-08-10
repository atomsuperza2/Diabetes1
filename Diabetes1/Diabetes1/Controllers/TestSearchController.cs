
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Diabetes1.Models;
using PagedList;



namespace Diabetes1.Controllers
{
    public class TestSearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
  
        // GET: TestSearch
       
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            try
            {
                int intPage = 1;
                int intPageSize = 5;
                int intTotalPageCount = 0;

                if (searchString != null)
                {
                    intPage = 1;
                }
                else
                {
                    if (currentFilter != null)
                    {
                        searchString = currentFilter;
                        intPage = page ?? 1;
                    }
                    else
                    {
                        searchString = "";
                        intPage = page ?? 1;
                    }
                }

                ViewBag.CurrentFilter = searchString;

                List<UserProfileInfo> col_UserDTO = new List<UserProfileInfo>();
                int intSkip = (intPage - 1) * intPageSize;
              
                intTotalPageCount = UserManager.Users
                    .Where(x => x.UserProfileInfo.FirstName.Contains(searchString))
                    .Count();

                var result = UserManager.Users
                    .Where(x => x.UserProfileInfo.FirstName.Contains(searchString))
                    .OrderBy(x => x.UserProfileInfo.FirstName)
                    .Skip(intSkip)
                    .Take(intPageSize)
                    .ToList();

                foreach (var item in result)
                {
                    UserProfileInfo objUserDTO = new UserProfileInfo();
                    
                    objUserDTO.FirstName = item.UserProfileInfo.FirstName;
                    objUserDTO.LastName = item.UserProfileInfo.LastName;
                    objUserDTO.Age = item.UserProfileInfo.Age;
                    objUserDTO.Height = item.UserProfileInfo.Height;
                    objUserDTO.Weight = item.UserProfileInfo.Weight;
                    col_UserDTO.Add(objUserDTO);
                }

                // Set the number of pages
                var _UserDTOAsIPagedList =
                    new StaticPagedList<UserProfileInfo>
                    (
                        col_UserDTO, intPage, intPageSize, intTotalPageCount
                        );

                return View(_UserDTOAsIPagedList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                List<UserProfileInfo> col_UserDTO = new List<UserProfileInfo>();

                return View(col_UserDTO.ToPagedList(1, 25));
            }
            //var user = from u in db.UserProfileInfo
            //           join a in db.UserAddress on u.addressId equals a.id
            //           select new { u.FirstName, u.LastName, u.Height, u.Weight, u.Age, u.Gender, a.Address, a.City, a.ZipCode };
            //{
            //    user = user.Where(u => u.FirstName.ToUpper().Contains(searchStringUserNameOrEmail.ToUpper()));
            //}
            //return View(user.ToList());
        }
      

        
        public ActionResult ViewGlycemic(int id)
        {
            var glycemics = db.UserGlycemics.Where(gly => gly.UserId == id).ToList();
            return View(glycemics);
        }
       

        
        public ActionResult UserAddress(int id)
        {
            var address = db.UserAddress.Where(a => a.UserId == id).ToList();
            return View(address);
        }
    

       
        public ActionResult UserMedicine(int id)

        {
            var medicine = db.Medicines.Where(m => m.UserId == id).ToList();
            return View(medicine);

        }
 

        // Utility

        
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
   
    }
}