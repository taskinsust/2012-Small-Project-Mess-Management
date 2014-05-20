using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using TestFileStream.Entity;
using TestFileStream.Models;
namespace TestFileStream.Controllers
{
    public class SessionFactory
    {
        private static ISessionFactory sessionFactory;

        public static ISessionFactory GetSessionFactory()
        {
            if (sessionFactory == null)
            {
                sessionFactory = Fluently.Configure()
              .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x =>
                  x.Server(".")
                  .Database("MessManagement")
                  .Username("taskin")
                  .Password("123456")))
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<Members>())
                    //.ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();                
            }

            return sessionFactory;
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration obj)
        {
            new SchemaExport(obj).Execute(true, true, false);
        }

        public static ISession OpenSession()
        {
            return GetSessionFactory().OpenSession();
        }
    }
    public class MemberController : Controller
    {
        MembersModel mM = new MembersModel();

        public ActionResult Index()
        {
            var session = SessionFactory.GetSessionFactory();
            return View();
        }

        [HttpGet]
        public ActionResult MemberAdd() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult MemberAdd(Members members) 
        {
            mM.Save(members);
           return RedirectToAction("ViewAllMembers");
        }

        public ActionResult ViewAllMembers() 
        {
           IList<Members> memberList=  mM.ViewAllMembers();
           return View(memberList);
        }

        public ActionResult EditMembers(long Id) 
        {
           Members member= mM.GetById(Id);
           return View(member);
        }

        [HttpPost]
        public ActionResult EditMembers(Members member) 
        {
            mM.Update(member);
            return RedirectToAction("ViewAllMembers");
        }

        public ActionResult DeleteMembers(long Id) 
        {
            Members member = mM.GetById(Id);
            return View(member);
        }

        [HttpPost]
        public ActionResult DeleteMembers(Members member) 
        {
            mM.DeleteMembers(member);
            return RedirectToAction("ViewAllMembers");
        }

        public ActionResult DEtailsMembers(long Id ) 
        {
            Members member = mM.GetById(Id);
           return View(member);
        }
    }
}










