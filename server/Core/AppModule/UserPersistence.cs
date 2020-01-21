using Dapper;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AppModule
{
    public class UserPersistence
    {
        private string connectionString;
        private readonly DatabaseContext databaseContext;

        public UserPersistence(IConfiguration configuration, DatabaseContext databaseContext)
        {
            connectionString = configuration["connectionString"];
            this.databaseContext = databaseContext;
        }

        public (string Username, string FirstName, Guid Id) Login(string username, string password)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                return conn.QueryFirst<(string FirstName, string Username, Guid Id)>("select FirstName, Username, Id from AppUsers where username=@username and password=@password", new {
                    username,
                    password
                });
            }
        }

        public IEnumerable<string> GetPermissions(string username)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<string>("select Permission from [UserPermission] where username=@username", new
                {
                    username
                });
            }
        }

        public IEnumerable<object> GetUsers()
        {
            return databaseContext.AppUsers.Select(x => x.Username).ToList();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query("select * from AppUsers");
            }
        }
    }
}
