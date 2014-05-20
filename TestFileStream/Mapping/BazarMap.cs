using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Mapping;
using TestFileStream.Entity;
namespace TestFileStream.Mapping
{
    public partial class BazarMap : ClassMap<Bazar>
    {
        public BazarMap() 
        {
            Id(x => x.Id);
            Map(x => x.CostDate);
            Map(x => x.BazarAmmount);

            References(x => x.Members);
        }
    }
}