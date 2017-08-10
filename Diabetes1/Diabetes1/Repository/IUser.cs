using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public interface IUser
    {
        IQueryable<ExpandedUserDTO> user { get; }
        ExpandedUserDTO Add(ExpandedUserDTO user);
        ExpandedUserDTO Edit(ExpandedUserDTO user);
        void Delete(ExpandedUserDTO user);
        ExpandedUserDTO Find(int? id);
    } 
}