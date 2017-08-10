using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Diabetes1.Models
{
    public class BloodSuggestion
    {
        [Key]
        public int id { get; set; }
        public string Bloodvalue { get; set; }
        [AllowHtml]
        public string Discription { get; set; }
        [AllowHtml]
        public string Cause { get; set; }
        [AllowHtml]
        public string  Management{ get; set; }
        
     
    }
}