using ScheduleAPI.App_Code.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ScheduleAPI.Models;

namespace ScheduleAPI.Controllers {
    public class AuthenticationController: ApiController {

        private const string JSON_MEDIA_TYPE = "application/json";
        private Security_Controller objectSecurity;

        //[HttpPost]
        //public ActionResult AdminSignIn([FromBody]User user) {
        //    objectSecurity = new Security_Controller();

        //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden) {
        //        Content = new StringContent(JsonConvert.SerializeObject("An error has occured"), Encoding.UTF8,JSON_MEDIA_TYPE)
        //    };

        //    try {
        //        var authorizedUser = objectSecurity.AdminSignIn(user);
        //    }

        //}

        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateNewUser([FromBody]User user) {
            objectSecurity = new Security_Controller();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden) {
                Content = new StringContent(JsonConvert.SerializeObject("An error has occured"),Encoding.UTF8,JSON_MEDIA_TYPE)
            };

            try {
                var newUser = objectSecurity.CreateNewUser(user);

                response = new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = new StringContent(JsonConvert.SerializeObject(newUser),Encoding.UTF8,JSON_MEDIA_TYPE)
                };

            } catch (Exception e) {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(e),Encoding.UTF8,JSON_MEDIA_TYPE);
            }
            return response;
        }

    }
}