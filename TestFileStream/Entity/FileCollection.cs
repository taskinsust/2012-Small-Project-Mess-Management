using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestFileStream.Entity
{
    public partial class FileCollection
    {
        public virtual int Id { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FileData { get; set; }
        public virtual Guid HashData { get; set; }
        public virtual int Version { get; set; }

    }
}