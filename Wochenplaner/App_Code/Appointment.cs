using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Wochenplaner.App_Code {
    public class Appointment: IEquatable<Appointment> {
        #region Variables
        /// <summary>
        /// Declaration of variables
        /// </summary>
        private int id;
        public int Id { get; set; }
        private string user;
        public string User { get { return this.user; } set { this.user = value; } }
        private string title;
        public string Title { get { return this.title; } set { this.title = value; } }
        private string description;
        public string Description { get { return this.description; } set { this.description = value; } }
        private DateTime startDate;
        public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
        // TODO enddate
        //private DateTime endDate;
        //public DateTime EndDate { get { return this.endDate; } set { this.endDate = value; } }
        private byte repeat;
        public byte Repeat { get { return this.repeat; } set { this.repeat = value; } }
        #endregion

        /// <summary>
        /// Constructor for 0 variables
        /// </summary>
        internal Appointment() {
        }

        /// <summary>
        /// Constructor for 3 variables
        /// </summary>
        /// <param name="_user">The currently logged in username</param>
        /// <param name="_title">The title of the Appointment</param>
        /// <param name="_startDate">The start dateTime String of the Appointment</param>
        internal Appointment(string _user, string _title, DateTime _startDate) {
            this.user = _user;
            this.title = _title;
            this.startDate = _startDate;
        }

        /// <summary>
        /// Constructor for 4 variables
        /// </summary>
        /// <param name="_user">The currently logged in username</param>
        /// <param name="_title">The title of the appointment</param>
        /// <param name="_desc">The description of the appointment</param>
        /// <param name="_startDate">The start dateTime-String of the appointment</param>
        internal Appointment(string _user, string _title, string _desc, DateTime _startDate) {
            this.user = _user;
            this.title = _title;
            this.description = _desc;
            this.startDate = _startDate;
        }

        /// <summary>
        /// Constructor for 5 variables
        /// </summary>
        /// <param name="_user">The currently logged in username</param>
        /// <param name="_title">The title of the appointment</param>
        /// <param name="_desc">The description of the appointment</param>
        /// <param name="_startDate">The start dateTime-String of the appointment</param>
        /// <param name="_repeat">The byte to demetmine the repeat-ratio of the appointment</param>
        internal Appointment(string _user, string _title, string _desc, DateTime _startDate, byte _repeat) {
            this.user = _user;
            this.title = _title;
            this.description = _desc;
            this.startDate = _startDate;
            this.repeat = _repeat;
        }

        /// <summary>
        /// Constructor for 6 variables
        /// </summary>
        /// <param name="_user">The currently logged in username</param>
        /// <param name="_title">The title of the appointment</param>
        /// <param name="_desc">The description of the appointment</param>
        /// <param name="_startDate">The start dateTime-String of the appointment</param>
        /// <param name="_repeat">The byte to demetmine the repeat-ratio of the appointment</param>
        /// <param name="_endDate">The end dateTime-String of the appointment</param>
        [Obsolete("EndDate not intended to be used anymore")]
        internal Appointment(string _user, string _title, string _desc, DateTime _startDate, byte _repeat, DateTime _endDate) {
            this.user = _user;
            this.title = _title;
            this.description = _desc;
            this.startDate = _startDate;
            this.repeat = _repeat;
            //this.endDate = _endDate;
        }

        /// <summary>
        /// Converts the dateTime of an appointment to a short datetime-String
        /// </summary>
        /// <returns>A short dateTime-String</returns>
        internal string getShortWeekday() {
            return this.startDate.ToString("ddd");
        }

        /// <summary>
        /// Equals-Method "IEquatable"
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj == null) return false;
            Appointment objAsPart = obj as Appointment;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        /// <summary>
        /// GetHashCode-Method "IEquatable"
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return id;
        }

        /// <summary>
        /// Equals-Method "IEquatable"
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Appointment other) {
            if (other == null) return false;
            return ( this.id.Equals(other.id) );
        }
    
    }
}