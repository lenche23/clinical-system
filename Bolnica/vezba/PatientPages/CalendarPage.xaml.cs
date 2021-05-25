using Model;
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
        private List<Appointment> tempToday = new List<Appointment>();
        private List<Appointment> tempWeek = new List<Appointment>();
        private List<Appointment> tempMonth = new List<Appointment>();
        public CalendarPage()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentFileRepository storage = new AppointmentFileRepository();

            foreach (Appointment appointment in storage.GetAll())
            {
                if (appointment.StartTime.Date == DateTime.Today && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    tempToday.Add(appointment);
                    tempToday.Sort((x, y) => y.StartTime.CompareTo(x.StartTime));
                    tempToday.Reverse();
                }
                Appointments = new ObservableCollection<Appointment>(tempToday);
            }
        }

        private void TodayClick(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10 + (135 * 0), 0, 0, 0);
            Appointments.Clear();
            AppointmentFileRepository storage = new AppointmentFileRepository();

            foreach (Appointment appointment in storage.GetAll())
            {
                if (appointment.StartTime.Date == DateTime.Today && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    Appointments.Add(appointment);
                }
            }
        }

        private void WeekClick(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10 + (135 * 1), 0, 0, 0);
            Appointments.Clear();
            AppointmentFileRepository storage = new AppointmentFileRepository();

            DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);


            foreach (Appointment appointment in storage.GetAll())
            {
                if (appointment.StartTime.Date >= DateTime.Today && appointment.StartTime.Date <= endOfWeek && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    Appointments.Add(appointment);
                }
            }
        }

        private void MonthClick(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10 + (135 * 2), 0, 0, 0);
            Appointments.Clear();
            AppointmentFileRepository storage = new AppointmentFileRepository();

            foreach (Appointment appointment in storage.GetAll())
            {
                if (appointment.StartTime.Date >= DateTime.Today && appointment.StartTime.Month == DateTime.Now.Month && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    Appointments.Add(appointment);
                }
            }
        }
    }
}
