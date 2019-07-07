using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using AppointmentDataAccess;
using Newtonsoft.Json;
using AppointmentInterface.App_Code.Services;
using ScheduleAPI.Models;

namespace AppointmentInterface.Controllers
{
    public class Home2Controller: Controller
    {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult appointmentsPage(appointment appointment) {
            return View();
        }

        public ActionResult CreateNewUser(appointment appointment) {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewUser(User user)
        {
            user.userId = Guid.NewGuid().GetHashCode();
            user.token = Guid.NewGuid().ToString();
            user.token_expire = DateTime.Now;

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            StringContent content = new StringContent(JsonConvert.SerializeObject(user));

            var apiCallService = new WebApiCallService<appointment>();
            var NewUser = apiCallService.ApiPostRequest("Authentication/CreateNewUser",content,responseMessage).Result;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult insertIntoDb(appointment appointment) {

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            StringContent content = new StringContent(JsonConvert.SerializeObject(appointment));

            var apiCallService = new WebApiCallService<appointment>();
            var inserted = apiCallService.ApiPostRequest("Appointments/SetUpAppointment",content,responseMessage).Result;

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult GetAllAppointment() {

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            StringContent content = new StringContent(JsonConvert.SerializeObject(""));

            var apiCallService = new WebApiCallService<List<appointment>>();
            var appointments = apiCallService.ApiGetRequest("Appointments/GetAllAppointments",responseMessage).Result; 

            return View(appointments);
        }
    }
}