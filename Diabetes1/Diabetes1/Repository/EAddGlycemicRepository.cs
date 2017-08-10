using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public class EAddGlycemicRepository : IAddBlycemicRepository
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public IQueryable<UserGlycemic> userglycemic
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual UserGlycemic Add(UserGlycemic userglycemic)
        {
            db.UserGlycemics.Add(userglycemic);
            db.SaveChanges();

            return userglycemic;
        }

        public void Delete(UserGlycemic userglycemic)
        {
            throw new NotImplementedException();
        }

        public UserGlycemic Edit(UserGlycemic userglycemic)
        {
            throw new NotImplementedException();
        }

        public UserGlycemic Find(int? id)
        {
            throw new NotImplementedException();
        }
    }
}