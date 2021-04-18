using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Model;
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for CreateAppointmentView.xaml
    /// </summary>
    public partial class CreateAppointmentView : Window
    {
        public List<Patient> Patients { get; set; }
        public List<Room> Rooms { get; set; }
        public PatientStorage storage;
        public RoomStorage rs;
        public CreateAppointmentView()
        {
            InitializeComponent();
            storage = new PatientStorage();
            Patients = storage.GetAll();
            rs = new RoomStorage();
            Rooms = rs.GetAll();
            this.DataContext = this;
            cmbPatients.SelectedIndex = 0;
            cmbRooms.SelectedIndex = 0;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var AppointmentID = int.Parse(idTB.Text);
            var format = "MM/dd/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var StartTime = DateTime.ParseExact(startTB.Text, format, provider);
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;
            var Patient = cmbPatients.SelectedItem;
            var Room = cmbRooms.SelectedItem;
            var appointment1 = new Appointment { AppointentId = AppointmentID, StartTime = StartTime, DurationInMunutes = DurationInMinutes, ApointmentDescription = ApointmentDescription, IsDeleted = false, Patient = (Patient)Patient, Room = (Room)Room };
            AppointmentStorage aps = new AppointmentStorage();
            aps.Save(appointment1);
            DoctorView.Appointments.Add(appointment1);
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
