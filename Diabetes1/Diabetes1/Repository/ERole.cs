using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public class ERole : IRole
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public IQueryable<RoleDTO> role
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual RoleDTO Add(RoleDTO role)
        {
            //db.Roles.Add(role);
            //db.SaveChanges();

            return role;
        }

        public void Delete(RoleDTO role)
        {
            throw new NotImplementedException();
        }

        public RoleDTO Edit(RoleDTO role)
        {
            throw new NotImplementedException();
        }

        public RoleDTO Find(int? id)
        {
            throw new NotImplementedException();
        }
    }
}