using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public interface IRole
    {
        IQueryable<RoleDTO> role { get; }
        RoleDTO Add(RoleDTO role);
        RoleDTO Edit(RoleDTO role);
        void Delete(RoleDTO role);
        RoleDTO Find(int? id);
    }
}