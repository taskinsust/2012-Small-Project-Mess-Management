using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFileStream.Entity;
using TestFileStream.Models;

namespace TestFileStream.Controllers
{
    public class BazarController : Controller
    {
        DepositModel dM = new DepositModel();
        BazarModel bM = new BazarModel();
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
                        .ExposeConfiguration(BuildSchema)
                  .BuildSessionFactory();
                }

                return sessionFactory;
            }

            private static void BuildSchema(NHibernate.Cfg.Configuration obj)
            {
                new SchemaExport(obj).Execute(true, true,false);
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

        public ActionResult AddBazarCost() 
        {
            ViewBag.Members = new SelectList(dM.MemberList(), "Id" ,"FName");
            return View();
        }

        [HttpPost]
        public ActionResult AddBazarCost(Bazar bazar ,long Members = 0) 
        {
            Members member = new Members();
            member = dM.GetById(Members);
            bazar.Members = member;
            bM.Save(bazar);
            return RedirectToAction("ViewBazarList");

        }
        public ActionResult ViewBazarList() 
        {
            IList<Bazar> BazarList = bM.ViewAllBazarList();
            return View(BazarList);
        }
        public ActionResult EditBazars(long Id)
        {
            Bazar bazar = bM.GetById(Id);
            return View(bazar);
        }

        [HttpPost]
        public ActionResult EditBazars(Bazar bazar)
        {
            bM.Update(bazar);
            return RedirectToAction("ViewBazarList");
        }

        public ActionResult DeleteBazars(long Id)
        {
            Bazar bazar = bM.GetById(Id);
            return View(bazar);
        }

        [HttpPost]
        public ActionResult DeleteBazars(Bazar bazar)
        {
            bM.DeleteMembers(bazar);
            return RedirectToAction("ViewBazarList");
        }

        public ActionResult DEtailsBazars(long Id)
        {
            Bazar bazar = bM.GetById(Id);
            Members members = bazar.Members;
            if(members !=null)
            {
                ViewBag.NAME = members.FName;    
            }
            
            return View(bazar);
        }
        public ActionResult Search(string sFname, DateTime Fdate, DateTime Tdate)
        {
            try
            {
                IList<Bazar> getSearchResult = bM.GetSearchResult(sFname, Fdate, Tdate);
                return View("ViewBazarList", getSearchResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
