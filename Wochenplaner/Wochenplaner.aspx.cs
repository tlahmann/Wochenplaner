using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wochenplaner.App_Code;

namespace Wochenplaner {
    public partial class Wochenplaner: System.Web.UI.Page {
        WPModel wpm;

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["wpmodel"] == null) {
                wpm = new WPModel();
                Session["wpmodel"] = wpm;
            } else {
                wpm = (WPModel)Session["wpmodel"];
                wpm.sqlReadAppointments();
                paintAppointments();
            }


            paintWeekNumber(wpm.Week, wpm.Year);
            paintDates();

            disableButtonsOnPageLoad();
        }

        #region Design

        #region DateManagement

        internal int weekdayToInt(string _day) {
            switch (_day) {
                case "MO":
                    return 0;
                case "DI":
                    return 1;
                case "MI":
                    return 2;
                case "DO":
                    return 3;
                case "FR":
                    return 4;
                case "SA":
                    return 5;
                case "SO":
                    return 6;
                default:
                    return -1;
            }
        }

        #endregion

        #region Display

        /// <summary>
        /// shows the appointment on the website
        /// </summary>
        /// <param name="_appo">An appointment to show</param>
        private void paintAppointment(Appointment _appo) {
            string _title;
            string _desc = null;
            if (_appo.Title.Length > 10) {
                _title = _appo.Title.Substring(0, 10);
            } else {
                _title = _appo.Title;
            }
            if (_appo.Description != null) {
                if (_appo.Description.Length > 10) {
                    _desc = _appo.Description.Substring(0, 10);
                } else {
                    _desc = _appo.Description;
                }
            }
            string h = null;
            if (_appo.StartDate.Hour < 10) {
                h = "0" + _appo.StartDate.Hour;
            } else {
                h = _appo.StartDate.Hour.ToString();
            }

            //Find control on page.
            Button chosenButton = (Button)FindControl(_appo.getShortWeekday() + h);
            if (chosenButton != null) {
                if (wpm.Dates[( (int)_appo.StartDate.DayOfWeek - 1 )].AddHours(_appo.StartDate.Hour) == _appo.StartDate) {
                    if (_desc != null) {
                        chosenButton.Text = _title + Environment.NewLine + _desc;
                    } else {
                        chosenButton.Text = _title;
                    }
                    chosenButton.BackColor = Color.Beige; // Paints the button in a other color to shot that an Appointment is present
                } else {
                    chosenButton.Text = "";
                    chosenButton.BackColor = Color.Transparent;
                }
            } else {
            }
        }

        /// <summary>
        /// shows the appointment on the website
        /// </summary>
        /// <param name="_appo">An appointment to show</param>
        private void paintAppointments() {
            foreach (Appointment _appo in wpm.AppointmentList) {
                paintAppointment(_appo);
            }
        }

        private void resetAppointmentDisplay() {
            for (int i = 0; i < 7; i++) {
                for (int j = 7; j < 21; j++) {
                    string h = null;
                    if (j < 10) {
                        h = "0" + j;
                    } else {
                        h = j.ToString();
                    }
                    Button chosenButton = (Button)FindControl(wpm.getShortWeekday(i) + h);
                    if (chosenButton != null) {
                        chosenButton.Text = "";
                        chosenButton.BackColor = Color.Transparent;
                    } else {
                    }
                }
            }

        }

        /// <summary>
        /// Fades in the overlay for the appointment creation.</summary>
        /// <param name="dateTime">datetime takes a string containing the date and time where to create the
        /// appointment</param>
        public void fadeInOverlay(string _day, int _time) {
            if (Session["wpmodel"] != null && wpm != null) {
                wpm = (WPModel)Session["wpmodel"];
            }
            string _t = null;
            if (_time < 10) {
                _t = "0" + _time;
            } else {
                _t = _time.ToString();
            }
            string scriptTxt = "openInputOverlay();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OverlayScript", scriptTxt, true);
            overlayChoosenDate.Text = "Zeit: " + wpm.getLongWeekday(weekdayToInt(_day)) + ", " + wpm.Dates[weekdayToInt(_day)].ToShortDateString() + " um " + _t + ":00 Uhr (" + _day + _t + ")";

        }

        /// <summary>
        /// Event reaction to handle the button klick on the webform.</summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event Argument</param>
        protected void openOverlay(object sender, EventArgs e) {
            Button bt = (Button)sender;
            int mid = bt.ID.Length / 2;
            string day = bt.ID.Substring(0, mid);
            int time = Convert.ToInt32(bt.ID.Substring(mid, mid));
            fadeInOverlay(day, time);
            overlayAppointmentRepresentation(bt, day, time);
        }

        /// <summary>
        /// Event reaction to handle the button klick on the webform.</summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event Argument</param>
        protected void closeOverlay(object sender, EventArgs e) {
            //TODO?
        }

        protected void overlayAppointmentRepresentation(Button _bt, string _day, int _time) {
            DateTime startDate = wpm.Dates[weekdayToInt(_day)].AddHours(_time);
            overlayTextBoxSmall.Text = "";
            overlayTextBoxLarge.Text = "";
            cbRepeat.Checked = false;
            ddRepeat.SelectedIndex = 0;
            cbEnd.Checked = false;
            textBoxYear.Text = "";
            textBoxMonth.Text = "";
            textBoxDay.Text = "";
            if (_bt.Text != "") {
                Appointment appo = wpm.getAppointment(startDate);
                overlayTextBoxSmall.Text = appo.Title;
                if (appo.Description != "") {
                    overlayTextBoxLarge.Text = appo.Description;
                }
                if (appo.Repeat != 255) {
                    cbRepeat.Checked = true;
                    ddRepeat.SelectedIndex = appo.Repeat;
                }
                if(appo.EndDate != new DateTime(9999,12, 31,00,00,00)){
                    cbEnd.Checked = true;
                    textBoxYear.Text = appo.EndDate.Year.ToString();
                    textBoxMonth.Text = appo.EndDate.Month.ToString();
                    textBoxDay.Text = appo.EndDate.Day.ToString();
                }
            }
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
        private void paintDates() {
            if (Session["Wochenplaner"] != null) {
                wpm = (WPModel)Session["Wochenplaner"];
            }
            DateTime[] dt = wpm.Dates;
            bt1.Text = "Montag" + Environment.NewLine + dt[0].ToShortDateString().ToString();
            bt2.Text = "Dienstag" + Environment.NewLine + dt[1].ToShortDateString().ToString();
            bt3.Text = "Mittwoch" + Environment.NewLine + dt[2].ToShortDateString().ToString();
            bt4.Text = "Donnerstag" + Environment.NewLine + dt[3].ToShortDateString().ToString();
            bt5.Text = "Freitag" + Environment.NewLine + dt[4].ToShortDateString().ToString();
            bt6.Text = "Samstag" + Environment.NewLine + dt[5].ToShortDateString().ToString();
            bt7.Text = "Sonntag" + Environment.NewLine + dt[6].ToShortDateString().ToString();
        }

        /// <summary>
        /// Reacts to the button click "backwards"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void moveWeekBackwards(object sender, EventArgs e) {
            wpm = (WPModel)Session["wpmodel"];
            if (wpm.Week <= 1) {
                wpm.Week = 52;
                wpm.Year -= 1;
            } else {
                wpm.Week -= 1;
            }
            paintWeekNumber(wpm.Week, wpm.Year);
            paintDates();
            paintAppointments();
        }

        /// <summary>
        /// Reacts to the button click "forwards"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void moveWeekForwards(object sender, EventArgs e) {
            wpm = (WPModel)Session["wpmodel"];
            if (wpm.Week >= 52) {
                wpm.Week = 1;
                wpm.Year += 1;
            } else {
                wpm.Week += 1;
            }
            paintWeekNumber(wpm.Week, wpm.Year);
            paintDates();
            paintAppointments();
        }

        #endregion

        #region Print
        /// <summary>
        /// Prints the acutally shown calendar view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void printCalendar(object sender, ImageClickEventArgs e) {
            string scriptTxt = "printPage();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "PrintScript", scriptTxt, true);
        }

        #endregion

        /// <summary>
        /// Disables the header buttons
        /// </summary>
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
            if (overlayTextBoxLogin.Text != "") {
                userData.Text = overlayTextBoxLogin.Text;
                if (Session["wpmodel"] != null && wpm != null) {
                    wpm = (WPModel)Session["wpmodel"];
                }
                wpm.sqlReadUser(overlayTextBoxLogin.Text);

                resetAppointmentDisplay();
                if (wpm.User.Name != overlayTextBoxLogin.Text) {
                    wpm.sqlWriteUser(registerNewUser());
                } else {
                    paintAppointments();
                }
            }
        }

        protected UserData registerNewUser() {
            return new UserData(overlayTextBoxLogin.Text);
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
            if (Session["wpmodel"] != null && wpm != null) {
                wpm = (WPModel)Session["wpmodel"];
                Appointment appo = null;

                string user = wpm.User.Id;
                string title = overlayTextBoxSmall.Text;
                string desc = overlayTextBoxLarge.Text;

                string cd = Regex.Match(overlayChoosenDate.Text, @"\(([^)]*)\)").Groups[1].Value;
                int mid = cd.Length / 2;
                string _day = cd.Substring(0, mid);
                int _time = Convert.ToInt32(cd.Substring(mid, mid));

                DateTime startDate = wpm.Dates[weekdayToInt(_day)].AddHours(_time);

                if (cbEnd.Checked) {
                    appo = new Appointment(user, title, desc, startDate, (byte)ddRepeat.SelectedIndex, new DateTime(Convert.ToInt32(textBoxYear.Text), Convert.ToInt32(textBoxMonth.Text), Convert.ToInt32(textBoxDay.Text), _time, 00, 00));
                } else if (cbRepeat.Checked) {
                    appo = new Appointment(user, title, desc, startDate, (byte)ddRepeat.SelectedIndex, new DateTime(9999, 12, 31));
                } else if (desc != "") {
                    appo = new Appointment(user, title, desc, startDate, (byte)255, new DateTime(9999, 12, 31));
                } else {
                    appo = new Appointment(user, title, null, startDate, (byte)255, new DateTime(9999, 12, 31));
                }

                wpm.sqlWriteAppointments(appo);
                paintAppointment(appo);
            }
        }

        /// <summary>
        /// Reacts to the button click "move"
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event e</param>
        protected void moveAppointment(object sender, EventArgs e) {
            //string scriptTxt = "openTimeOverlay();";
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OverlayScript", scriptTxt, true);
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