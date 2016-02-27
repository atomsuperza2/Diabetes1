using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diabetes1.Models
{
    public class Food
    {

        [Key]
        public int id_food { get; set; }
        public string food_name { get; set; }
        public double food_Calories { get; set; }
        public double food_GlycemicIndex { get; set; }
    }
}