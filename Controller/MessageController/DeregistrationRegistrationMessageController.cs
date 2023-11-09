using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller.MessageController
{
    internal class DeregistrationRegistrationMessageController
    {
        public static string BuildDeregistrationRegistrationMessage(string messageToBuild)
        {
            string message = "Unbekannter Fehler";

            switch (messageToBuild)
            {
                case "DateIsInThePast":
                    message = $"Das angegebene Datum liegt in der Vergangenheit. Eine Abmeldung für die Vergangenheit ist nicht möglich!";
                    break;
                case "DateIsAWeekendDay":
                    message = $"Das angegebene Datum ist ein Samstag oder Sonntag! Für diese Tage wird keine Abmeldung benötigt";
                    break;
            }

            return message;
        }

        public static string BuildDeregistrationRegistrationMessage(string messageToBuild, string name)
        {
            string message = "Unbekannter Fehler";

            switch (messageToBuild)
            {
                case "CompleteDeregistration":
                    message = $"Ich habe {name} erfolgreich Abgemeldet";
                    break;
                case "ExistsDeregistration":
                    message = $"Für den angegebenen Zeitraum existiert für {name} bereits eine Abmeldung!";
                    break;
                case "CompleteRegistration":
                    message = $"Ich habe {name} für den angegebenen Zeitraum wieder Angemeldet";
                    break;
                case "NoExistingDeregistration":
                    message = $"Es gibt für den Zeitraum keine Abmeldung für {name}! Eine Anmeldung ist somit nicht nötig";
                    break;
            }

            return message;
        }

        public static string BuildDeregistrationRegistrationMessage(string messageToBuild, DateTime dateFrom, DateTime dateTo)
        {
            var message = "Unbekannter Fehler";

            switch (messageToBuild)
            {
                case "DatePeriodError":
                    message = $"Der angegebene Zeitraum passt nicht zusammen. Bitte überprüfe den angegebenen Zeitraum auf beispielsweise Fehler in den Jahreszahlen ({dateFrom.ToString("dd.MM.yyyy")} - {dateTo.ToString("dd.MM.yyyy")})";
                    break;
            }

            return message;
        }
    }
}
