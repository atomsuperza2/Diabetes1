using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diabetes1.Models
{
    public class Activity
    {
        [Key]
        public int id_activity { get; set; }
        public string activity_name { get; set; }
        public double activity_calories { get; set; }
    }
}