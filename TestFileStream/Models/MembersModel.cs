using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Cfg;
using TestFileStream.Entity;
using TestFileStream.Controllers;

namespace TestFileStream.Models
{
    public class MembersModel
    {
        public void Save (Members members)
        {
            using(ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(members);
                    transaction.Commit();
                    session.Close();
                }
            } 
        }

        public IList<Members> ViewAllMembers()
        {
            IList<Members> getAllMembersList;
            using(ISession session = SessionFactory.OpenSession())
            {
                getAllMembersList = session.QueryOver<Members>().List<Members>();    
            }
            return getAllMembersList;
        }

        internal void DeleteMembers(Members member)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(member);
                    transaction.Commit();
                    session.Close();
                }
            }
        }

        public Members GetById(long Id)
        {
            Members member = new Members();
            var session = SessionFactory.OpenSession();
            member = session.QueryOver<Members>().Where(x => x.Id == Id).SingleOrDefault<Members>();
            
            return member;
        }

        internal void Update(Members member)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(member);
                    transaction.Commit();
                }
            } 
        }

        
    }
}