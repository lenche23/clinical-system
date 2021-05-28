using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using vezba.Repository;

namespace vezba.SecretaryGUI
{
    public partial class SecretaryOverlapingAppointments : Window
    {
        public ObservableCollection<AppointmentForReschedulingDTO> Appointments { get; set; }
        public SecretaryOverlapingAppointments(Appointment emergencyAppointment)
        {
            InitializeComponent(); 
            this.DataContext = this;
            Appointments = new ObservableCollection<AppointmentForReschedulingDTO>();

            Patient.Content = emergencyAppointment.PatientName;
            Speciality.Content = emergencyAppointment.Doctor.Speciality;
            Room.Content = emergencyAppointment.RoomName;
            Duration.Content = emergencyAppointment.DurationInMunutes;

            emergencyAppointment.StartTime = DateTime.Now;
            AppointmentService appointmentService = new AppointmentService();
            List<AppointmentForReschedulingDTO> appointmentsForRescheduling = appointmentService.CreateAppointmentsForRescheduling(emergencyAppointment);
            foreach (AppointmentForReschedulingDTO afr in appointmentsForRescheduling)
                Appointments.Add(afr);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void RescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (overlapingAppointmentsTable.SelectedCells.Count > 0)
            {
                AppointmentForReschedulingDTO selectedAppointmentForRescheduling = (AppointmentForReschedulingDTO)overlapingAppointmentsTable.SelectedItem;
                AppointmentService appointmentService = new AppointmentService();
                List<Appointment> scheduledAppointments = appointmentService.GetAllAppointments();
                Appointment rescheduledAppointment = scheduledAppointments.FirstOrDefault(a => a.AppointentId.Equals(selectedAppointmentForRescheduling.AppointmentId));
                if (rescheduledAppointment != null)
                {
                    rescheduledAppointment.StartTime = selectedAppointmentForRescheduling.SuggestedTime;
                    appointmentService.EditAppointment(rescheduledAppointment);
                    Appointment previousAppointment = SecretaryAppointments.Appointments.FirstOrDefault(a => a.AppointentId.Equals(rescheduledAppointment.AppointentId));
                    if (previousAppointment != null)
                        SecretaryAppointments.Appointments[SecretaryAppointments.Appointments.IndexOf(previousAppointment)] = rescheduledAppointment;
                    Appointments.Remove(selectedAppointmentForRescheduling);
                }
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }

        
    }
}
