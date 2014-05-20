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

    public class DepositController : Controller
    {
        DepositModel dM = new DepositModel();
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DepositAdd() 
        {
            ViewBag.Members = new SelectList(dM.MemberList(), "Id", "FName");
            return View();
        }
        
        [HttpPost]
        public ActionResult DepositAdd(Deposit deposit, long Members= 0) 
        {
            Members member = new Members();
            member = dM.GetById(Members);
            deposit.Members = member;
            string monthName = "";
            int monthNameIntValue = deposit.DepositDate.Month;
            switch (monthNameIntValue)
            {
                case 1: monthName = "January";
                    break;
                case 2: monthName = "February";
                    break;
                case 3: monthName = "March";
                    break;
                case 4: monthName = "April";
                    break;
                case 5: monthName = "May";
                    break;
                case 6: monthName = "June";
                    break;
                case 7: monthName = "July";
                    break;
                case 8: monthName = "August";
                    break;
                case 9: monthName = "September";
                    break;
                case 10: monthName = "October";
                    break;
                case 11: monthName = "November";
                    break;
                case 12: monthName = "December";
                    break;
                default: monthName = "";
                    break;
            }
            deposit.MonthName = monthName;
            dM.Save(deposit);
            return RedirectToAction("ViewDepositList");
        }
        public ActionResult ViewDepositList()
        {
            IList<Deposit> DepositList = dM.ViewAllBazarList();
            return View(DepositList);
        }
        public ActionResult EditDeposit(long Id)
        {
            Deposit deposit = dM.GetByDepositId(Id);
            return View(deposit);
        }

        [HttpPost]
        public ActionResult EditDeposit(Deposit deposit)
        {
            dM.Update(deposit);
            return RedirectToAction("ViewDepositList");
        }

        public ActionResult DeleteDeposit(long Id)
        {
            Deposit deposit = dM.GetByDepositId(Id);
            return View(deposit);
        }

        [HttpPost]
        public ActionResult DeleteDeposit(Deposit deposit)
        {
            dM.DeleteDeposit(deposit);
            return RedirectToAction("ViewDepositList");
        }

        public ActionResult DetailsDeposit(long Id)
        {
            Deposit deposit = dM.GetByDepositId(Id);
            Members members = deposit.Members;
            ViewBag.NAME = members.FName;
            return View(deposit);
        }

        public ActionResult Search(string sFname , DateTime Fdate ,DateTime Tdate)
        {
            try
            {
                IList<Deposit> getSearchResult = dM.GetSearchResult(sFname, Fdate, Tdate);
                return View("ViewDepositList", getSearchResult);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
