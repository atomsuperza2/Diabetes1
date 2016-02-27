using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Diabetes1.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual UserProfileInfo UserProfileInfo { get; set; }
        public virtual UserAddress UserAddress { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class UserProfileInfo
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string DiabetesType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        //public DateTime StartTreatment { get; set; }
        public string localImage { get; set; }
       
    }

    public class UserAddress
    {
        public int id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<UserProfileInfo> UserProfileInfo { get; set; }
        public System.Data.Entity.DbSet<UserAddress>UserAddress { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.Food> Foods { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.Activity> Activities { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.Nutritionists> Nutritionists { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.UserFood> UserFoods { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.UserActivity> UserActivities { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.UserGlycemic> UserGlycemics { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.Medicine> Medicines { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.TodayActivity> TodayActivities { get; set; }

        public System.Data.Entity.DbSet<Diabetes1.Models.TodayFood> TodayFoods { get; set; }
    }
}