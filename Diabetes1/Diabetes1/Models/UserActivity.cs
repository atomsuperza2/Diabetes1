using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diabetes1.Models
{
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }
    }
}