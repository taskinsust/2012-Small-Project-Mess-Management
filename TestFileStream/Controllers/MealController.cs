using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using TestFileStream.Entity;
using TestFileStream.Models;
using NHibernate;
namespace TestFileStream.Controllers
{
    public class MealController : Controller
    {
        MembersModel mM = new MembersModel();
        DepositModel dM = new DepositModel();
        MealModel mMS = new MealModel();
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
        public ActionResult MealCount()
        {
            ViewBag.Members = dM.MemberList();
            //ViewBag.Members = new SelectList(dM.MemberList(), "Id", "FName");
            IList<Members> memberList = mM.ViewAllMembers();
            ViewBag.ALLMEMBERS = memberList; 
            return View();
        }

        [HttpPost]
        public ActionResult MealCount(Meal meal, string[] selectedMemberId, long Members = 0) 
        {
            try
            {
                for (int i = 0; i < selectedMemberId.Count(); i++)
                {
                    Members members = new Members();
                    members = mM.GetById(Convert.ToInt64(selectedMemberId[i]));
                    meal.Members = members;
                    mMS.Save(meal);
                }
            }
            catch(Exception ex)
            {
                throw ex; 
            }

            return RedirectToAction("ViewMeal");
        }

        public ActionResult ViewMeal()
        {
            IList<Meal> GetMealList = mMS.GetMealList();
            return View(GetMealList);
        }
        public ActionResult EditMeals(long Id)
        {
            Meal meal = mMS.GetById(Id);
            return View(meal);
        }

        [HttpPost]
        public ActionResult EditMeals(Meal meal)
        {
            mMS.Update(meal);
            return RedirectToAction("ViewMeal");
        }

        public ActionResult DeleteMeal(long Id)
        {
            Meal meal = mMS.GetById(Id);
            return View(meal);
        }

        [HttpPost]
        public ActionResult DeleteMeal(Meal meal)
        {
            mMS.DeleteMembers(meal);
            return RedirectToAction("ViewMeal");
        }

        public ActionResult DEtailsMeal(long Id)
        {
            Meal meal = mMS.GetById(Id);
            Members members = meal.Members;
            if(members !=null)
            {
                ViewBag.NAME = members.FName;    
            }
            
            return View(meal);
        }
        public ActionResult Search(string sFname, DateTime? Fdate = null, DateTime? Tdate = null)
        {
            try
            {
                IList<Meal> getSearchResult = mMS.GetSearchResult(sFname, Fdate, Tdate);
                return View("ViewMeal", getSearchResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
