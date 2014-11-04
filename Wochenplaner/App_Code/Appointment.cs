using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wochenplaner {
    public class Appointment {
        private string user { get { return this.user; } set { this.user = value; } }
        private string title { get { return this.title; } set { this.title = value; } }
        private string description { get { return this.description; } set { this.description = value; } }
        private DateTime startDate { get { return this.startDate; } set { this.startDate = value; } }
        private DateTime endDate { get { return this.endDate; } set { this.endDate = value; } }
        private byte repeat { get { return this.repeat; } set { this.repeat = value; } }

        Appointment(string _user, string _title, DateTime _startDate) {
            this.user = _user;
            this.title = _title;
            this.startDate = _startDate;
        }

        Appointment(string _user, string _title, string _desc, DateTime _startDate) {
            this.user = _user;
            this.title = _title;
            this.description = _desc;
            this.startDate = _startDate;
        }

        Appointment(string _user, string _title, string _desc, DateTime _startDate, DateTime _endDate) {
            this.user = _user;
            this.title = _title;
            this.description = _desc;
            this.startDate = _startDate;
            this.endDate = _endDate;
        }

        Appointment(string _user, string _title, string _desc, DateTime _startDate, DateTime _endDate, byte _repeat) {
            this.user = _user;
            this.title = _title;
            this.description = _desc;
            this.startDate = _startDate;
            this.endDate = _endDate;
            this.repeat = _repeat;
        }
    }
}