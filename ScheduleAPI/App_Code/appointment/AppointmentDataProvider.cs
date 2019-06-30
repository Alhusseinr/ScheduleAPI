using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using AppointmentDataAccess;

namespace NitrosoftAPI.App_Code.appointment
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

        public AppointmentDataAccess.appointment SetUpAppointment(AppointmentDataAccess.appointment appointment) {
            AppointmentDataAccess.appointment result = null;
            using (var sqlConnection = new SqlConnection(ConnectionString)) {
                DynamicParameters parameters = new DynamicParameters();
                sqlConnection.Open();
                parameters.Add("@appointmentId",appointment.appointmentId,DbType.String,ParameterDirection.Input);
                parameters.Add("@appointmentStartDate",appointment.appointmentStartDate,DbType.String,ParameterDirection.Input);
                parameters.Add("@appointmentEndDate",appointment.appointmentEndDate,DbType.String,ParameterDirection.Input);
                parameters.Add("@memo",appointment.memo,DbType.String,ParameterDirection.Input);
                parameters.Add("@cancelled",appointment.cancelled ? 1 : 0,DbType.String,ParameterDirection.Input);
                parameters.Add("@confirmed",appointment.confirmed ? 1 : 0,DbType.String,ParameterDirection.Input);
                parameters.Add("@completed",appointment.completed ? 1 : 0,DbType.String,ParameterDirection.Input);
                result = sqlConnection.QueryFirstOrDefault<AppointmentDataAccess.appointment>("Appointment_add",parameters,commandType: CommandType.StoredProcedure);
                sqlConnection.Close();
            }
            return result;
        }

        public List<AppointmentDataAccess.appointment> GetAllAppointments() {
            List<AppointmentDataAccess.appointment> result = null;
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["testDb"].ToString())) {
                DynamicParameters parameters = new DynamicParameters();
                sqlConnection.Open();
                string sql = "select * from appointment";
                result = sqlConnection.Query<AppointmentDataAccess.appointment>(sql,parameters,commandType: CommandType.Text).ToList();
                sqlConnection.Close();
            }
            return result;
        }

    }
}