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
    /// Interaction logic for CreateAppointmentPage.xaml
    /// </summary>
    public partial class CreateAppointmentPage : Page
    {
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }
        public PatientStorage storage;
        public DoctorStorage docstorage;
        public RoomStorage rs;

        private DoctorView dw;
        public CreateAppointmentPage(DoctorView dw)
        {
            InitializeComponent();
            storage = new PatientStorage();
            Patients = storage.GetAll();
            docstorage = new DoctorStorage();
            Doctors = docstorage.GetAll();
            rs = new RoomStorage();
            Rooms = rs.GetAll();
            this.DataContext = this;
            cmbPatients.SelectedIndex = 0;
            cmbRooms.SelectedIndex = 0;
            cmbDoctors.SelectedIndex = 0;
            this.dw = dw;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            //var AppointmentID = int.Parse(idTB.Text);
            AppointmentStorage aps = new AppointmentStorage();
            var AppointmentID = aps.generateNextId();
            var format = "dd/MM/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var StartTime = DateTime.ParseExact(startTB.Text, format, provider);
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;
            var Patient = cmbPatients.SelectedItem;
            var Room = cmbRooms.SelectedItem;
            var Doctor = cmbDoctors.SelectedItem;
            var IsEmergency = IsEmergencyCB.IsChecked;
            Appointment appointment1 = new Appointment(AppointmentID, (Patient)Patient, (Doctor)Doctor, (Room)Room, StartTime, DurationInMinutes, ApointmentDescription, (Boolean)IsEmergency);
            aps.Save(appointment1);
            CalendarView.Appointments.Add(appointment1);
            dw.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
