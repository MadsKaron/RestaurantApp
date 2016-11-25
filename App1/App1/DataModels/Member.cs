using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class Members
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email_address { get; set; }
        public double user_point { get; set; }
        public DateTime latest_visit { get; set; }
        public string version { get; set; }
        public bool deleted { get; set; }

    }
}
