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
    public class ReportController : Controller
    {
        MembersModel mM = new MembersModel();
        DepositModel dM = new DepositModel();
        MealModel mMS = new MealModel();
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
        public ActionResult DetailsReportPerUser()
        {
            ViewBag.Members = new SelectList(dM.MemberList(), "Id", "FName");

            return View("DetailsReportPerUser");
        }
        [HttpPost]
        public ActionResult DetailsReportPerUser(DateTime Fdate, DateTime Tdate, long Members = 0)
        {
            ViewBag.Members = new SelectList(dM.MemberList(), "Id", "FName");
            if(Fdate.Month != Tdate.Month)
            {
                ViewBag.ErrorMessage = "Select Same Month For Report Generation !!!";
                    return View ("DetailsReportPerUser");
            }
            else
            {
            double approximateMealCanEat = 0;
           
            Members members = mM.GetById(Members);

            double sumMealCountByPerson = mMS.GetMeal(members.Id, Fdate ,Tdate);
            double sumDepositAmountByPerson = dM.GetDeposit(members.Id, Fdate, Tdate);

            double totalDeposit = dM.GetTotalDeposit(Fdate, Tdate);
            double totalBazarCost = bM.GetTotalBazarCost(Fdate, Tdate);
            double totalMealCount = mMS.GetTotalMeal(Fdate, Tdate);

            double mealRate = (totalBazarCost / totalMealCount);
            if (double.IsNaN(mealRate) ) 
            {
                mealRate = 0;
            }
            double untilCostForMeal = (mealRate * sumMealCountByPerson);
            double remainAmount = (sumDepositAmountByPerson - untilCostForMeal);

            if (remainAmount > 0)
            {
                approximateMealCanEat = (remainAmount / mealRate);
            }
            else 
            {
                 approximateMealCanEat = 0;  
            }

            ViewBag.MEALCOUNTBYPERSON = sumMealCountByPerson;
            ViewBag.DEPOSITAMOUNTBYPERSON = sumDepositAmountByPerson;
            ViewBag.MEMBER = members;
            ViewBag.TOTALBAZARCOST = totalBazarCost;
            ViewBag.MEALRATE = mealRate;
            ViewBag.COSTFORMEAL = untilCostForMeal;
            ViewBag.REMAIN = remainAmount;
            ViewBag.APPMEALCANEAT = approximateMealCanEat;

            return View("DetailsReportPerUser");
            }

        }
        public ActionResult CalculateDepositAndCost()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CalculateDepositAndCost(DateTime Fdate , DateTime Tdate)
        {

            double totalDeposit = dM.GetTotalDeposit(Fdate, Tdate);
            double totalBazarCost = bM.GetTotalBazarCost(Fdate, Tdate);
            double totalMealCount = mMS.GetTotalMeal(Fdate, Tdate);
            double mealRate = (totalBazarCost / totalMealCount);

            ViewBag.DEPOSIT = totalDeposit;
            ViewBag.TOTALBAZARCOST = totalBazarCost;
            ViewBag.TOTALMEAL = totalMealCount;
            ViewBag.REMAINBALANCE = (totalDeposit - totalBazarCost);
            ViewBag.MEALRATE = mealRate;

            return View();
        }
    }
}
