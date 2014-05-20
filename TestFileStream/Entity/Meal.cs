using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestFileStream.Entity
{
    public partial class Meal
    {
        public virtual long Id { get; set; }
        [Required, Display(Name = "Meal Date")]
        [DataType(DataType.Date)]
        public virtual DateTime MealDate { get; set; }
        [Required, Display(Name = "Meal Count")]
        public virtual double MealCount { get; set; } 

        public virtual Members Members { get; set; }
    }
}