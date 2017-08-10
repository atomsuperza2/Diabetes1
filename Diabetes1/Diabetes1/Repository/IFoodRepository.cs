using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;
namespace Diabetes1.Repository
{
    public interface IFoodRepository
    {
        IQueryable<Food> food { get; }
        Food Add(Food food);
        Food Edit(Food food);
        void Delete(Food food);
        Food Find(int? id);
    }
}