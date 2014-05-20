using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using TestFileStream.Entity;
namespace TestFileStream.Mapping
{
    public partial class MealMap : ClassMap<Meal>
    {
        public MealMap() 
        {
            Id(x => x.Id);
            Map(x => x.MealDate);
            Map(x => x.MealCount);

            References(x => x.Members).LazyLoad();
        }
    }
}