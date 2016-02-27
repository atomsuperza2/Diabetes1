using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Diabetes1.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MedicineName { get; set; }
        public int Take { get; set; }
        public DateTime Today { get; set; }

    }
}