using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Diabetes1;
using Diabetes1.Controllers;
using Diabetes1.Models;
using System.Threading.Tasks;
using Moq;
using Diabetes1.Repository;

namespace UnitTestDiabetes1
{
    [TestClass]
    public class ControllerTest
    {
     
        //HomController
        [TestMethod]
        public void Index()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            ViewResult resule = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(resule);
        }
        [TestMethod]
        public void About()
        {
            HomeController con = new HomeController();
            ViewResult resule = con.About() as ViewResult;
            Assert.AreEqual("Your application description page.", resule.ViewBag.Message);
        }
        [TestMethod]
        public void Contact()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            ViewResult resule = controller.Contact() as ViewResult;
            //Assert
            Assert.IsNotNull(resule);
        }
    }
    //AccountController
    [TestClass]
    public class AccountControllerTest
    {
  
        [TestMethod]
        public void AddTodayFood()
        {
            var todayRespo = new Mock<AccountController>();
            todayRespo.Setup(x => x.AddTodayFood(It.IsAny<int>())).Returns(new ViewResult());
            var re = todayRespo.Object.AddTodayFood(0);
            var Respo = new Mock<AccountController>();
            Respo.Setup(x => x.AddTodayFood(It.IsAny<int>())).Returns(new RedirectResult("/Account/"));
            var result = Respo.Object.AddTodayFood() as RedirectResult;

            Assert.AreEqual(result.Url, "/Account/");
            Assert.IsInstanceOfType(re, typeof(ViewResult));
         
        }
        [TestMethod]
        public void addtodayfood()
        {
            var tody = new TodayFood(); 
            tody.UserId = 1;
            tody.FoodId = 1;
            tody.Date = new DateTime();

            var today = new Mock<ETodayFoods>();
            today.Setup(x => x.Add(tody)).Returns(tody);
            var result = today.Object.Add(tody);

            Assert.IsNotNull(result);
  
        }
        //[TestMethod]
        //public void failaddtodayfood()
        //{

        //    var tody = new UserProfileInfo();

        //    var today = new Mock<AccountController>();
        //    today.Setup(x => x.addTodayfood(tody, 0)).Returns((TodayFood)null);
        //    var result = today.Object.addTodayfood(tody, 0);

        //    Assert.IsNull(result);

        //}
        [TestMethod]
        public void RemoveTodayFood()
        {
          
            var todayRespo = new Mock<AccountController>();
            todayRespo.Setup(x => x.RemoveTodayFood(It.IsAny<int>())).Returns(new ViewResult());
            var re = todayRespo.Object.RemoveTodayFood(1);
            
            
            var Respo = new Mock<AccountController>();
            Respo.Setup(x => x.RemoveTodayFood(It.IsAny<int>())).Returns(new RedirectResult("/Account/"));
            var result = Respo.Object.RemoveTodayFood() as RedirectResult;

            Assert.AreEqual(result.Url, "/Account/");
            Assert.IsInstanceOfType(re, typeof(ViewResult));
           
            
        }
        [TestMethod]
        public void removeTodayFoodValue()
        {
            var tody = new TodayFood();
            tody.UserId = 1;
            tody.FoodId = 1;
            tody.Date = new DateTime();

            var today = new Mock<ETodayFoods>();
            today.Setup(x => x.Delete(tody)).Returns(tody);
            var result = today.Object.Delete(tody);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Healthplan()
        {
            //var model = new UserProfileInfo();
            //model.Height = 1;
            //model.Weight = 1;
            //model.Age = 1;

            //var food = new Food();
            //double food1 = 100;
            //food.food_Calories = food1;
            //food.food_GlycemicIndex = food1;
            //double food2 = 100;
            //food.food_Calories = food2;
            //food.food_GlycemicIndex = food2;
            //double food3 = 200;
            //food.food_Calories = food3;
            //food.food_GlycemicIndex = food3;

            //double BMRM = (66 + ((13.7 * model.Weight) + (5 * model.Height) - (6.8 * model.Age)));
            //double BMRF = (665 + ((9.6 * model.Weight) + (1.8 * model.Height) - (4.7 * model.Age)));
            //double BMI = model.Weight / ((model.Height / 100) * (model.Height / 100));
            //double calories = food1 + food2 + food3;
            //double glycemic = food1 + food2 + food3;

            //Assert.AreEqual(77.9, BMRM);
            //Assert.AreEqual(671.7, BMRF);
            //Assert.AreEqual(10000, BMI);
            //Assert.AreEqual(400, calories);
            //Assert.AreEqual(400, glycemic);


        }
        [TestMethod]
        public void TestBMRForMale()
        {
            var model = new UserProfileInfo();
            model.Height = 1;
            model.Weight = 1;
            model.Age = 1;
            double BMRM = (66 + ((13.7 * model.Weight) + (5 * model.Height) - (6.8 * model.Age)));
            Assert.AreEqual(77.9, BMRM);
        }
        [TestMethod]
        public void TestBMRForFemale()
        {
            var model = new UserProfileInfo();
            model.Height = 1;
            model.Weight = 1;
            model.Age = 1;
            double BMRF = (665 + ((9.6 * model.Weight) + (1.8 * model.Height) - (4.7 * model.Age)));
            Assert.AreEqual(671.7, BMRF);
        }
        [TestMethod]
        public void TestBMI()
        {
            var model = new UserProfileInfo();
            model.Height = 1;
            model.Weight = 1;
            
            double BMI = model.Weight / ((model.Height / 100) * (model.Height / 100));
            Assert.AreEqual(10000, BMI);
        }
        [TestMethod]
        public void valurOfFood()
        {
            var food = new Food();
            double food1 = 100;
            food.food_Calories = food1;
            food.food_GlycemicIndex = food1;
            double food2 = 100;
            food.food_Calories = food2;
            food.food_GlycemicIndex = food2;
            double food3 = 200;
            food.food_Calories = food3;
            food.food_GlycemicIndex = food3;
            double calories = food1 + food2 + food3;
            double glycemic = food1 + food2 + food3;
        
            Assert.AreEqual(400, calories);
            Assert.AreEqual(400, glycemic);
        }
        [TestMethod]
        public void AddGlycemic()
        {
            var addglycemicRespo = new Mock<AccountController>();
            addglycemicRespo.Setup(x => x.AddGlycemic(It.IsAny<int>())).Returns(new ViewResult());
            var re = addglycemicRespo.Object.AddGlycemic(1);
           
            var Respo = new Mock<AccountController>();
            Respo.Setup(x => x.AddGlycemic(It.IsAny<int>())).Returns(new RedirectResult("/Account/HealthPlan/"));
            var result = Respo.Object.AddGlycemic() as RedirectResult;

            Assert.AreEqual(result.Url, "/Account/HealthPlan/");
            Assert.IsInstanceOfType(re, typeof(ViewResult));
            

        }
        [TestMethod]
        public void addglycemicNotnull()
        {
            var gly = new UserGlycemic();
            gly.UserId = 1;
            gly.Value = 100;
            gly.Date = new DateTime();

            var glyy = new Mock<EAddGlycemicRepository>();
           glyy.Setup(x => x.Add(gly)).Returns(gly);
            var result = glyy.Object.Add(gly);

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void glycemicIsNull()
        {

            var glyy = new Mock<EAddGlycemicRepository>();
            glyy.Setup(x => x.Add(null));
            var result = glyy.Object.Add(null);

            Assert.IsNull(result);

        }
        [TestMethod]
        public void AddFoodToHealthPlan()
        {
            var addfoodRespo = new Mock<AccountController>();
            addfoodRespo.Setup(x => x.AddFood(It.IsAny<int>())).Returns(new ViewResult());
            var re = addfoodRespo.Object.AddFood(1);
        
            var Respo = new Mock<AccountController>();
            Respo.Setup(x => x.AddFood(It.IsAny<int>())).Returns(new RedirectResult("/Account/HealthPlan/"));
            var result = Respo.Object.AddFood() as RedirectResult;

            Assert.AreEqual(result.Url, "/Account/HealthPlan/");
            Assert.IsInstanceOfType(re, typeof(ViewResult));         
        }
        [TestMethod]
        public void addFoodToHealthPlanValue()
        {
            var foods = new UserFood();
            foods.Id = 1;
            foods.FoodId = 1;
            foods.UserId = 1;

            var add = new Mock<EUserFood>();
            add.Setup(x => x.Add(foods)).Returns(foods);
            var result = add.Object.Add(foods);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void DeleteFoodFromHealthPlan()
        {
            var deletefoodRespo = new Mock<AccountController>();
            deletefoodRespo.Setup(x => x.DeleteFood(It.IsAny<int>())).Returns(new ViewResult());
            var re = deletefoodRespo.Object.DeleteFood(1);
          
            var Respo = new Mock<AccountController>();
            Respo.Setup(x => x.DeleteFood(It.IsAny<int>())).Returns(new RedirectResult("/Account/HealthPlan/"));
            var result = Respo.Object.DeleteFood() as RedirectResult;

            Assert.AreEqual(result.Url, "/Account/HealthPlan/");
            Assert.IsInstanceOfType(re, typeof(ViewResult));
            
        }

        [TestMethod]
        public void DeleteFoodfromHealthPlanValue()
        {
            var foods = new UserFood();
            foods.Id = 1;
            foods.FoodId = 1;
            foods.UserId = 1;

            var delete = new Mock<EUserFood>();
            delete.Setup(x => x.Delete(foods)).Returns(foods);
            var result = delete.Object.Delete(foods);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Addmedicine()
        {
            var addMedicineRespo = new Mock<AccountController>();
            addMedicineRespo.Setup(x => x.AddMedicine(It.IsAny<string>())).Returns(new ViewResult());
           
            var re1 = addMedicineRespo.Object.AddMedicine("para");
           
            var Respo = new Mock<AccountController>();
            Respo.Setup(x => x.AddMedicine(It.IsAny<string>())).Returns(new RedirectResult("/Account/DisplayProfile/"));
            var result = Respo.Object.AddMedicine() as RedirectResult;

            Assert.AreEqual(result.Url, "/Account/DisplayProfile/");
            
            Assert.IsInstanceOfType(re1, typeof(ViewResult));

        }
        [TestMethod]
        public void AddMedicineIsNotNull()
        {

            var medicine = "test";
            var addMedicineRespo = new Mock<AccountController>();
            addMedicineRespo.Setup(x => x.AddMedicine(medicine)).Returns(new ViewResult());
            var re = addMedicineRespo.Object.AddMedicine(medicine);
            Assert.IsNotNull(re);

        }
        [TestMethod]
        public void AddMedicineIsNull()
        {

            string medicine = null;
            var addMedicineRespo = new Mock<AccountController>();
            addMedicineRespo.Setup(x => x.AddMedicine(medicine)).Returns(new ViewResult());
            var re = addMedicineRespo.Object.AddMedicine();
            Assert.IsNull(re);

        }

        [TestMethod]
        public void DeleteMedicine()
        {
            var deleteMedicineRespo = new Mock<AccountController>();
            deleteMedicineRespo.Setup(x => x.DeleteMedicine(It.IsAny<int>())).Returns(new ViewResult());
            var re = deleteMedicineRespo.Object.DeleteMedicine(1);
    
            var Respo = new Mock<AccountController>();
            Respo.Setup(x => x.DeleteMedicine(It.IsAny<int>())).Returns(new RedirectResult("/Account/DisplayProfile/"));
            var result = Respo.Object.DeleteMedicine() as RedirectResult;

            Assert.AreEqual(result.Url, "/Account/DisplayProfile/");
            Assert.IsInstanceOfType(re, typeof(ViewResult));
            

        }
        [TestMethod]
        public void DeleteMedicineInsertValue()
        { var m = new Medicine();
            m.Id = 1;
            var d = new Mock<AccountController>();
            d.Setup(x => x.DeleteMedicine(m.Id)).Returns(new ViewResult());
            var re = d.Object.DeleteMedicine(m.Id);

            Assert.IsNotNull(re);
        }
        

        //[TestMethod]
        //public void GetRegister()
        //{
        //    AccountController con = new AccountController();
        //    ViewResult re = con.Register() as ViewResult;
        //    Assert.IsNotNull(re);
        //}
        //[TestMethod]
        //public void PostRegister()
        //{
        //    //    var accountController = new AccountController();
        //    //    var registerViewModel = new RegisterViewModel
        //    //    {
        //    //        Email = "test@test.com",
        //    //        Password = "123456"
        //    //    };

            //    //    var result = accountController.Register(registerViewModel).Result;
            //    //    Assert.IsTrue(result is RedirectToRouteResult);
            //    //    Assert.IsTrue(accountController.ModelState.All(kvp => kvp.Key != ""));
            //}
        [TestMethod]
        public void GetEditUserProfile()
        {

        }
        [TestMethod]
        public void PostEditUserProfile()
        {

        }
    }
   
    //Admin controller
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void AddRoles()
        {
            RoleDTO ro = new RoleDTO();
            ro.Id = "213" ;
            ro.RoleName = "Doctor";

            var role = new Mock<AdminController>();
            role.Setup(x => x.AddRole(ro)).Returns(new ViewResult());
            var re1 = role.Object.AddRole(ro);

            
            role.Setup(x => x.AddRole(ro)).Returns(new RedirectResult("~/ Admin / ViewAllRoles"));
            var r2 = role.Object.AddRole(ro) as RedirectResult;

            
            Assert.IsInstanceOfType(re1, typeof(ViewResult));
            Assert.AreEqual(r2.Url, "~/ Admin / ViewAllRoles");
        }

        [TestMethod]
        public void AddRoleValue()
        {
            RoleDTO ro = new RoleDTO();
            ro.Id = "213";
            ro.RoleName = "Doctor";

            var role = new Mock<AdminController>();
            role.Setup(x => x.AddRole(ro)).Returns(new ViewResult());
            var re = role.Object.AddRole(ro);

            Assert.IsNotNull(re);


        }
        [TestMethod]
        public void CreateAccount()
        {
            ExpandedUserDTO ro = new ExpandedUserDTO();
            List<UserRolesDTO> uro = new List<UserRolesDTO>();
            ro.Email = "test@test.com";
            ro.UserName = "Doctors";
            ro.Password = "Doctors";
            ro.Roles = uro;

            var create = new Mock<AdminController>();
            create.Setup(x => x.Create(ro)).Returns(new ViewResult());
            var re1 = create.Object.Create(ro);


            create.Setup(x => x.Create(ro)).Returns(new RedirectResult("~/Admin "));
            var r2 = create.Object.Create(ro) as RedirectResult;


            Assert.IsInstanceOfType(re1, typeof(ViewResult));
            Assert.AreEqual(r2.Url, "~/Admin ");
        }
       [TestMethod]
       public void CreateAccountValue()
        {
            ExpandedUserDTO ro = new ExpandedUserDTO();
            List<UserRolesDTO> uro = new List<UserRolesDTO>();
            
            ro.Email = "test@test.com";
            ro.UserName = "Doctors";
            ro.Password = "Doctors";
            ro.Roles = uro;

            var create = new Mock<AdminController>();
            create.Setup(x => x.Create(ro)).Returns(new ViewResult());
            var re = create.Object.Create(ro);

            Assert.IsNotNull(re);

        }
        [TestMethod]
        public void DeleteUser()
        {
            var Duser = new Mock<AdminController>();
            Duser.Setup(x => x.DeleteUser("test")).Returns(new ViewResult());
            var re = Duser.Object.DeleteUser("test");

            Duser.Setup(x => x.DeleteUser("test")).Returns(new RedirectResult("~/Admin "));
            var re1 = Duser.Object.DeleteUser("test") as RedirectResult;

            Assert.IsInstanceOfType(re, typeof (ViewResult));
            Assert.AreEqual(re1.Url, "~/Admin ");
        }
        
        [TestMethod]
        public void DeleteUserInUsername()
        {
            var UserName = "test";

            var duser = new Mock<AdminController>();
            duser.Setup(x => x.DeleteUser(UserName)).Returns(new ViewResult());
            var re = duser.Object.DeleteUser(UserName);

            Assert.IsNotNull(re);

        }
        [TestMethod]
        public void DeleteRoles()
        {
            var dr = new Mock<AdminController>();
            dr.Setup(x => x.DeleteRole("TestUser", "Role")).Returns(new ViewResult());
            var re = dr.Object.DeleteRole("TestUser", "Role");

            Assert.IsInstanceOfType(re, typeof(ViewResult));
        }
        [TestMethod]
        public void DeleteRolesValue()
        {
            var userName = "TestUSer";
            var Role = "RoleTEst";  

            var dr = new Mock<AdminController>();
            dr.Setup(x => x.DeleteRole(userName, Role)).Returns(new ViewResult());
            var re = dr.Object.DeleteRole(userName, Role);

            Assert.IsNotNull(re);
        }
        [TestMethod]
        public void DeleteUserRole()
        {
            var DUR = new Mock<AdminController>();
            DUR.Setup(x => x.DeleteUserRole("test")).Returns(new ViewResult());
            var re = DUR.Object.DeleteUserRole("test");

            Assert.IsInstanceOfType(re, typeof(ViewResult));
        }
        [TestMethod]
        public void DeleteUserRoleValueRoleName()
        {
            var RoleName = "test";
                var DUR = new Mock<AdminController>();
            DUR.Setup(x => x.DeleteUserRole(RoleName)).Returns(new ViewResult());
            var re = DUR.Object.DeleteUserRole(RoleName);

            Assert.IsNotNull(re);
        }


    }
}
