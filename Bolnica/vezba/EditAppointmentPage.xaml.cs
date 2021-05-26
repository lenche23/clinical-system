using Model;
using Service;
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
    /// Interaction logic for EditAppointmentPage.xaml
    /// </summary>
    public partial class EditAppointmentPage : Page
    {
        public Appointment Selected { get; set; }
        private DoctorView doctorView;
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }
        private Calendar calendar;
        private Grid appointmentGrid;
        public EditAppointmentPage(Appointment selected, DoctorView doctorView, Calendar calendar, Grid appointmentGrid)
        {
            InitializeComponent();

            PatientService patientService = new PatientService();
            Patients = patientService.GetAll();

            DoctorService doctorService = new DoctorService();
            Doctors = doctorService.GetAll();

            RoomService roomService = new RoomService();
            Rooms = roomService.GetAll();

            Selected = selected;
            DataContext = this;
            this.doctorView = doctorView;

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
            var appointmentId = Selected.AppointentId;
            var startDate = StartDatePicker.SelectedDate;
            var hour = int.Parse(TimeTB.Text.Split(':')[0]);
            var minute = int.Parse(TimeTB.Text.Split(':')[1]);
            var startDateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, hour, minute, 0);
            var durationInMinutes = int.Parse(DurationTB.Text);
            var appointmentDescription = DescriptionTB.Text;

            var patient = (Patient)cmbPatients.SelectedItem;
            var room = (Room)cmbRooms.SelectedItem;
            var doctor = (Doctor)cmbDoctors.SelectedItem;
            var isEmergency = (Boolean)IsEmergencyCB.IsChecked;
            var updatedAppointment = new Appointment(appointmentId, patient, doctor, room, startDateTime, durationInMinutes, appointmentDescription, isEmergency);

            AppointmentService appointmentService = new AppointmentService();
            appointmentService.Update(updatedAppointment);

            calendar.RemoveAppointment(appointmentGrid);
            calendar.AddAppointmentToCurrentView(updatedAppointment);

            doctorView.Main.GoBack();
            doctorView.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.GoBack();
        }
    }
}
