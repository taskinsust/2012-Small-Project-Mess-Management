using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestFileStream.Entity
{
    public partial class Members
    {
        public Members() 
        {
            this.Meals = new List<Meal>();
            this.Deposits = new List<Deposit>();
            this.Bazars = new List<Bazar>();
        }
        public virtual long Id { get; set; }
        [Required, Display(Name = "First Name  ")]
        public virtual string FName { get; set; }
        [Required, Display(Name = "Last Name ")]
        public virtual string LName { get; set; }
        [Required, Display(Name = "Address ")]
        public virtual string Address { get; set; }
        [Required, Display(Name = "Mobile No ")]
        public virtual string Contact { get; set; }

        public virtual IList<Meal> Meals { get; set; }
        public virtual IList<Deposit> Deposits { get; set; }
        public virtual IList<Bazar> Bazars { get; set; }
    }
}