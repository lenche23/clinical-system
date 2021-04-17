using Model;
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

namespace vezba
{

    public partial class ChangeAppointmentView : Window
    {

        public static ObservableCollection<Appointment> Appointments { get; set; }
        public ChangeAppointmentView()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage ps = new AppointmentStorage();
            List<Appointment> temp = ps.GetAll();
            Appointments = new ObservableCollection<Appointment>(temp);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedItems.Count > 0) {
                Appointment selectedAppointment = (Appointment)appointmentsTable.SelectedItem;
                var s = new EditAppointment(selectedAppointment);
                s.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali pregled koji želite da promenite!");
            }
    }
    }
}
