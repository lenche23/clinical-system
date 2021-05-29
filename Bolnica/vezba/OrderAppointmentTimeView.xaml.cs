using Model;
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
using System.Windows.Shapes;
using vezba.Repository;

namespace vezba
{
    public partial class OrderAppointmentTimeView : Window
    {
        public static ObservableCollection<Doctor> Doctors { get; set; }

        public OrderAppointmentTimeView()
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorFileRepository ps = new DoctorFileRepository();
            List<Doctor> temp = ps.GetAll();
            Doctors = new ObservableCollection<Doctor>(temp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate != null && (comboBox.Text != null && !comboBox.Text.Equals("")) && doctorsTable.SelectedItems.Count > 0)
            {
                Doctor selectedDoctor = (Doctor)doctorsTable.SelectedItem;
                DateTime selectedDate = datePicker.SelectedDate.Value.Date;
                
                selectedDate.ToString("MM/dd/yyyy");
                String selectedTime = comboBox.Text;
                DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
                DateTime dateTimeFinal = selectedDate.Date.Add(dateTime.TimeOfDay);
                PatientFileRepository pps = new PatientFileRepository();
                Patient patient = pps.GetOne("1008985563244");

                AppointmentFileRepository fileRepository = new AppointmentFileRepository();
                int id = fileRepository.generateNextId();
                Appointment a = new Appointment(selectedDoctor, dateTimeFinal, patient);
                a.AppointentId = id;

                /*EventsLogFileRepository eventsLogFileRepository = new EventsLogFileRepository();
                String patientJMBG = patient.Jmbg;
                List<DateTime> events = new List<DateTime>();*/

                EventsLogFileRepository eventsLogFileRepository = new EventsLogFileRepository();
                List<EventsLog> list = eventsLogFileRepository.Load();
                String patientJMBG = patient.Jmbg;
                List<DateTime> events = new List<DateTime>();
                DateTime log = DateTime.Now;

                int diff = (selectedDate - DateTime.Now.Date).Days;
                if (diff <= 0)
                {
                    MessageBox.Show("Unet datum nije validan!");
                    this.Close();
                }
                else {
                    if (GetOverlapingAppoinments(a).Count == 0)
                    {
                        Boolean b = fileRepository.Save(a);
                        if (b)
                        {
                            PatientView.Apps.Add(a);
                        }
                        log = DateTime.Now;
                        foreach (EventsLog elog in list)
                        {
                            if (elog.PatientJmbg.Equals(patientJMBG))
                            {
                                elog.EventDates.Add(log);
                                eventsLogFileRepository.Update(elog);
                            }
                        }
                        MessageBox.Show("Uspešno ste zakazali pregled.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Termin je zauzet! Izaberite drugo vreme.");
                    }
                }
                
                /*EventsLog eventsLog = new EventsLog(patientJMBG, events);
                eventsLogFileRepository.SaveDeclinedMedicine(eventsLog);*/
            }
            else
            {
                MessageBox.Show("Molimo Vas popunite sva potrebna polja!");
            }
        }

        private List<Appointment> GetOverlapingAppoinments(Appointment appointment)
        {
            AppointmentFileRepository aps = new AppointmentFileRepository();
            List<Appointment> appointments = aps.GetAll();
            List<Appointment> overlaping = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (this.DoShareResources(appointment, appointments[i]))
                {
                    overlaping.Add(appointments[i]);
                }
            }
            return overlaping;
        }

        private Boolean DoShareResources(Appointment a1, Appointment a2)
        {
            if (a1.Doctor.Jmbg.Equals(a2.Doctor.Jmbg) || a1.Patient.Jmbg.Equals(a2.Patient.Jmbg))
            {
                DateTime beginning1 = a1.StartTime;
                DateTime end1 = a1.StartTime.AddMinutes(a1.DurationInMunutes);
                DateTime beginning2 = a2.StartTime;
                DateTime end2 = a2.StartTime.AddMinutes(a2.DurationInMunutes);
                if (DateTime.Compare(end2, beginning1) <= 0) //drugi zavrsava pre pocetka prvog
                {
                    return false;
                }
                else if (DateTime.Compare(end1, beginning2) <= 0) //prvi zavrsava pre pocetka drugog
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;

        }
    }
}
