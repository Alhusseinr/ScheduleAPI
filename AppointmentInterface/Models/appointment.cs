using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentInterface.Models
{
    public class appointment
    {
        public int appointmentId { get; set; }
        public DateTime appointmentStartDate { get; set; }
        public DateTime appointmentEndDate { get; set; }
        public string memo { get; set; }
        public bool cancelled { get; set; }
        public bool confirmed { get; set; }
        public bool completed { get; set; }
    }
}