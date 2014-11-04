using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wochenplaner.App_Code {
    public class WP_Model {
        #region Variables
        /// <summary>
        /// Declaration of variables
        /// </summary>
        private int year;
        public int Year { get { return this.year; } set { this.year = value; } }
        private int month;
        public int Month { get { return this.month; } set { this.month = value; } }
        private int week;
        public int Week { get { return this.week; } set { this.week = value; } }
        #endregion

        /// <summary>
        /// Linked list of appointments to hold these
        /// </summary>
        LinkedList<Appointment> appointmentList;

        /// <summary>
        /// Constructor for the WP_Model class
        /// </summary>
        internal WP_Model() {
            appointmentList = new LinkedList<Appointment>();
        }

        /// <summary>
        /// Adds the appointment to the linkedList
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void addAppointment(Appointment _appo) {
            this.appointmentList.AddLast(_appo);
        }

        /// <summary>
        /// Removes the appointment from the linkedList
        /// </summary>
        /// <param name="_appo">An appointment</param>
        /// <returns>bool if successfull</returns>
        internal bool removeAppointment(Appointment _appo) {
            return this.appointmentList.Remove(_appo);
        }

        /// <summary>
        /// Changes the title of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterTitle(Appointment _appo) {
            this.appointmentList.Find(_appo);
        }

        /// <summary>
        /// Changes the description of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterDescription(Appointment _appo) {
            this.appointmentList.Find(_appo);
        }

        /// <summary>
        /// Changes the starttime of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterStartTime(Appointment _appo) {
            this.appointmentList.Find(_appo);
        }

        /// <summary>
        /// Changes the endtimeof the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterEndTime(Appointment _appo) {
            this.appointmentList.Find(_appo);
        }

        /// <summary>
        /// Changes the repeat-rate of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterRepeat(Appointment _appo) {
            this.appointmentList.Find(_appo);
        }
    }
}