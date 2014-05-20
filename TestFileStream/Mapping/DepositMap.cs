using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Mapping;
using TestFileStream.Entity;
namespace TestFileStream.Mapping
{
    public partial class DepositMap : ClassMap<Deposit> 
    {
        public DepositMap() 
        {
            Id(x => x.Id);
            Map(x => x.DepositDate);
            Map(x => x.MonthName);
            Map(x => x.DepositAmmount);

            References(x => x.Members).LazyLoad();
        }
    }
}