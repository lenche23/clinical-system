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
using System.Windows.Shapes;
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for EditAppointmentView.xaml
    /// </summary>
    public partial class EditAppointmentView : Window
    {
        public Appointment Selected { get; set; }
        private DoctorView dw;
        public PatientStorage storage;
        public DoctorStorage docstorage;
        public RoomStorage rs;
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }

        public EditAppointmentView(Appointment selected, DoctorView dw)
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
            if(Selected.Patient != null && Selected.Patient.Jmbg != null)
                cmbPatients.SelectedValue = Selected.Patient.Jmbg;
            if (Selected.Room != null)
                cmbRooms.SelectedValue = selected.Room.RoomNumber;
            if(Selected.Doctor != null && Selected.Doctor.Jmbg != null)
                cmbDoctors.SelectedValue = Selected.Patient.Jmbg;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var AppointmentID = int.Parse(idTB.Text);
            var format = "dd/MM/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var StartTime = DateTime.ParseExact(startTB.Text, format, provider);
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;

            var Patient = (Patient)cmbPatients.SelectedItem;
            var Room = (Room)cmbRooms.SelectedItem;
			      var Doctor = (Doctor)cmbDoctors.SelectedItem;
            var appointment1 = new Appointment(AppointmentID, Patient, Doctor, Room, StartTime, DurationInMinutes, ApointmentDescription);

            //var appointment1 = new Appointment(StartTime, DurationInMinutes, ApointmentDescription, AppointmentID, (Doctor) Doctor, (Room)Room, (Patient)Patient);

            AppointmentStorage aps = new AppointmentStorage();
            aps.Update(appointment1);
            Selected.StartTime = StartTime;
            Selected.DurationInMunutes = DurationInMinutes;
            Selected.ApointmentDescription = ApointmentDescription;

            Selected.Patient = (Patient)Patient;
            //Selected.patient = (Patient)Patient;
            Selected.Doctor = (Doctor)Doctor;
            Selected.Room = (Room)Room;
            dw.listViewAppointments.Items.Refresh();
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
