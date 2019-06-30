using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using AppointmentDataAccess;
using Dapper;
using Newtonsoft.Json;
using ScheduleAPI.App_Code.appointments;

namespace ScheduleAPI.Controllers {
    public class AppointmentsController: ApiController {

        private const string JSON_MEDIA_TYPE = "application/json";
        private Appointment_Controller objAppointmentController;


        //[System.Web.Http.HttpGet]
        //public IEnumerable<appointment> Get()
        //{
        //    using (appointmentsEntities entities = new appointmentsEntities())
        //    {
        //        return entities.appointments.ToList();
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public appointment Get(int Id)
        //{
        //    using (appointmentsEntities entities = new appointmentsEntities())
        //    {
        //        return entities.appointments.FirstOrDefault(e => e.appointmentId == Id);
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public IEnumerable<appointment> GetAllAppointments() {
        //    IEnumerable<appointment> result = null;
        //    using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["testDb"].ToString())) {
        //        DynamicParameters parameters = new DynamicParameters();
        //        sqlConnection.Open();
        //        string sql = "select * from appointment";
        //        result = sqlConnection.Query<appointment>(sql,parameters,commandType: CommandType.Text).ToList();
        //        sqlConnection.Close();
        //    }
        //    return result;
        //}

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SetUpAppointment(appointment appointment) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden) {
                Content = new StringContent(JsonConvert.SerializeObject("An error has occured"),Encoding.UTF8,JSON_MEDIA_TYPE)
            };

            try {
                objAppointmentController = new Appointment_Controller();
                var appointments = objAppointmentController.SetUpAppointment(appointment);

                response = new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = new StringContent(JsonConvert.SerializeObject(true),Encoding.UTF8,JSON_MEDIA_TYPE)
                };

            } catch (Exception e) {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(e),Encoding.UTF8,JSON_MEDIA_TYPE);
            }
            return response;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllAppointments() {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden) {
                Content = new StringContent(JsonConvert.SerializeObject("An error has occured"), Encoding.UTF8,JSON_MEDIA_TYPE)
            };

            try {
                objAppointmentController = new Appointment_Controller();
                var appointments = objAppointmentController.GetAppointments();

                response = new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = new StringContent(JsonConvert.SerializeObject(appointments),Encoding.UTF8,JSON_MEDIA_TYPE)
                };

            } catch (Exception e) {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(e),Encoding.UTF8,JSON_MEDIA_TYPE);
            }
            return response;
        }

        

    }
}
