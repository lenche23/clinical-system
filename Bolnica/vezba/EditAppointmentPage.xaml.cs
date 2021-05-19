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
        public PatientFileRepository fileRepository;
        public DoctorFileRepository docstorage;
        public RoomFileRepository rs;
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }

        private Calendar calendar;

        private Grid appointmentGrid;

        public EditAppointmentPage(Appointment selected, DoctorView dw, Calendar calendar, Grid appointmentGrid)
        {
            InitializeComponent();
            fileRepository = new PatientFileRepository();
            Patients = fileRepository.GetAll();
            docstorage = new DoctorFileRepository();
            Doctors = docstorage.GetAll();
            rs = new RoomFileRepository();
            Rooms = rs.GetAll();
            Selected = selected;
            DataContext = this;
            this.dw = dw;
            if (Selected.Patient != null && Selected.Patient.Jmbg != null)
                cmbPatients.SelectedValue = Selected.Patient.Jmbg;
            if (Selected.Room != null)
                cmbRooms.SelectedValue = selected.Room.RoomNumber;
            if (Selected.Doctor != null && Selected.Doctor.Jmbg != null)
                cmbDoctors.SelectedValue = Selected.Doctor.Jmbg;
            this.calendar = calendar;
            this.appointmentGrid = appointmentGrid;
            StartDatePicker.SelectedDate = selected.StartTime.Date;
            TimeTB.Text = Selected.StartTime.ToString("t");
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var AppointmentID = Selected.AppointentId;
            var startDate = StartDatePicker.SelectedDate;
            var hour = int.Parse(TimeTB.Text.Split(':')[0]);
            var minute = int.Parse(TimeTB.Text.Split(':')[1]);
            var startDateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, hour, minute, 0);
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;

            var Patient = (Patient)cmbPatients.SelectedItem;
            var Room = (Room)cmbRooms.SelectedItem;
            var Doctor = (Doctor)cmbDoctors.SelectedItem;
            var IsEmergency = (Boolean)IsEmergencyCB.IsChecked;
            var appointment = new Appointment(AppointmentID, Patient, Doctor, Room, startDateTime, DurationInMinutes, ApointmentDescription, IsEmergency);

            AppointmentFileRepository aps = new AppointmentFileRepository();
            aps.Update(appointment);
            calendar.RemoveAppointment(appointmentGrid);
            calendar.AddAppointmentToCurrentView(appointment);

            dw.Main.GoBack();
            dw.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
