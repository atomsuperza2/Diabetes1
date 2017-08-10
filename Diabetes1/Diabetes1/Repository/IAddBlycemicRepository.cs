using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public interface IAddBlycemicRepository
    {
        IQueryable<UserGlycemic> userglycemic { get; }
        UserGlycemic Add(UserGlycemic userglycemic);
        UserGlycemic Edit(UserGlycemic userglycemic);
        void Delete(UserGlycemic userglycemic);
        UserGlycemic Find(int? id);
    }
}