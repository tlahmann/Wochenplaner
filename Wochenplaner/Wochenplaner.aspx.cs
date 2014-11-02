﻿using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlClient;
using Wochenplaner.App_Code;

namespace Wochenplaner {
    public partial class Wochenplaner : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (subtitle.Text == "Kalenderwoche") {
                paintWeekNumber(getWeeknumber(DateTime.Now), DateTime.Now.Year);
            }
            if (bt1.Text == "Montag") {
                createRandomUser();
            }
            paintDate(getWeeknumber(DateTime.Now), DateTime.Now.Year);
            disableButtonsOnPageLoad();
            sqlRead();
        }

        #region Design

        #region DateManagement

        /// <summary>
        /// Creates dates array to display the dates from the given weeknumber in the
        /// webform</summary>
        /// <param name="weekOfYear">weeknumber</param>
        /// <param name="year">year</param>
        /// <returns>datearray</returns>
        /// <seealso cref="paintDate(int, int)"> Is used in method paintDate</seealso>
        public static DateTime[] getDatesFromWeekNumber(int weekOfYear, int year) {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1) {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays((weekNum * 7) - 4);
            DateTime[] dt = new DateTime[7];
            for (int i = 0; i < 7; i++) {
                dt[i] = result.AddDays(1);
                result = result.AddDays(1);
            }
            return dt;
        }

        /// <summary>
        /// Creates dates array to display the dates from the given weeknumber in the
        /// webform</summary>
        /// <param name="weekOfYear">weeknumber</param>
        /// <param name="year">year</param>
        /// <returns>datearray</returns>
        /// <seealso cref="paintDate(int, int)"> Is used in method paintDate</seealso>
        public static DateTime getDateFromWeekday(int weekOfYear, int year, string day) {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1) {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);

            int i = 0;
            switch (day) {
                case "MO":
                    i = -3;
                    break;
                case "DI":
                    i = -2;
                    break;
                case "MI":
                    i = -1;
                    break;
                case "DO":
                    i = 0;
                    break;
                case "FR":
                    i = 1;
                    break;
                case "SA":
                    i = 2;
                    break;
                case "SO":
                    i = 3;
                    break;
                default:
                    break;
            }

            return result.AddDays(i);
        }

        /// <summary>
        /// <c>getWeeknumber</c> is a method in the <c>Wochenplaner</c> class. It is used to 
        /// calculate a weeknumber from a given DateTime</summary>
        /// <param name="time">time</param>
        /// <returns>weeknumber of the given time</returns>
        private int getWeeknumber(DateTime time) {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// getWeeknumber gets the weeknumber from the displayed
        /// week-string (asp - header)
        /// <returns>weeknumber of the shown time</returns>
        private int getWeeknumber() {
            string[] words = subtitle.Text.Split(' ');

            return Convert.ToInt32(words[1]);
        }

        /// <summary>
        /// <c>getWeeknumber</c> gets the yearnumber from the displayed
        /// week-string (asp - header)
        /// <returns>yearnumber of the shown time</returns>
        private int getYearnumber() {
            string[] words = subtitle.Text.Split(' ');

            return Convert.ToInt32(words[3]);
        }

        /// <summary>
        /// Create a date string from a day and time string</summary>
        /// <param name="day">day string</param>
        /// <returns>datestring</returns>
        private string buildDateString(string day) {
            string _day = null;
            switch (day) {
                case "MO":
                    _day = "Montag";
                    break;
                case "DI":
                    _day = "Dienstag";
                    break;
                case "MI":
                    _day = "Mittwoch";
                    break;
                case "DO":
                    _day = "Donnerstag";
                    break;
                case "FR":
                    _day = "Freitag";
                    break;
                case "SA":
                    _day = "Samstag";
                    break;
                case "SO":
                    _day = "Sonntag";
                    break;
                default:
                    break;
            }

            return _day;
        }

        #endregion

        #region Display

        /// <summary>
        /// shows the appointment on the website
        /// </summary>
        private void paintAppointment(string _datetime, string _title, string _desc) {
            if (_title.Length > 10) {
                _title = _title.Substring(0, 10);
            }
            if (_desc.Length > 10) {
                _desc = _desc.Substring(0, 10);
            }

            // Find control on page.
            Button chosenButton = (Button)FindControl(_datetime);

            if (chosenButton != null) {
                chosenButton.Text = _title + Environment.NewLine + _desc;
                chosenButton.BackColor = Color.FromArgb(255, 236, 220);
            } else {
            }
        }

        /// <summary>
        /// Fades in the overlay for the appointment creation.</summary>
        /// <param name="dateTime">datetime takes a string containing the date and time where to create the
        /// appointment</param>
        public void fadeInOverlay(string weekday, DateTime date, string time, string buttonCode) {
            string scriptTxt = "openInputOverlay();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OverlayScript", scriptTxt, true);
            overlayChoosenDate.Text = "Zeit: " + weekday + ", " + date.ToShortDateString() + " um " + time + ":00 Uhr (" + buttonCode + ")";
        }

        /// <summary>
        /// Event reaction to handle the button klick on the webform.</summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event Argument</param>
        protected void openOverlay(object sender, EventArgs e) {
            Button bt = (Button)sender;
            int mid = bt.ID.Length / 2;
            string day = bt.ID.Substring(0, mid);
            string time = bt.ID.Substring(mid, mid);
            fadeInOverlay(buildDateString(day), getDateFromWeekday(getWeeknumber(), getYearnumber(), day), time, bt.ID);
        }

        /// <summary>
        /// Event reaction to handle the button klick on the webform.</summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event Argument</param>
        protected void closeOverlay(object sender, EventArgs e) {

        }

        /// <summary>
        /// Paints the given weeknumber and year to the subtitle label</summary>
        /// <param name="weekNr">weekNr takes the week number to display</param>
        /// <param name="year">year takes the year to display</param>
        private void paintWeekNumber(int weekNr, int year) {
            subtitle.Text = "Kalenderwoche " + weekNr.ToString() + " - " + year.ToString();
        }

        /// <summary>
        /// Displays the dates of the chosen week number.</summary>
        /// <param name="weekNr">weekNr takes the week number to display</param>
        /// <param name="year">year takes the year to display</param>
        private void paintDate(int weekNr, int year) {
            DateTime[] dt = getDatesFromWeekNumber(weekNr, year);
            bt1.Text = "Montag" + Environment.NewLine + dt[0].ToShortDateString().ToString();
            bt2.Text = "Dienstag" + Environment.NewLine + dt[1].ToShortDateString().ToString();
            bt3.Text = "Mittwoch" + Environment.NewLine + dt[2].ToShortDateString().ToString();
            bt4.Text = "Donnerstag" + Environment.NewLine + dt[3].ToShortDateString().ToString();
            bt5.Text = "Freitag" + Environment.NewLine + dt[4].ToShortDateString().ToString();
            bt6.Text = "Samstag" + Environment.NewLine + dt[5].ToShortDateString().ToString();
            bt7.Text = "Sonntag" + Environment.NewLine + dt[6].ToShortDateString().ToString();
        }

        protected void btnBkwd_Click(object sender, EventArgs e) {
            string[] words = subtitle.Text.Split(' ');
            int week = Convert.ToInt32(words[1]);
            int year = Convert.ToInt32(words[3]);
            if (week <= 1) {
                week = 52;
                year -= 1;
            } else {
                week -= 1;
            }
            paintWeekNumber(week, year);
            paintDate(week, year);
        }

        protected void btnFwrd_Click(object sender, EventArgs e) {
            string[] words = subtitle.Text.Split(' ');
            int week = Convert.ToInt32(words[1]);
            int year = Convert.ToInt32(words[3]);
            if (week >= 52) {
                week = 1;
                year += 1;
            } else {
                week += 1;
            }
            paintWeekNumber(week, year);
            paintDate(week, year);
        }

        #endregion

        /// <summary>
        /// Creates a random String to be used as user login to (teporarily) save
        /// the entered appointments
        /// </summary>
        private void createRandomUser() {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            userData.Text = result;
        }

        /// <summary>
        /// Disables the header buttons</summary>
        private void disableButtonsOnPageLoad() {
            this.bt1.Enabled = false;
            this.bt2.Enabled = false;
            this.bt3.Enabled = false;
            this.bt4.Enabled = false;
            this.bt5.Enabled = false;
            this.bt6.Enabled = false;
            this.bt7.Enabled = false;
        }

        #endregion

        #region Login User

        /// <summary>
        /// Reacts to the Button Click "Login"
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event e</param>
        protected void loginBtnClick(object sender, EventArgs e) {
            string scriptTxt = "openLoginOverlay();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OverlayScript", scriptTxt, true);
        }

        protected void loginUser(object sender, EventArgs e) {
            userData.Text = overlayTextBoxLogin.Text;
            sqlRead();
        }

        #endregion

        #region Appointment Handling

        /// <summary>
        /// Reacts to the Button Click from the "create" Button on the overlay
        /// Opens an SQL connection and passes the data from 
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event e</param>
        protected void createAppointment(object sender, EventArgs e) {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Benutzer\Tobias\Studium\2 Semester\Softwaregrundprojekt\Wochenplaner\Wochenplaner\App_Data\WP_DataBase.mdf;Integrated Security=True");
            try {
                sqlConnection.Open();
                sqlWrite(sqlConnection);
            } catch (Exception ex) {
            }
        }

        /// <summary>
        /// Writes an Appointment into an sql table
        /// </summary>
        private void sqlWrite(SqlConnection sqlCon) {
            string cd = Regex.Match(overlayChoosenDate.Text, @"\(([^)]*)\)").Groups[1].Value;
            int mid = cd.Length / 2;
            string day = cd.Substring(0, mid);
            string time = cd.Substring(mid, mid);

            try {
                // Grap user data and content
                string user = userData.Text;
                string title = overlayTextBoxSmall.Text;
                string desc = overlayTextBoxLarge.Text;

                DateTime startDate = getDateFromWeekday(getWeeknumber(), getYearnumber(), day);

                SqlCommand command = new SqlCommand("INSERT INTO appointments VALUES (@USER, @TITLE, @DESC, @STARTDATE, @TIME, @ENDDATE, @REPEAT)", sqlCon);
                command.Parameters.AddWithValue("@USER", user);
                command.Parameters.AddWithValue("@TITLE", title);
                command.Parameters.AddWithValue("@DESC", desc);
                command.Parameters.AddWithValue("@STARTDATE", startDate);
                command.Parameters.AddWithValue("@TIME", time);

                if (cbRepeat.Checked) {
                    command.Parameters.AddWithValue("@REPEAT", ddRepeat.SelectedIndex);
                    if (cbEnd.Checked) {
                        command.Parameters.AddWithValue("@ENDDATE", new DateTime(Convert.ToInt32(textBoxYear.Text), Convert.ToInt32(textBoxMonth.Text), Convert.ToInt32(textBoxDay.Text)));
                    } else {
                        command.Parameters.AddWithValue("@ENDDATE", new DateTime(2999, 01, 01));
                    }
                } else {
                    command.Parameters.AddWithValue("@REPEAT", -1);
                    command.Parameters.AddWithValue("@ENDDATE", new DateTime(2999, 01, 01));
                }

                command.ExecuteNonQuery();
                paintAppointment(cd, title, desc);
            } catch (Exception ex) {
                AppointmentDelegate ad = new AppointmentDelegate();
                ad.TriggerError += new AppointmentCreateEventHandler(ad.playErrorSound);
                ad._triggerError();
            }
        }

        /// <summary>
        /// reads an entry from an sql table
        /// </summary>
        private void sqlRead() {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Benutzer\Tobias\Studium\2 Semester\Softwaregrundprojekt\Wochenplaner\Wochenplaner\App_Data\WP_DataBase.mdf;Integrated Security=True");
            try {
                sqlConnection.Open();
            } catch (Exception ex) {
            }

            try {
                //SqlDataReader reader = null;
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Appointments", sqlConnection);
                //command.Parameters.AddWithValue("@USER", userData.Text);
                //command.Parameters.Add("@USER", SqlDbType.NVarChar);
                //command.Parameters["@USER"].Value = userData.Text;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    if (reader.GetString(1) == userData.Text) {
                        string user = reader.GetString(1);
                        string title = reader.GetString(2);
                        string desc = reader.GetString(3);
                        DateTime startDate = reader.GetDateTime(4);
                        int time = reader.GetInt32(5);
                        DateTime endDate = reader.GetDateTime(6);
                        int repeat = reader.GetInt32(7);

                        paintAppointment(startDate.ToString("ddd", new CultureInfo("de-DE")) + "0" + time, title, desc);
                    }
                }

                reader.Close();
            } catch (Exception ex) {
                AppointmentDelegate ad = new AppointmentDelegate();
                ad.TriggerError += new AppointmentCreateEventHandler(ad.playErrorSound);
                ad._triggerError();
            }
        }

        /// <summary>
        /// Reacts to the button click "move"
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event e</param>
        protected void moveAppointment(object sender, EventArgs e) {
            string scriptTxt = "openTimeOverlay();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OverlayScript", scriptTxt, true);
        }

        /// <summary>
        /// Reacts to the button click "delete"
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event e</param>
        protected void deleteAppointment(object sender, EventArgs e) {

        }

        #endregion

    }
}