using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestFileStream.Entity;
using FluentNHibernate.Mapping;
using NHibernate;
namespace TestFileStream.Mapping
{
    public partial class FileCollectionMap:ClassMap<FileCollection>
    {
        public FileCollectionMap() 
        {
            Id(x => x.Id);
            Map(x => x.FileName);
            Map(x => x.FileData).CustomSqlType("VARBINARY (MAX) FILESTREAM").Length(2147483647);
            Map(x => x.HashData);
            Map(x => x.Version);
        }
    }
}