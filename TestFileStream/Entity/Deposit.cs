using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestFileStream.Entity
{
    public partial class Deposit
    {
        public virtual long Id { get; set; }
        [DataType(DataType.Date)]
        public virtual DateTime DepositDate { get; set; }
        [Required ,Display(Name = "Month")]
        public virtual String MonthName { get; set; }
        [Required, Display(Name = "Deposit Amount ")]
        public virtual double DepositAmmount { get; set; }

        public virtual Members Members { get; set; }
    }
}