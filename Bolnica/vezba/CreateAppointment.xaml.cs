using Model;
using System;
using System.Collections.Generic;
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

namespace vezba
{
    /// <summary>
    /// Interaction logic for CreateAppointment.xaml
    /// </summary>
    public partial class CreateAppointment : Page
    {
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }
        public PatientStorage storage;
        public DoctorStorage docstorage;
        public RoomStorage rs;

        private DoctorView dw;

        private Calendar calendar;

        public CreateAppointment(DoctorView dw, Calendar calendar, Doctor doctor)
        {
            InitializeComponent();
            storage = new PatientStorage();
            Patients = storage.GetAll();
            docstorage = new DoctorStorage();
            Doctors = docstorage.GetAll();
            rs = new RoomStorage();
            Rooms = rs.GetAll();
            DataContext = this;
            cmbPatients.SelectedIndex = 0;
            cmbRooms.SelectedIndex = 0;
            this.dw = dw;
            this.calendar = calendar;
            if (doctor != null && doctor.Jmbg != null)
                cmbDoctors.SelectedValue = doctor.Jmbg;
        }

        public CreateAppointment(DoctorView dw, Calendar calendar, DateTime generatedStartTime, Doctor doctor)
        {
            InitializeComponent();
            storage = new PatientStorage();
            Patients = storage.GetAll();
            docstorage = new DoctorStorage();
            Doctors = docstorage.GetAll();
            rs = new RoomStorage();
            Rooms = rs.GetAll();
            DataContext = this;
            cmbPatients.SelectedIndex = 0;
            cmbRooms.SelectedIndex = 0;
            this.dw = dw;
            this.calendar = calendar;
            StartDatePicker.SelectedDate = generatedStartTime.Date;
            TimeTB.Text = generatedStartTime.Hour + ":" + generatedStartTime.Minute;
            if (doctor != null && doctor.Jmbg != null)
                cmbDoctors.SelectedValue = doctor.Jmbg;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            AppointmentStorage aps = new AppointmentStorage();
            var AppointmentID = aps.generateNextId();
            var startDate = StartDatePicker.SelectedDate;
            var hour = int.Parse(TimeTB.Text.Split(':')[0]);
            var minute = int.Parse(TimeTB.Text.Split(':')[1]);
            var startDateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, hour, minute, 0);
             
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;
            var Patient = cmbPatients.SelectedItem;
            var Room = cmbRooms.SelectedItem;
            var Doctor = cmbDoctors.SelectedItem;
            var IsEmergency = IsEmergencyCB.IsChecked;
            Appointment appointment = new Appointment(AppointmentID, (Patient)Patient, (Doctor)Doctor, (Room)Room, startDateTime, DurationInMinutes, ApointmentDescription, (Boolean)IsEmergency);
            aps.Save(appointment);
            dw.Main.GoBack();

            if (calendar.AddAppointmentToCurrentView(appointment))
            {
                calendar.ShowAppointment(appointment);
                calendar.SetScrollViewerToFirstAppointment();
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
