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
    public partial class OrderDoctorAppointment : Page
    {
        private DoctorService DoctorService { get; set; }
        private AppointmentService AppointmentService { get; set; }
        //private EventsLogService EventsLogService { get; set; }
        public Doctor Doctor{ get; set; }
        public OrderDoctorAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorService = new DoctorService();
            AppointmentService = new AppointmentService();
            Doctor = DoctorService.LoadDoctor();
        }

        private void OrderAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (HasAllInfo())
            {
                DateTime dateTimeFinal = ParseTime();
                Appointment a = new Appointment(Doctor, dateTimeFinal, PatientView.Patient);
               
                if (CanSchedule())
                {
                    AppointmentService.PatientCanScheduleAppointment(a);
                }
                else
                {
                    PatientNotification noti = new PatientNotification("Unet datum nije validan!");
                    noti.ShowDialog();
                }
            }
            else
            {
                PatientNotification noti = new PatientNotification("Molimo Vas popunite sva potrebna polja!");
                noti.ShowDialog();
            }
        }

        private Boolean HasAllInfo()
        {
            if (calendar.SelectedDate != null && (comboBox.Text != null && !comboBox.Text.Equals("")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private DateTime ParseTime()
        {
            DateTime selectedDate = calendar.SelectedDate.Value.Date;
            selectedDate.ToString("MM/dd/yyyy");
            String selectedTime = comboBox.Text;
            DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime dateTimeFinal = selectedDate.Date.Add(dateTime.TimeOfDay);
            return dateTimeFinal;
        }

        private Boolean CanSchedule()
        {
            DateTime selectedDate = calendar.SelectedDate.Value.Date;
            int diff = (selectedDate - DateTime.Now.Date).Days;
            if (diff <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
