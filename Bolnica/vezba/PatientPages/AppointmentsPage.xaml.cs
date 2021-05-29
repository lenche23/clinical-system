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
using System.Windows.Navigation;
using System.Windows.Shapes;
using vezba.Repository;

namespace vezba.PatientPages
{
    public partial class AppointmentsPage : Page
    {
        public static Patient Patient { get; set; }
        public List<EventsLog> list;
        public PatientFileRepository pps;
        public EventsLogFileRepository fileRepository;
        public AppointmentsPage()
        {
            InitializeComponent();
            /*fileRepository = new EventsLogFileRepository();
            list = fileRepository.Load();
            pps = new PatientFileRepository();
            Patient patient = pps.GetOne("1008985563244");
            Patient = patient;
            Patient.IsBlocked = false;*/
            /*for (int i = 0; i < list.Count; i++)//kroz pacijente
            {
                if (list[i].PatientJmbg.Equals(Patient.Jmbg))
                {
                    if (!Patient.IsBlocked)
                    {
                        EventsLog log = list[i];
                        List<DateTime> events = log.EventDates;
                        DateTime reff = DateTime.Now.Date.AddDays(-5);
                        int count = 0;
                        foreach (DateTime event1 in events)
                        {
                            if (event1.Date >= reff)
                            {
                                count++;
                            }
                        }
                        if (count == 10)
                        {
                            Patient.IsBlocked = true;
                            pps.Update(Patient);
                            events.Clear();
                            fileRepository.Update(log);
                        }
                    }
                }
            }*/
        }

        private void ButtonOrderAppointment_Click(object sender, RoutedEventArgs e)
        {
            fileRepository = new EventsLogFileRepository();
            list = fileRepository.ReadFromFile();
            pps = new PatientFileRepository();
            Patient patient = pps.GetOne("1008985563244");
            Patient = patient;
            Patient.IsBlocked = false;
            for (int i = 0; i < list.Count; i++)//kroz pacijente
            {
                if (list[i].PatientJmbg.Equals(Patient.Jmbg))
                {
                    if (!Patient.IsBlocked)
                    {
                        EventsLog log = list[i];
                        List<DateTime> events = log.EventDates;
                        DateTime reff = DateTime.Now.Date.AddDays(-5);
                        int count = 0;
                        foreach (DateTime event1 in events)
                        {
                            if (event1.Date >= reff)
                            {
                                count++;
                            }
                        }
                        if (count == 10)
                        {
                            Patient.IsBlocked = true;
                            pps.Update(Patient);
                            events.Clear();
                            fileRepository.Update(log);
                        }
                    }
                }
            }
            if (Patient.IsBlocked)
            {
                MessageBox.Show("Trenutno ne mozete da zakazete pregled!", "Blokirani ste");
            }
            else
            {
                var zakaziPegled = new OrderAppointmentPopup();
                zakaziPegled.Show();
            }
        }

        private void ButtonChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            fileRepository = new EventsLogFileRepository();
            list = fileRepository.ReadFromFile();
            pps = new PatientFileRepository();
            Patient patient = pps.GetOne("1008985563244");
            Patient = patient;
            Patient.IsBlocked = false;
            for (int i = 0; i < list.Count; i++)//kroz pacijente
            {
                if (list[i].PatientJmbg.Equals(Patient.Jmbg))
                {
                    if (!Patient.IsBlocked)
                    {
                        EventsLog log = list[i];
                        List<DateTime> events = log.EventDates;
                        DateTime reff = DateTime.Now.Date.AddDays(-5);
                        int count = 0;
                        foreach (DateTime event1 in events)
                        {
                            if (event1.Date >= reff)
                            {
                                count++;
                            }
                        }
                        if (count == 10)
                        {
                            Patient.IsBlocked = true;
                            pps.Update(Patient);
                            events.Clear();
                            fileRepository.Update(log);
                        }
                    }
                }
            }
            if (Patient.IsBlocked)
            {
                MessageBox.Show("Trenutno ne mozete da izmenite pregled!", "Blokirani ste");
            }
            else
            {
                this.NavigationService.Navigate(new ChangeAppointmentPage());
            }
        }

        private void ButtonCancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            var otkaziPregled = new CancelAppointmentView();
            otkaziPregled.Show();
        }

        private void ButtonHistoryAppointment_Click(object sender, RoutedEventArgs e)
        {
            var istorijaPregleda = new AppointmentHistory();
            istorijaPregleda.Show();
        }
    }
}
