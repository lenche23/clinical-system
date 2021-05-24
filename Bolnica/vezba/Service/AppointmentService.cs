using Model;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using vezba;
using vezba.Repository;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentFileRepository Repository { get; set; }
        private Appointment ChangingAppointment { get; set; }
        public AppointmentService()
        {
            Repository = new AppointmentFileRepository();
            ChangingAppointment = new Appointment();
        }

        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************
        public Boolean Update(Appointment appointment)
        {
            return Repository.Update(appointment);
        }

        public Boolean MoveableAppointment(DateTime initDate, DateTime pickedDate)
        {
            int diff = (pickedDate - initDate).Days;
            if (diff > 2)
                return false;

            return true;
        }

        public void ChangeAppointment(Appointment initAppointment, DateTime pickedDate, String pickedTime)
        {
            int id = initAppointment.AppointentId;
            pickedDate.ToString("MM/dd/yyyy");
            Doctor initDoctor = initAppointment.Doctor;
            DateTime parsedTime = DateTime.ParseExact(pickedTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime movedDate = pickedDate.Date.Add(parsedTime.TimeOfDay);

            Appointment changedAppointment = new Appointment(initDoctor, movedDate, PatientView.Patient);
            changedAppointment.AppointentId = id;
            Update(changedAppointment);

            Appointment idAppointment = ChangeAppointmentView.Appointments.FirstOrDefault(a => a.AppointentId.Equals(id));
            if (idAppointment != null)
            {
                ChangeAppointmentView.Appointments[ChangeAppointmentView.Appointments.IndexOf(idAppointment)] = changedAppointment;
            }
        }
        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}