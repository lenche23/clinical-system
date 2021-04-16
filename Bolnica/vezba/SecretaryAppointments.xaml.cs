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
    public partial class SecretaryAppointments : Window
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }
        public SecretaryAppointments()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage s = new AppointmentStorage();
            List<Appointment> temp = s.GetAll();
            Appointments = new ObservableCollection<Appointment>(temp);
        }

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new NewAppointment();
            s.Show();
        }
        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewAppointment();
            s.Show();
        }
        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new EditAppointment();
            s.Show();
        }
        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
