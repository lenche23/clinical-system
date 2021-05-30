using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using vezba.Repository;

namespace vezba.PatientPages
{
    public partial class CalendarPage : Page
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        private AppointmentService AppointmentService { get; set; }
        public CalendarPage()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentService = new AppointmentService();
            List<Appointment> tempToday = AppointmentService.GetPatientTodayAppointments();
            Appointments = new ObservableCollection<Appointment>(tempToday);           
        }

        private void TodayClick(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10 + (135 * 0), 0, 0, 0);
            Appointments.Clear();

            foreach (Appointment appointment in AppointmentService.GetAllAppointments())
            {
                if (appointment.StartTime.Date == DateTime.Today && appointment.Patient.Jmbg.Equals(PatientView.Patient.Jmbg))
                {
                    Appointments.Add(appointment);
                }
            }
        }

        private void WeekClick(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10 + (135 * 1), 0, 0, 0);
            Appointments.Clear();

            DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            foreach (Appointment appointment in AppointmentService.GetAllAppointments())
            {
                if (appointment.StartTime.Date >= DateTime.Today && appointment.StartTime.Date <= endOfWeek && appointment.Patient.Jmbg.Equals(PatientView.Patient.Jmbg))
                {
                    Appointments.Add(appointment);
                }
            }
        }

        private void MonthClick(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10 + (135 * 2), 0, 0, 0);
            Appointments.Clear();

            foreach (Appointment appointment in AppointmentService.GetAllAppointments())
            {
                if (appointment.StartTime.Date >= DateTime.Today && appointment.StartTime.Month == DateTime.Now.Month && appointment.Patient.Jmbg.Equals(PatientView.Patient.Jmbg))
                {
                    Appointments.Add(appointment);
                }
            }
        }
    }
}
