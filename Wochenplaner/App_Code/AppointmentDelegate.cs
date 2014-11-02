using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Media;

namespace Wochenplaner.App_Code {
    // Declare the delegate handler for the event. The delegate
    // defines a signature that returns void and has no parameters.
    public delegate void AppointmentCreateEventHandler();
    public delegate void AppointmentErrorEventHandler();

    /// <summary>
    /// Zusammenfassungsbeschreibung für AppointmentDelegate
    /// </summary>
    public class AppointmentDelegate {
        // Declare the event of type AppointmentCreateEventHandler. Event handlers
        // for TriggerIt must have the same signature as MyEventHandler.
        public event AppointmentCreateEventHandler TriggerSuccess;
        public event AppointmentCreateEventHandler TriggerError;

        // Declare a method that triggers the event:
        public void _triggerSuccess() {
            TriggerSuccess();
        }

        // Declare a method that triggers the event:
        public void _triggerError() {
            TriggerError();
        }

        // Declare the methods that will be associated with the TriggerIt event.
        public void displayAppointment() {
            //SystemSounds.Beep.Play();
            SystemSounds.Asterisk.Play();
        }

        // Declare the methods that will be associated with the TriggerIt event.
        public void playErrorSound() {
            SystemSounds.Beep.Play();
        }
    }
}
