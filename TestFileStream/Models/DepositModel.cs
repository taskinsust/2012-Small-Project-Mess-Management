using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using TestFileStream.Controllers;
using TestFileStream.Entity;
using NHibernate;
using FluentNHibernate.Cfg;

namespace TestFileStream.Models
{
    public class DepositModel
    {
        public void Save(Deposit deposits)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(deposits);
                    transaction.Commit();
                    
                }
            }
        }

        public IList<Members> MemberList()
        {
            IList<Members> memberList;
            using (ISession session = SessionFactory.OpenSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    memberList = session.QueryOver<Members>().List<Members>();
                }
            }
            return memberList;
        }

        public Members GetById(long Id)
        {
            Members member = new Members();
            ISession session = SessionFactory.OpenSession();
            
                member = session.Get<Members>(Id);
            
            return member;
        }

        private string GetMonth(int monthNameIntValue) 
        {
            string monthName = "";
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
            return monthName;
        }
        internal double GetDeposit(long p ,DateTime Fdate ,DateTime Tdate)
        {
            ISession session = SessionFactory.OpenSession();
            
            int monthNameIntValue = Fdate.Month;
            string monthName = GetMonth(monthNameIntValue);
            var query = session.CreateSQLQuery("SELECT SUM(D.DepositAmmount) "+
                                                        "AS AMAOUNT FROM Deposit AS D "+ 
                                                        "INNER JOIN Members AS M "+
                                                        "ON D.Members_id = M.Id "+
                                                            "WHERE D.Members_id = "+ p +""+
                                                            "AND D.MonthName = '" + monthName +"'");

           // query.SetParameter("p" , p);
            var sumDepositAmount = query.UniqueResult();
            return Convert.ToDouble(sumDepositAmount);
        }


        internal double GetTotalDeposit(DateTime Fdate ,DateTime Tdate)
        {
            ISession session = SessionFactory.OpenSession();
            int monthNameIntValue = Fdate.Month;
            string monthName = GetMonth(monthNameIntValue);
            var query = session.CreateSQLQuery("SELECT SUM(D.DepositAmmount) " +
                                                        "AS AMAOUNT FROM Deposit AS D " +
                                                        "INNER JOIN Members AS M " +
                                                        "ON D.Members_id = M.Id " +
                                                        "WHERE D.MonthName = '" + monthName + "'");
            var totalDepositAmount = query.UniqueResult();
            return Convert.ToDouble(totalDepositAmount);
        }

        internal IList<Deposit> ViewAllBazarList()
        {
            IList<Deposit> returnDepositList;
            ISession session = SessionFactory.OpenSession();
            returnDepositList = session.QueryOver<Deposit>().List<Deposit>();
            return returnDepositList;
        }

        internal Deposit GetByDepositId(long Id)
        {
            Deposit deposit = new Deposit();
            ISession session = SessionFactory.OpenSession();

            deposit = session.Get<Deposit>(Id);

            return deposit;
        }

        internal void Update(Deposit deposit)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(deposit);
                    transaction.Commit();
                }
            }
        }

        internal void DeleteDeposit(Deposit deposit)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(deposit);
                    transaction.Commit();
                    session.Close();
                }
            }
        }

        internal IList<Deposit> GetSearchResult(string sFname, DateTime Fdate, DateTime Tdate)
        {
            IList<Deposit> returnDepositList;
            ISession session = SessionFactory.OpenSession();
            returnDepositList =
                session.CreateCriteria<Deposit>().Add(Restrictions.Between("DepositDate", Fdate, Tdate)).
                CreateCriteria("Members").Add(Restrictions.Like("FName", sFname, MatchMode.Anywhere)).List<Deposit>();
            return returnDepositList;
        }
    }

}