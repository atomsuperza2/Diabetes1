using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diabetes1.Models
{
    public class Exercises
    {
        [Key]
        public int id { get; set; }
        public string symptom { get; set; }
        public string suggestion { get; set; }
        public string ProperExercise { get; set; }
        public string ExerciseInappropriate { get; set; }
        public string StepExercise { get; set; } 
    }
}