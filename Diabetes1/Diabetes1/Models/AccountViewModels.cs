using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Diabetes1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

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
        //[Required]
        //[Display(Name = "StartTreatment ")]
        //public DateTime StartTreatment { get; set; }
        //public ICollection<UserProfileInfo> UserProfilrInfo { get; set; }
    }

    //public class UserProfileDisplay
    //{
    //    [Required]
    //    [Display(Name = "UserName")]
    //    public string UserName { get; set; }


    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }


    //    [Required]
    //    [Display(Name = "FirstName")]
    //    public string FirstName { get; set; }

    //    [Required]
    //    [Display(Name = "LastName")]
    //    public string LastName { get; set; }

    //    [Required]
    //    [Display(Name = "Height")]
    //    public double Height { get; set; }

    //    [Required]
    //    [Display(Name = "Weight")]
    //    public double Weight { get; set; }
    //    [Required]
    //    [Display(Name = "Gender")]
    //    public bool Gender { get; set; }
    //    [Required]
    //    [Display(Name = "Age")]
    //    public int Age { get; set; }
    //    [Required]
    //    [Display(Name = "Address")]
    //    public string Address { get; set; }
    //    [Required]
    //    [Display(Name = "City")]
    //    public string City { get; set; }
    //    [Required]
    //    [Display(Name = "ZipCode")]
    //    public int ZipCode { get; set; }
    //}

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}