using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Mapping;
using TestFileStream.Entity;

namespace TestFileStream.Mapping
{
    public partial class MembersMap : ClassMap<Members> 
    {
        public MembersMap() 
        {
            Id(x => x.Id);
            Map(x => x.FName);
            Map(x => x.LName);
            Map(x => x.Address);
            Map(x => x.Contact);

            
            HasMany(x => x.Meals).Cascade.SaveUpdate().Inverse();
            HasMany(x => x.Deposits).Cascade.SaveUpdate().Inverse();
            HasMany(x => x.Bazars).Cascade.SaveUpdate().Inverse();
        }
    }
}