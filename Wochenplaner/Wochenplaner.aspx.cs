using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Globalization;

using Wochenplaner.App_Code;

namespace Wochenplaner {
    public partial class Wochenplaner: System.Web.UI.Page {
        WPModel wpm;
        UserData ud;

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["wpmodel"] == null) {
                wpm = new WPModel();
                Session["wpmodel"] = wpm;
            } else {
                wpm = (WPModel)Session["wpmodel"];
            }
            if (Session["user"] == null) {
                ud = new UserData();
                Session["user"] = ud;
            } else {
                ud = (UserData)Session["user"];
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
                    return 1;
                case "DI":
                    return 2;
                case "MI":
                    return 3;
                case "DO":
                    return 4;
                case "FR":
                    return 5;
                case "SA":
                    return 6;
                case "SO":
                    return 7;
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

            //Find control on page.
            Button chosenButton = (Button)FindControl(_appo.getShortWeekday() + _appo.StartDate.Hour);
            if (chosenButton != null) {
                if (_desc != null) {
                    chosenButton.Text = _title + Environment.NewLine + _desc;
                } else {
                    chosenButton.Text = _title;
                }
                chosenButton.BackColor = Color.FromArgb(170, 117, 57); // Paints the button in a other color to shot that an Appointment is present
            } else {
            }
        }

        /// <summary>
        /// Fades in the overlay for the appointment creation.</summary>
        /// <param name="dateTime">datetime takes a string containing the date and time where to create the
        /// appointment</param>
        public void fadeInOverlay(string _day, int _time) {
            if (Session["wpmodel"] != null) {
                wpm = (WPModel)Session["wpmodel"];
            }

            string scriptTxt = "openInputOverlay();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OverlayScript", scriptTxt, true);
            overlayChoosenDate.Text = "Zeit: " + wpm.getLongWeekday(weekdayToInt(_day)) + ", " + wpm.Dates[weekdayToInt(_day)].ToShortDateString() + " um " + _time + ":00 Uhr";
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
        }

        /// <summary>
        /// Event reaction to handle the button klick on the webform.</summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event Argument</param>
        protected void closeOverlay(object sender, EventArgs e) {
            //TODO
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
        }

        /// <summary>
        /// Prints the acutally shown calendar view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void printCalendar(object sender, ImageClickEventArgs e) {

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
            //string scriptTxt = "openLoginOverlay();";
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OverlayScript", scriptTxt, true);
            //TODO create user object
        }

        protected void loginUser(object sender, EventArgs e) {
            //userData.Text = overlayTextBoxLogin.Text;
            //sqlRead();
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
            if (Session["wpmodel"] != null) {
                wpm = (WPModel)Session["wpmodel"];
                Appointment appo = null;

                string user = overlayTextBoxLarge.t;
                string title = reader.GetString(2);
                string desc = reader.GetString(3);
                DateTime startDate = reader.GetDateTime(4);
                DateTime endDate = reader.GetDateTime(5);
                byte repeat = reader.GetByte(6);

                if (repeat != 0) {
                    appo = new Appointment(user, title, desc, startDate, endDate, repeat);
                } else if (endDate != null) {
                    appo = new Appointment(user, title, desc, startDate, endDate);
                } else if (desc != null) {
                    appo = new Appointment(user, title, desc, startDate);
                } else {
                    appo = new Appointment(user, title, startDate);
                }

                wpm.sqlWrite();
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