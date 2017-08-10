using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diabetes1.Models
{
    public class AccountInfoViewModel
    {
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Bloodsugar")]
        public double Bloodsugar { get; set; }

        [Required]
        [Display(Name = "Height")]
        public double Height { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public double Weight { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public bool Gender { get; set; }
        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "ZipCode")]
        public int ZipCode { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}