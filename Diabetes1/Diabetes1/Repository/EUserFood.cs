using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public class EUserFood : IUserFood
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public IQueryable<UserFood> userfood
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual UserFood Add(UserFood userfood)
        {
            db.UserFoods.Add(userfood);
            db.SaveChanges();

            return userfood;
        }

        public virtual UserFood Delete(UserFood userfood)
        {
            db.UserFoods.Remove(userfood);
            db.SaveChanges();

            return userfood;
        }

        public UserFood Find(int? id)
        {
            throw new NotImplementedException();
        }
    }
}