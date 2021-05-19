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
            AppointmentStorage storage = new AppointmentStorage();

            DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -(int)DateTime.Today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);
           

            foreach (Appointment appointment in storage.GetAll())
            {
                if (appointment.StartTime.Date == DateTime.Today && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    tempToday.Add(appointment);

                }
                if (appointment.StartTime.Date >= DateTime.Today && appointment.StartTime.Date <= endOfWeek && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    tempWeek.Add(appointment);
                    tempWeek.Sort((x, y) => y.StartTime.CompareTo(x.StartTime));
                    tempWeek.Reverse();
                }
                if (appointment.StartTime.Date >= DateTime.Today && appointment.StartTime.Month == DateTime.Now.Month && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    tempMonth.Add(appointment);
                    tempMonth.Sort((x, y) => y.StartTime.CompareTo(x.StartTime));
                    tempMonth.Reverse();
                }
                Appointments = new ObservableCollection<Appointment>(tempMonth);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (135 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    GridTable.Background = Brushes.Transparent;
                    Appointments = new ObservableCollection<Appointment>(tempToday);
                    break;
                case 1:
                    GridTable.Background = Brushes.Transparent;
                    Appointments = new ObservableCollection<Appointment>(tempWeek);
                    break;
                case 2:
                    GridTable.Background = Brushes.Transparent;
                    Appointments = new ObservableCollection<Appointment>(tempMonth);
                    break;
            }
        }
    }
}
