using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;


namespace Diabetes1.Repository
{
    public interface IUserFood
    {
        IQueryable<UserFood> userfood { get; }
        UserFood Add(UserFood userfood);
        UserFood Delete(UserFood userfood);
        UserFood Find(int? id);
    }
}