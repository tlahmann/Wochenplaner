using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Wochenplaner.App_Code {
    public class WPModel {
        #region Variables
        /// <summary>
        /// Declaration of variables
        /// </summary>
        private int year;
        public int Year { get { return this.year; } set { this.year = value; } }
        private int month;
        public int Month { get { return this.month; } set { this.month = value; } }
        private int week;
        public int Week { get { return this.week; } set { this.week = value; updateDates(); } }
        private DateTime[] dates;
        public DateTime[] Dates { get { return this.dates; } set { this.dates = value; } }
        #endregion

        /// <summary>
        /// Linked list of appointments to hold these
        /// </summary>
        LinkedList<Appointment> appointmentList;

        /// <summary>
        /// Constructor for the WP_Model class
        /// </summary>
        internal WPModel() {
            appointmentList = new LinkedList<Appointment>();
            this.year = DateTime.Now.Year;
            this.month = DateTime.Now.Month;
            this.week = getWeeknumber();
            dates = new DateTime[7];
            updateDates();
        }

        /// <summary>
        /// Adds the appointment to the linkedList
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void addAppointment(Appointment _appo) {
            if (!this.appointmentList.Contains(_appo)) {
                this.appointmentList.AddLast(_appo);
            } else {
                //throw new ArgumentException("Dieser Termin existiert bereits");
            }
        }

        /// <summary>
        /// Removes the appointment from the linkedList
        /// </summary>
        /// <param name="_appo">An appointment</param>
        /// <returns>bool if successfull</returns>
        internal bool removeAppointment(Appointment _appo) {
            if (!this.appointmentList.Contains(_appo)) {
                return this.appointmentList.Remove(_appo);
            } else {
                return false;
                //throw new ArgumentException("Dieser Termin existiert nicht");
            }
        }

        /// <summary>
        /// Changes the title of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterTitle(Appointment _appo, string _title) {
            if (!this.appointmentList.Contains(_appo)) {
                this.appointmentList.Find(_appo).Value.Title = _title;
            } else {
                //throw new ArgumentException("Dieser Termin existiert nicht");
            }
        }

        /// <summary>
        /// Changes the description of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterDescription(Appointment _appo, string _desc) {
            if (!this.appointmentList.Contains(_appo)) {
                this.appointmentList.Find(_appo).Value.Description = _desc;
            } else {
                //throw new ArgumentException("Dieser Termin existiert nicht");
            }
        }

        /// <summary>
        /// Changes the starttime of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterStartTime(Appointment _appo, DateTime _startDate) {
            if (!this.appointmentList.Contains(_appo)) {
                this.appointmentList.Find(_appo).Value.StartDate = _startDate;
            } else {
                //throw new ArgumentException("Dieser Termin existiert nicht");
            }
        }

        /// <summary>
        /// Changes the endtimeof the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterEndTime(Appointment _appo, DateTime _endDate) {
            if (!this.appointmentList.Contains(_appo)) {
                this.appointmentList.Find(_appo).Value.EndDate = _endDate;
            } else {
                //throw new ArgumentException("Dieser Termin existiert nicht");
            }
        }

        /// <summary>
        /// Changes the repeat-rate of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterRepeat(Appointment _appo, byte _repeat) {
            if (!this.appointmentList.Contains(_appo)) {
                this.appointmentList.Find(_appo).Value.Repeat = _repeat;
            } else {
                //throw new ArgumentException("Dieser Termin existiert nicht");
            }
        }

        /// <summary>
        /// Method to update the Dates stored in the dates array.
        /// Gets called when the week is changed.
        /// </summary>
        private void updateDates() {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = week;
            if (firstWeek <= 1) {
                weekNum -= 1;
            }

            var result = firstThursday.AddDays(( weekNum * 7 ) - 4);
            for (int i = 0; i < 7; i++) {
                dates[i] = result.AddDays(1);
                result = result.AddDays(1);
            }
        }

        /// <summary>
        /// <c>getWeeknumber</c> is a method in the <c>Wochenplaner</c> class. It is used to 
        /// calculate a weeknumber from a given DateTime</summary>
        /// <returns>weeknumber of the given time</returns>
        private int getWeeknumber() {
            DateTime time = DateTime.Now;
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}