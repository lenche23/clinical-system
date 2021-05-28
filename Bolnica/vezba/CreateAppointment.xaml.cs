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
using vezba.Repository;

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
        public PatientFileRepository fileRepository;
        public DoctorFileRepository docstorage;
        public RoomFileRepository rs;

        private DoctorView dw;

        private Calendar calendar;

        public CreateAppointment(DoctorView dw, Calendar calendar, Doctor doctor)
        {
            InitializeComponent();
            fileRepository = new PatientFileRepository();
            Patients = fileRepository.GetAll();
            docstorage = new DoctorFileRepository();
            Doctors = docstorage.GetAll();
            rs = new RoomFileRepository();
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
            fileRepository = new PatientFileRepository();
            Patients = fileRepository.GetAll();
            docstorage = new DoctorFileRepository();
            Doctors = docstorage.GetAll();
            rs = new RoomFileRepository();
            Rooms = rs.GetAll();
            DataContext = this;
            cmbPatients.SelectedIndex = 0;
            cmbRooms.SelectedIndex = 0;
            this.dw = dw;
            this.calendar = calendar;
            StartDatePicker.SelectedDate = generatedStartTime.Date;
            TimeTB.Text = generatedStartTime.ToString("t");
            if (doctor != null && doctor.Jmbg != null)
                cmbDoctors.SelectedValue = doctor.Jmbg;
            DurationTB.Text = "60";
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            AppointmentFileRepository aps = new AppointmentFileRepository();
            var AppointmentID = aps.GenerateNextId();
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

            calendar.AddAppointmentToCurrentView(appointment);

        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
