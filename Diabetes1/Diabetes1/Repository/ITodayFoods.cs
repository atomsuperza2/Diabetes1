using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;
namespace Diabetes1.Repository
{
    public interface ITodayFoods
    {
        IQueryable<TodayFood> todayfood { get; }
        TodayFood Add(TodayFood todayfood);
        TodayFood Edit(TodayFood todayfood);
        TodayFood Delete(TodayFood todayfood);
        TodayFood Find(int? id);
    }
}