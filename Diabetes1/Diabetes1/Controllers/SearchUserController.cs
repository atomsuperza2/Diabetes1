using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diabetes1.Models;
using System.Data.Entity.Infrastructure;
using PagedList;

namespace Diabetes1.Controllers
{
    public class SearchUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: 
        public ActionResult Index(string searchString, string Filter_Value, int? Page_No)
        {
            
            List<ViewUserDetail> model = new List<ViewUserDetail>();

            if (searchString != null)
            {
                Page_No = 1;
            }
            else
            {
                searchString = Filter_Value;
            }

            ViewBag.FilterValue = searchString;

            var userInfo = db.UserProfileInfo.ToList();

            var userInfos = from u in db.UserProfileInfo
                            select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                userInfos = userInfos.Where(s => s.FirstName.Contains(searchString));
            }


            foreach (var item in userInfo)
            {
                var viewmodelx = new ViewUserDetail();
                viewmodelx.userInfo = item;
                var userAddress = db.UserAddress.Find(item.addressId);
                viewmodelx.userAddress = userAddress;
                //var userGlycemi = db.UserGlycemics.Find(item.userGlycemicId);
                //viewmodelx.userGlycemic = userGlycemi;
                //var userMedicine = db.Medicines.Find(item.userMedicineId);
                model.Add(viewmodelx);


            }

            int Size_Of_Page = 2;
            int No_Of_Page = (Page_No ?? 1);

            //var view = from a in db.UserProfileInfo
            //           join b in db.UserGlycemics on a.id equals b.UserId
            //           where a.id.Equals(b.UserId)
            //           select new ViewUserDetail { userInfo = a, userGlycemic = b };
            return View(model.ToPagedList(No_Of_Page, Size_Of_Page));     
        }
       
        public ActionResult ViewGlycemic(int id) {
            var glycemics = db.UserGlycemics.Where(gly => gly.UserId == id).ToList();
            return View(glycemics);
        }
        public ActionResult UserMedicine(int id)
            
        {
            var medicine = db.Medicines.Where(m => m.UserId == id).ToList();
            return View(medicine);

        }
        //public ActionResult ViewUserDetail()
        //{
        //    var userInfo = db.UserProfileInfo.ToList();
        //    var userAddress = db.UserAddress.ToList();
        //    var UseGlycemic = db.UserGlycemics.ToList();

        //    var view = new ViewUserDetail()
        //    {
        //        userInfo = userInfo,
        //        userAddress = userAddress,
        //        userGlycemic = UseGlycemic
        //    };
        //    return View(view);

        //}
        //public ActionResult Details(ViewUserDetail model, UserProfileInfo userProfileInfo, UserGlycemic userGlycemic, UserAddress userAddress)
        //{
        //    if (model == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var m = new ViewUserDetail(
        //    userProfileInfo = new UserProfileInfo
        //    {
        //        FirstName = model.userInfo.FirstName,
        //        LastName = model.userInfo.LastName,
        //        Gender = model.userInfo.Gender,
        //        Age = model.userInfo.Age,
        //        Height = model.userInfo.Height,
        //        Weight = model.userInfo.Weight
        //    },
        //    userGlycemic = new UserGlycemic
        //    {
        //        Value = model.userGlycemic.Value,
        //        Date = model.userGlycemic.Date,
        //    },
        //    userAddress = new UserAddress
        //    {
        //        Address = model.userAddress.Address,
        //        City = model.userAddress.City,
        //        ZipCode = model.userAddress.ZipCode
        //    });

        //    if (m == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View();
        //}
        //public ApplicationDbContext db;
        //public SearchUserController()
        //{
        //    this.db = new ApplicationDbContext();
        //}
        //// GET: SearchUser
        //    public ViewResult Index(string sortOrder/*, string searchString*/)
        //    {

        //        ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : " ";
        //        //ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
        //        //ViewBag.GlycemicSortParm = sortOrder == "Value" ? "value_desc" : "Value";
        //        var user = from x in db.UserProfileInfo select x;
        //        //var user2 = from a in db.UserAddress select a;
        //        //var user3 = from g in db.UserGlycemics select g; 

        //        //if (!String.IsNullOrEmpty(searchString))
        //        //{
        //        //    user = user.Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString));
        //        //    //user2 = user2.Where(a => a.Address.Contains(searchString) || a.City.Contains(searchString));

        //        //}

        //        switch (sortOrder)
        //        {
        //            case "name_desc":
        //                user = user.OrderByDescending(x => x.FirstName);
        //                break;
        //            default:
        //                user = user.OrderBy(x => x.FirstName);
        //                break;
        //        }
        //        return View(user.ToList());
        //    }
    }
}

////var m = new ViewUserDetail();
////m.userInfo.FirstName = model.userInfo.FirstName;
////m.userInfo.LastName = model.userInfo.LastName;
////m.userInfo.Gender = model.userInfo.Gender;
////m.userInfo.Age = model.userInfo.Age;
////m.userInfo.Height = model.userInfo.Height;
////m.userInfo.Weight = model.userInfo.Weight;
////m.userGlycemic.Value = model.userGlycemic.Value;
////m.userGlycemic.Date = model.userGlycemic.Date;
////m.userAddress.Address = model.userAddress.Address;
////m.userAddress.City = model.userAddress.City;
////m.userAddress.ZipCode = model.userAddress.ZipCode;

//var x = (from u in db.UserProfileInfo
//         join a in db.UserAddress on u.addressId equals a.id
//         join g in db.UserGlycemics on u.userGlycemicId equals g.Id
//         select new { u.FirstName, u.LastName, u.Age, u.DiabetesType, u.Gender, u.Height, u.Weight, g.Value, a.Address, a.City, a.ZipCode });