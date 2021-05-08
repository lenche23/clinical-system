using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace vezba
{
    public partial class CancelAppointmentView : Window
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }
        public CancelAppointmentView()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage ps = new AppointmentStorage();
            List<Appointment> temp = new List<Appointment>();
            foreach (Appointment appointment in ps.GetAll())
            {
                if (appointment.StartTime.Date > DateTime.Now && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    temp.Add(appointment);

                }
            }
            Appointments = new ObservableCollection<Appointment>(temp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cancelTable.SelectedItems.Count > 0)
            {
                Appointment a = (Appointment)cancelTable.SelectedItem;
                AppointmentStorage storage = new AppointmentStorage();
                Boolean bla =  storage.Delete(a.AppointentId);
                Appointments.Remove(a);
                MessageBox.Show("Your appointment has been canceled.");
                this.Close();
            }
            else 
            {
                MessageBox.Show("You did not pick an appointment!");
            }
        }
    }
}
