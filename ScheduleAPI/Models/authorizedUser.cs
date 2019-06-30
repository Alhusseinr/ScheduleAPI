using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScheduleAPI.Models {
    public class authorizedUser {
        public string emial { get; set; }
        public Guid authorized_user { get; set; }
    }
}