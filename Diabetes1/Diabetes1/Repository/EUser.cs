using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diabetes1.Models;

namespace Diabetes1.Repository
{
    public class EUser : IUser
    {
        public IQueryable<ExpandedUserDTO> user
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ExpandedUserDTO Add(ExpandedUserDTO user)
        {
            throw new NotImplementedException();
        }

        public void Delete(ExpandedUserDTO user)
        {
            throw new NotImplementedException();
        }

        public ExpandedUserDTO Edit(ExpandedUserDTO user)
        {
            throw new NotImplementedException();
        }

        public ExpandedUserDTO Find(int? id)
        {
            throw new NotImplementedException();
        }
    }
}