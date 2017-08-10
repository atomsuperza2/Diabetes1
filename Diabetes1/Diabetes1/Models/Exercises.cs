using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diabetes1.Models
{
    public class Exercises
    {
        [Key]
        public int id { get; set; }
        public string symptom { get; set; }
        [AllowHtml]
        public string suggestion { get; set; }
        [AllowHtml]
        public string ProperExercise { get; set; }
        [AllowHtml]
        public string ExerciseInappropriate { get; set; }
        [AllowHtml]
        public string StepExercise { get; set; } 
    }
}