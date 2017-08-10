using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public class ETodayFoods : ITodayFoods
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public IQueryable<TodayFood> todayfood
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual TodayFood Add(TodayFood todayfood)
        {
            
            db.TodayFoods.Add(todayfood);
            db.SaveChanges();

            return todayfood;
        }

        public virtual TodayFood Delete(TodayFood todayfood)
        {
            db.TodayFoods.Remove(todayfood);
            db.SaveChanges();

            return todayfood;
        }

        public TodayFood Edit(TodayFood todayfood)
        {
            throw new NotImplementedException();
        }

        public TodayFood Find(int? id)
        {
            throw new NotImplementedException();
        }
    }
}