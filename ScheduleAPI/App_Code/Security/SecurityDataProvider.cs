using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using AppointmentDataAccess;
using ScheduleAPI.Models;

namespace NitrosoftAPI.App_Code.Security
{
    /// <summary>
    /// https://dapper-tutorial.net/queryfirstordefault
    /// </summary>
    public sealed class DataProvider
    {
        private static volatile DataProvider instance;
        private static object syncRoot = new Object();
        private static string _connectionString;

        private DataProvider(string ConnStr)
        {
            _connectionString = ConnStr;
        }

        public static DataProvider Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(syncRoot)
                    {
                        if(instance == null)
                            instance = new DataProvider(ConfigurationManager.ConnectionStrings["testDb"].ToString());
                    }
                }
                return instance;
            }
        }
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public user AdminSignIn(user user) {
            user result = null;
            using (var sqlConnection = new SqlConnection(ConnectionString)) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@email",user.email,DbType.String,ParameterDirection.Input);
                parameters.Add("@password",user.password,DbType.String,ParameterDirection.Input);
                sqlConnection.Open();
                result = sqlConnection.QueryFirstOrDefault<user>("admin_signIn",parameters,commandType: CommandType.StoredProcedure);
                sqlConnection.Close();
            }
            return result;
        }

        public User CreateNewUser(User user) {
            User result = null;
            using (var sqlConnection = new SqlConnection(ConnectionString)) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@userId",user.userId,DbType.String,ParameterDirection.Input);
                parameters.Add("@fname",user.fname,DbType.String,ParameterDirection.Input);
                parameters.Add("@lname",user.lname,DbType.String,ParameterDirection.Input);
                parameters.Add("@username",user.username,DbType.String,ParameterDirection.Input);
                parameters.Add("@email",user.email,DbType.String,ParameterDirection.Input);
                parameters.Add("@password",user.password,DbType.String,ParameterDirection.Input);
                parameters.Add("@token",user.token,DbType.String,ParameterDirection.Input);
                parameters.Add("@token_expire",user.token_expire,DbType.DateTime,ParameterDirection.Input);
                sqlConnection.Open();
                result = sqlConnection.QueryFirstOrDefault<User>("Create_new_user",parameters,commandType: CommandType.StoredProcedure);
                sqlConnection.Close();
            }
            return result;
        }
    }
}