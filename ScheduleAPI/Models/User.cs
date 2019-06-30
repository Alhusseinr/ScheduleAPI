using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScheduleAPI.Models {
    public class User {
        public int userId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public DateTime token_expire { get; set; }
    }
}