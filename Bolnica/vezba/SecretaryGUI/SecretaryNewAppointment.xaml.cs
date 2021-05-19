using Model;
using System;
using System.Collections.Generic;
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

namespace vezba.SecretaryGUI
{
    public partial class SecretaryNewAppointment : Window
    {
        public SecretaryNewAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientFileRepository ps = new PatientFileRepository();
            List<Patient> patients = ps.GetAll();
            Patient.ItemsSource = patients;
            DoctorFileRepository ds = new DoctorFileRepository();
            List<Doctor> doctors = ds.GetAll();
            Doctor.ItemsSource = doctors;
            RoomFileRepository rs = new RoomFileRepository();
            List<Room> rooms = rs.GetAll();
            Room.ItemsSource = rooms;
            List<int> durations = new List<int> { 15, 30, 45, 60 };
            Duration.ItemsSource = durations;
            List<string> hours = new List<string> { "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
            Hours.ItemsSource = hours;
            List<string> minutes = new List<string> { "00", "15", "30", "45" };
            Minutes.ItemsSource = minutes;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEntries() == false)
                return;
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            Appointment appointment = new Appointment(appointmentFileRepository.generateNextId(), (Patient)Patient.SelectedItem, (Doctor)Doctor.SelectedItem, (Room)Room.SelectedItem, GetTime(), (int)Duration.SelectedItem, Description.Text);

            if (GetOverlapingAppoinments(appointment).Count == 0)
            {
                Boolean isSuccess = appointmentFileRepository.Save(appointment);
                if (isSuccess)
                    SecretaryAppointments.Appointments.Add(appointment);
                this.Close();
            }
            else
                MessageBox.Show("Termin je zauzet! Izaberite drugo vreme.\n Prvi sledeci dostupan termin za unete kriterijume je " + FindNextFreeAppointmentStartTime(appointment).ToString("dd.MM.yyyy. HH:mm"));
            
        }

        private List<Appointment> GetOverlapingAppoinments(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            List<Appointment> overlapingAppointments = new List<Appointment>();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (AppointmentsOverlap(appointment, scheduledAppointments[i]))
                {
                    overlapingAppointments.Add(scheduledAppointments[i]);
                }
            }
            return overlapingAppointments;
        }

        private Boolean AppointmentsOverlap(Appointment appointment1, Appointment appointment2)
        {
            if (AppointmentsShareDoctor(appointment1, appointment2) || AppointmentsSharePatient(appointment1, appointment2) || AppointmentsShareRoom(appointment1, appointment2))
            {
                if (DateTime.Compare(appointment2.EndTime, appointment1.StartTime) <= 0) //drugi zavrsava pre pocetka prvog
                    return false;
                else if (DateTime.Compare(appointment1.EndTime, appointment2.StartTime) <= 0) //prvi zavrsava pre pocetka drugog
                    return false;
                else
                    return true;
            }
            return false;
        }

        private Boolean AppointmentsSharePatient(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Patient.Jmbg.Equals(appointment2.Patient.Jmbg);
        }

        private Boolean AppointmentsShareDoctor(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Doctor.Jmbg.Equals(appointment2.Doctor.Jmbg);
        }

        private Boolean AppointmentsShareRoom(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Room.RoomNumber == appointment2.Room.RoomNumber;
        }


        private DateTime FindNextFreeAppointmentStartTime(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in scheduledAppointments)
                {
                    if (AppointmentsOverlap(a, appointment))
                    {
                        appointment.StartTime = CalculateNewStartTime(a.EndTime);
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment.StartTime;
        }

        private DateTime CalculateNewStartTime(DateTime overlapingAppointmentEndTime)
        {
            DateTime newStartTime = overlapingAppointmentEndTime;
            if (IsAfterWorkingHours(newStartTime))
            {
                newStartTime = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, 8, 0, 0);
                newStartTime = newStartTime.AddDays(1);
            }
            return newStartTime;
        }

        private Boolean IsAfterWorkingHours(DateTime time)
        {
            DateTime endOfDay = new DateTime(time.Year, time.Month, time.Day, 19, 45, 0);
            if (time >= endOfDay)
                return true;

            return false;
        }

        //HCI deo
        private Boolean ValidateEntries()
        {
            if (Patient.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli pacijenta!");
                return false;
            }
            if (Doctor.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli lekara!");
                return false;
            }
            if (Room.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli prostoriju!");
                return false;
            }
            if (Hours.SelectedItem == null || Minutes.SelectedItem == null)
            {
                MessageBox.Show("Niste izabrali vreme!");
                return false;
            }
            if (DateTime.Compare(GetTime(), DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je vec prosao!");
                return false;
            }
            if (Duration.SelectedItem == null)
            {
                MessageBox.Show("Niste izabrali trajanje termina!");
                return false;
            }
            return true;
        }

        private DateTime GetTime()
        {
            DateTime selectedDate = new DateTime(1900, 1, 1);
            try
            {
                selectedDate = Date.SelectedDate.Value.Date;
            }
            catch
            {
                MessageBox.Show("Niste izabrali datum!");
                return selectedDate;
            }
            string hours = (string)Hours.SelectedItem;
            string minutes = (string)Minutes.SelectedItem;
            String selectedTime = (Convert.ToString(hours) + ":" + Convert.ToString(minutes)).Trim();
            DateTime time;
            DateTime.TryParseExact(selectedTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out time);
            DateTime startTime = selectedDate.Date.Add(time.TimeOfDay);
            return startTime;
        }
    }
}
