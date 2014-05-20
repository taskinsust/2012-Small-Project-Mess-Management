using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Cfg;
using NHibernate.Criterion;
using TestFileStream.Controllers;
using TestFileStream.Entity;

namespace TestFileStream.Models
{
    public class BazarModel
    {
        public void Save(Bazar bazars)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(bazars);
                    transaction.Commit();

                }
            }
        }
        
        internal double GetTotalBazarCost(DateTime Fdate, DateTime Tdate)
        {
            ISession session = SessionFactory.OpenSession();
            var query = session.CreateSQLQuery("SELECT SUM(B.BazarAmmount) "+
                                                        "AS AMAOUNT FROM Bazar AS B "+ 
                                                        "INNER JOIN Members AS M "+
                                                        "ON B.Members_id = M.Id "+
                                                        "WHERE B.CostDate BETWEEN '"+ Fdate +"' AND '"+ Tdate +"'");
            var totalBazarCost = query.UniqueResult();
            return Convert.ToDouble(totalBazarCost);
        }

        internal IList<Bazar> ViewAllBazarList()
        {
            IList<Bazar> ReturnBazarList;
            ISession session = SessionFactory.OpenSession();
            ReturnBazarList = session.QueryOver<Bazar>().List<Bazar>();
            return ReturnBazarList;
        }

        internal Bazar GetById(long Id)
        {
            Bazar bazars = new Bazar();
            var session = SessionFactory.OpenSession();
            bazars = session.QueryOver<Bazar>().Where(x => x.Id == Id).SingleOrDefault<Bazar>();
            return bazars;
        }

        internal void Update(Bazar bazar)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(bazar);
                    transaction.Commit();
                }
            }
        }

        internal void DeleteMembers(Bazar bazar)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(bazar);
                    transaction.Commit();
                    session.Close();
                }
            }
        }

        internal IList<Bazar> GetSearchResult(string sFname, DateTime Fdate, DateTime Tdate)
        {
            IList<Bazar> returnBazarList;
            ISession session = SessionFactory.OpenSession();
            returnBazarList =
                session.CreateCriteria<Bazar>().Add(Restrictions.Between("CostDate", Fdate, Tdate)).
                CreateCriteria("Members").Add(Restrictions.Like("FName", sFname, MatchMode.Anywhere)).List<Bazar>();
            return returnBazarList;
        }
    }
}