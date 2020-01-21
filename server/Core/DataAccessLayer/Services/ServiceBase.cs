using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Services.Validators;
using Infrastructure;
using Infrastructure.Commands.AppRoles;
using Microsoft.Extensions.Configuration;
using Dapper;
using ServiceStack;
using DataAccessLayer.Models;
using ServiceStack.OrmLite;
using ServiceStack.Data;
using System.Data;
namespace DataAccessLayer.Services.Validators
{
    public abstract class ServiceBase
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public ServiceBase(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        protected void ExecInTraction(Action<IDbConnection> action)
        {
            using (var db = dbConnectionFactory.Open())
            {
                using (IDbTransaction trans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        action.Invoke(db);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }

                }
            }
        }
    }
}
