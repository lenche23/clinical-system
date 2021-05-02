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
    /// Interaction logic for EditAppointmentPage.xaml
    /// </summary>
    public partial class EditAppointmentPage : Page
    {
        public Appointment Selected { get; set; }
        private DoctorView dw;
        public PatientStorage storage;
        public DoctorStorage docstorage;
        public RoomStorage rs;
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }

        public EditAppointmentPage(Appointment selected, DoctorView dw)
        {
            InitializeComponent();
            storage = new PatientStorage();
            Patients = storage.GetAll();
            docstorage = new DoctorStorage();
            Doctors = docstorage.GetAll();
            rs = new RoomStorage();
            Rooms = rs.GetAll();
            this.Selected = selected;
            this.DataContext = this;
            this.dw = dw;
            if (Selected.Patient != null && Selected.Patient.Jmbg != null)
                cmbPatients.SelectedValue = Selected.Patient.Jmbg;
            if (Selected.Room != null)
                cmbRooms.SelectedValue = selected.Room.RoomNumber;
            if (Selected.Doctor != null && Selected.Doctor.Jmbg != null)
                cmbDoctors.SelectedValue = Selected.Doctor.Jmbg;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var AppointmentID = Selected.AppointentId;
            var format = "dd/MM/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var StartTime = DateTime.ParseExact(startTB.Text, format, provider);
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;

            var Patient = (Patient)cmbPatients.SelectedItem;
            var Room = (Room)cmbRooms.SelectedItem;
            var Doctor = (Doctor)cmbDoctors.SelectedItem;
            var IsEmergency = IsEmergencyCB.IsChecked;
            var appointment1 = new Appointment(AppointmentID, Patient, Doctor, Room, StartTime, DurationInMinutes, ApointmentDescription, (Boolean)IsEmergency);

            AppointmentStorage aps = new AppointmentStorage();
            aps.Update(appointment1);
            dw.Main.Content = new CalendarView(dw);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new CalendarView(dw);
        }
    }
}
