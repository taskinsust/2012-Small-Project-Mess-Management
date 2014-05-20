using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFileStream.Entity;
using TestFileStream.Mapping;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace TestFileStream.Controllers
{
    public class UdvashFileStreamController : Controller
    {
        //
        // GET: /UdvashFileStream/

        private class SessionFactory
        {
            private static ISessionFactory _sessionFactory;

            public static ISessionFactory GetSessionFactory() 
            {
                if(_sessionFactory==null)
                {
                    _sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.Server(".")
                        .Database("FileStreaming"))).Mappings(x => x.FluentMappings.AddFromAssemblyOf<FileCollection>()).BuildSessionFactory();
                
                }

                return _sessionFactory;
            }

            public static ISession OpenSession() 
            {

                return GetSessionFactory().OpenSession();
            }
        
        }




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFile() 
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(FileCollection Collection) 
        {

            Collection.FileName = Request.Files[2].FileName;
            var FileContent = Request.Files[0].InputStream;
            byte[] fileContent;
            //using (var content=FileContent.ReadByte())
            //{

            //    fileContent = content;
            //}

            return View();
        }

    }
}
