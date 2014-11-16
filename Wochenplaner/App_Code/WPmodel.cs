using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

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
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Benutzer\Tobias\Studium\2 Semester\Softwaregrundprojekt\Wochenplaner\Wochenplaner\App_Data\WP_DataBase.mdf;Integrated Security=True");
        public SqlConnection SqlConnection { get { return this.sqlConnection; } set { this.sqlConnection = value; } }
        /// <summary>
        ///   list of appointment objects to hold these
        /// </summary>
        private List<Appointment> appointmentList;
        public List<Appointment> AppointmentList { get { return this.appointmentList; } set { this.appointmentList = value; } }
        /// <summary>
        /// user object
        /// </summary>
        private UserData user;
        public UserData User { get { return this.user; } set { this.user = value; } }
        #endregion

        /// <summary>
        /// Constructor for the WP_Model class
        /// </summary>
        internal WPModel() {
            appointmentList = new List<Appointment>();
            this.year = DateTime.Now.Year;
            this.month = DateTime.Now.Month;
            this.week = calculateWeeknumber();
            dates = new DateTime[7];
            updateDates();

            this.user = new UserData();
        }

        /// <summary>
        /// Adds the appointment to the  List
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void addAppointment(Appointment _appo) {
            if (!this.appointmentList.Contains(_appo)) {
                this.appointmentList.Add(_appo);
            } else {
                //throw new ArgumentException("Dieser Termin existiert bereits");
            }
        }

        /// <summary>
        /// Removes the appointment from the  List
        /// </summary>
        /// <param name="_appo">An appointment</param>
        /// <returns>bool if successfull</returns>
        internal void removeAppointment(DateTime _dt) {
            if (this.appointmentList.Contains(getAppointment(_dt))) {
                this.appointmentList.Remove(getAppointment(_dt));
            }
        }

        /// <summary>
        /// graps the Appointment from the given DateTime
        /// </summary>
        /// <param name="_dt">the dateTime for the appointment search</param>
        /// <returns>the appointment</returns>
        internal Appointment getAppointment(DateTime _dt) {
            return appointmentList.Find(x => x.StartDate == _dt);
        }

        /// <summary>
        /// Changes the title of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterTitle(string _title, DateTime _dt) {
            Appointment _appo = this.appointmentList.Find(x => x.StartDate == _dt);
            if (_appo != null) {
                _appo.Title = _title;
            }
        }

        /// <summary>
        /// Changes the description of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterDescription(string _desc, DateTime _dt) {
            Appointment _appo = this.appointmentList.Find(x => x.StartDate == _dt);
            if (_appo != null) {
                _appo.Description = _desc;
            }
        }

        /// <summary>
        /// Changes the starttime of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterStartTime(Appointment _appo, DateTime _startDate) {
            if (!this.appointmentList.Contains(_appo)) {
                //TODO
                //this.appointmentList.Find(_appo).Value.StartDate = _startDate;
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
                //TODO
                //this.appointmentList.Find(_appo).Value.EndDate = _endDate;
            } else {
                //throw new ArgumentException("Dieser Termin existiert nicht");
            }
        }

        /// <summary>
        /// Changes the repeat-rate of the appointment
        /// </summary>
        /// <param name="_appo">An appointment</param>
        internal void alterRepeat(byte _repeat, DateTime _dt) {
            Appointment _appo = this.appointmentList.Find(x => x.StartDate == _dt);
            if (_appo != null) {
                _appo.Repeat = _repeat;
            }
        }

        /// <summary>
        /// Converts the dateTime of an appointment to a short datetime-String
        /// </summary>
        /// <returns>A short dateTime-String</returns>
        internal string getShortWeekday(int i) {
            return this.dates[i].ToString("ddd");
        }

        /// <summary>
        /// Converts the dateTime of an appointment to a full datetime-String
        /// </summary>
        /// <returns>A full dateTime-String</returns>
        internal string getLongWeekday(int i) {
            return this.dates[i].ToString("dddd");
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
        private int calculateWeeknumber() {
            DateTime time = DateTime.Now;
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        #region SQL

        /// <summary>
        /// Writes an Appointment into an sql table
        /// </summary>
        public void sqlWriteAppointments(Appointment _appo) {
            try {
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Closed) {
                    sqlConnection.Open();
                }
                SqlCommand command = new SqlCommand("INSERT INTO appointments VALUES (@USER, @TITLE, @DESC, @STARTDATE, @ENDDATE, @REPEAT)", sqlConnection);
                command.Parameters.AddWithValue("@USER", _appo.User);
                command.Parameters.AddWithValue("@TITLE", _appo.Title);
                command.Parameters.AddWithValue("@DESC", _appo.Description);
                command.Parameters.AddWithValue("@STARTDATE", _appo.StartDate);
                command.Parameters.AddWithValue("@REPEAT", _appo.Repeat);
                command.Parameters.AddWithValue("@ENDDATE", _appo.EndDate);

                command.ExecuteNonQuery();

            } catch (Exception ex) {
                AppointmentDelegate ad = new AppointmentDelegate();
                ad.TriggerError += new AppointmentCreateEventHandler(ad.playErrorSound);
                ad._triggerError();
            }
        }

        /// <summary>
        /// reads an appointment entry from an sql table
        /// </summary>
        public void sqlReadAppointments() {
            try {
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Closed) {
                    sqlConnection.Open();
                }
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Appointments", sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                Appointment appo = null;
                List<Appointment> appoList = new List<Appointment>();

                while (reader.Read()) {
                    if (reader.GetString(1) == user.Id) {
                        string _user = reader.GetString(1);
                        string title = reader.GetString(2);
                        string desc = reader.GetString(3);
                        DateTime startDate = reader.GetDateTime(4);
                        DateTime endDate = reader.GetDateTime(5);
                        byte repeat = reader.GetByte(6);

                        if (repeat != 0) {
                            appo = new Appointment(_user, title, desc, startDate, repeat, endDate);
                        } else if (endDate != null) {
                            appo = new Appointment(_user, title, desc, startDate, repeat);
                        } else if (desc != null) {
                            appo = new Appointment(_user, title, desc, startDate);
                        } else {
                            appo = new Appointment(_user, title, startDate);
                        }

                        appoList.Add(appo);
                    }
                }

                reader.Close();
                this.appointmentList = appoList;

            } catch (Exception ex) {
                AppointmentDelegate ad = new AppointmentDelegate();
                ad.TriggerError += new AppointmentCreateEventHandler(ad.playErrorSound);
                ad._triggerError();
            }
        }

        /// <summary>
        /// Deletes an Appointment from an sql table
        /// </summary>
        public void sqlDeleteAppointment(DateTime _dt) {
            try {
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Closed) {
                    sqlConnection.Open();
                }

                SqlCommand command = new SqlCommand("DELETE FROM Appointments WHERE startDate = @STARTDATE", sqlConnection);
                command.Parameters.AddWithValue("@STARTDATE", _dt);

                command.ExecuteNonQuery();

            } catch (Exception ex) {
                AppointmentDelegate ad = new AppointmentDelegate();
                ad.TriggerError += new AppointmentCreateEventHandler(ad.playErrorSound);
                ad._triggerError();
            }
        }
        
        /// <summary>
        /// Writes an user into an sql table
        /// </summary>
        public void sqlWriteUser(UserData _user) {
            try {
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Closed) {
                    sqlConnection.Open();
                }
                SqlCommand command = new SqlCommand("INSERT INTO users VALUES (@USER, @NAME, @PASSWORD)", sqlConnection);
                command.Parameters.AddWithValue("@USER", _user.Id);
                command.Parameters.AddWithValue("@NAME", _user.Name);
                command.Parameters.AddWithValue("@PASSWORD", DBNull.Value);

                command.ExecuteNonQuery();

            } catch (Exception ex) {
                AppointmentDelegate ad = new AppointmentDelegate();
                ad.TriggerError += new AppointmentCreateEventHandler(ad.playErrorSound);
                ad._triggerError();
            }
        }

        /// <summary>
        /// reads an user entry from an sql table
        /// </summary>
        public void sqlReadUser(string _name) {
            try {
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Closed) {
                    sqlConnection.Open();
                }

                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Users", sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                string _id = null;
                string name = null;

                while (reader.Read()) {
                    if (reader.GetString(2) == _name) {
                        _id = reader.GetString(1);
                        name = reader.GetString(2);
                        //string pass = reader.GetString(3);

                        user = new UserData(name);
                        user.Id = _id;
                        break;
                    }
                }

                reader.Close();
                if (_id != null) {
                    sqlReadAppointments();
                }

            } catch (Exception ex) {
                AppointmentDelegate ad = new AppointmentDelegate();
                ad.TriggerError += new AppointmentCreateEventHandler(ad.playErrorSound);
                ad._triggerError();
            }
        }
        #endregion

    }
}