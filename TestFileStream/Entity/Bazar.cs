using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestFileStream.Entity
{
    public partial class Bazar
    {
        public virtual long Id { get; set; }
        [Required, Display(Name = "Date")]

        [DataType(DataType.Date)]
        public virtual DateTime CostDate { get; set; }
        [Required, Display(Name = " Cost Of Bazar")]
        public virtual double BazarAmmount { get; set; }

        public virtual Members Members { get; set; }
    }
}