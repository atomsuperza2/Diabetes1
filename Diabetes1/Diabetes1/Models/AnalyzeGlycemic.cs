using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diabetes1.Models
{
    public class AnalyzeGlycemic
    {

            [Key]
            public int id { get; set; }
            public string name { get; set; }
            [AllowHtml]
            public string symptom { get; set; }
            [AllowHtml]
            public string cause { get; set; }
            [AllowHtml]
            public string TakeCare { get; set; }
        }
    }
