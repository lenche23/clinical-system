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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba.PatientPages
{
    public partial class GradeDoctorPage : Page
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }

        public GradeDoctorPage()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage ps = new AppointmentStorage();
            List<Appointment> temp = new List<Appointment>();
            foreach (Appointment appointment in ps.GetAll())
            {
                if (appointment.StartTime.Date < DateTime.Now && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    temp.Add(appointment);

                }
            }
            Appointments = new ObservableCollection<Appointment>(temp);
        }

        private void ButtonGradeDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (gradingTable.SelectedItems.Count > 0)
            {
                Appointment a = (Appointment)gradingTable.SelectedItem;
                Doctor selectedDoctor = a.Doctor;
                this.NavigationService.Navigate(new GradeSelectedDoctorPage(selectedDoctor));
            }
            else
            {
                var s = new TableNote();
                s.ShowDialog();
            }
            
        }
    }
}
