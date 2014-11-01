using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Media;

namespace Wochenplaner.App_Code {
    // Declare the delegate handler for the event. The delegate
    // defines a signature that returns void and has no parameters.
    public delegate void AppointmentCreateEventHandler();

    /// <summary>
    /// Zusammenfassungsbeschreibung für AppointmentDelegate
    /// </summary>
    public class AppointmentDelegate {
        // Declare the event of type AppointmentCreateEventHandler. Event handlers
        // for TriggerIt must have the same signature as MyEventHandler.
        public event AppointmentCreateEventHandler TriggerIt;

        // Declare a method that triggers the event:
        public void Trigger() {
            TriggerIt();
        }
        // Declare the methods that will be associated with the TriggerIt event.
        public void displayAppointment() {
            SystemSounds.Beep.Play();
        }
    }
}
