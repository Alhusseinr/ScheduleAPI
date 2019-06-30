using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NitrosoftAPI.App_Code.Security;
using AppointmentDataAccess;
using ScheduleAPI.Models;

namespace ScheduleAPI.App_Code.Security
{
    public class Security_Controller
    {
        public Security_Controller() {
        }

        public user AdminSignIn(user user) {
            return DataProvider.Instance.AdminSignIn(user);
        }

        public User CreateNewUser(User user) {
            return DataProvider.Instance.CreateNewUser(user);
        }
    }
}