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
    /// <summary>
    /// Interaction logic for CancelAppointmentView.xaml
    /// </summary>
    public partial class CancelAppointmentView : Window
    {
        public CancelAppointmentView()
        {
            InitializeComponent();
            lvUsers2.ItemsSource = PatientView.Apps;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvUsers2.SelectedItems.Count > 0)
            {
                Appointment a = (Appointment)lvUsers2.SelectedItem;
                AppointmentStorage storage = new AppointmentStorage();
                Boolean bla =  storage.Delete(a.AppointentId);
                PatientView.Apps.Remove(a);
                MessageBox.Show("Your appopintment has been canceled.");
                this.Close();
            }
            else 
            {
                MessageBox.Show("You did not pick an appointment!");
            }
        }
    }
}
