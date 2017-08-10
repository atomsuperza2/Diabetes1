using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diabetes1.Models
{
    public class ViewUserDetail //view model
    {      

        public UserProfileInfo userInfo { get; set; }
      
        public UserAddress userAddress { get; set; }
       
        public UserGlycemic userGlycemic { get; set; }

        public Medicine userMedicine { get; set; }

    }
}