using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vezba
{
    public partial class PatientNotifications : Window
    {
        public static List<Prescription> prescriptions;
        public PatientNotifications()
        {
            InitializeComponent();
            PrescriptionStorage ps = new PrescriptionStorage();
            prescriptions = ps.GetAll();
        }

        public static void medicineNotification(Patient patient)
        {
            while (true)
            {
                foreach (Prescription p in prescriptions)
                {
                    GenerateNotificationsSpanTime(p);
                }
            }

            Thread.Sleep(TimeSpan.FromMinutes(5));
        }

        public static List<DateTime> GenerateNotificationsSpanTime(Prescription prescription)
        {
            List<DateTime> notifications = new List<DateTime>();
            DateTime start = new DateTime();
            start = prescription.StartDate;
            DateTime end = new DateTime();
            end = prescription.StartDate.AddDays(prescription.DurationInDays);

            while (DateTime.Now < end.Date)
            { 
                if (prescription.ReferencePeriod == Period.daily)
                {
                    for (int i = 0; i < prescription.Number; i++) {
                        notifications.Add(start.AddHours(i * 24 / prescription.Number));
                    }
                }
                start = start.AddDays(1);
            }

            foreach (DateTime dt in notifications) 
            {
                if (dt >= DateTime.Now  && dt <= DateTime.Now.AddMinutes(5)) {
                    MessageBox.Show("Podsetnik da lek " + prescription.Medicine.Name + "trebate popiti u " + dt.ToString("HH:mm"), "Terapija", MessageBoxButton.OK);
                    /*PatientNotifications noti = new PatientNotifications();
                    noti.ShowDialog();*/
                }
            }

            return notifications;
        }
    }
}
