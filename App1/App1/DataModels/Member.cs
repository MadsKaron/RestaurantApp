using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class Member
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email_address { get; set; }
        public string day_of_birth { get; set; }
        public string latest_visit { get; set; }
        public string version { get; set; }
        public bool deleted { get; set; }

    }
}
