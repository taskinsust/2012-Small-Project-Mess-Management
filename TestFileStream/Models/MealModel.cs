using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using TestFileStream.Entity;
using NHibernate;
using FluentNHibernate.Mapping;
using TestFileStream.Controllers;
namespace TestFileStream.Models
{
    public class MealModel
    {
        public void Save(Meal meal)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(meal);
                    transaction.Commit();

                }
            }
        }

        internal double GetMeal(long p, DateTime Fdate ,DateTime Tdate)
        {
            ISession session = SessionFactory.OpenSession();
            var query = session.CreateSQLQuery("SELECT SUM(MM.MealCount) " +
                "FROM Meal AS MM " +
                "INNER JOIN Members AS M " +
                "ON MM.Members_id = M.Id " +
                "WHERE MM.Members_id ="+p+"" +
                "and MM.MealDate between '" + Fdate + "' and '" + Tdate + "'");
                
            var sumMealCount =query.UniqueResult();
            return Convert.ToDouble(sumMealCount);
        }

        internal double GetTotalMeal(DateTime Fdate, DateTime Tdate)
        {
            ISession session = SessionFactory.OpenSession();
            var query = session.CreateSQLQuery("SELECT SUM(MC.MealCount) " +
                                                        "AS AMOUNT FROM Meal AS MC " +
                                                        "INNER JOIN Members AS M " +
                                                        "ON MC.Members_id = M.Id " +
                                                        "WHERE MC.MealDate BETWEEN '"+ Fdate +"' AND '"+ Tdate +"'");
            var totalMealCount = query.UniqueResult();
            return Convert.ToDouble(totalMealCount);
        }

        internal IList<Meal> GetMealList()
        {
            IList<Meal> returnMealList;
            ISession session = SessionFactory.OpenSession();
            returnMealList = session.QueryOver<Meal>().List<Meal>();
            return returnMealList;
        }

        internal Meal GetById(long Id)
        {
            Meal meal = new Meal();
            ISession session = SessionFactory.OpenSession();

            meal = session.Get<Meal>(Id);

            return meal;
        }

        internal void Update(Meal meal)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(meal);
                    transaction.Commit();
                }
            }
        }

        internal void DeleteMembers(Meal meal)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(meal);
                    transaction.Commit();
                    session.Close();
                }
            }
        }

        internal IList<Meal> GetSearchResult(string sFname, DateTime? Fdate = null, DateTime? Tdate = null)
        {
            IList<Meal> returnMealList;
            ISession session = SessionFactory.OpenSession();
            returnMealList =
                session.CreateCriteria<Meal>().Add(Restrictions.Between("MealDate", Fdate, Tdate)).
                CreateCriteria("Members").Add(Restrictions.Like("FName", sFname, MatchMode.Anywhere)).List<Meal>();
            return returnMealList;
        }
    }
}