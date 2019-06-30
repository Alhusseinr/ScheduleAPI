using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppointmentDataAccess;
using NitrosoftAPI.App_Code.appointment;

namespace ScheduleAPI.App_Code.appointments {
    public class Appointment_Controller
    {
        public List<AppointmentDataAccess.appointment> GetAppointments() {
            return DataProvider.Instance.GetAllAppointments();
        }

        public AppointmentDataAccess.appointment SetUpAppointment(AppointmentDataAccess.appointment appointment) {
            return DataProvider.Instance.SetUpAppointment(appointment);
        }
    }
}