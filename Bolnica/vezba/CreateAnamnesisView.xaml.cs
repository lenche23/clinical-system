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
    /// Interaction logic for CreateAnamnesisView.xaml
    /// </summary>
    public partial class CreateAnamnesisView : Window
    {
        public Appointment appointment;

        public CreateAnamnesisView(Appointment appointment)
        { 
            InitializeComponent();
            this.DataContext = appointment;
            this.appointment = appointment;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var Comment = TbComment.Text;
            var Patient = PatientNameSurname.Text;
            var Doctor = DoctorNameSurname.Text;
            var Time = DateTime.Now;
            var Anamnesis = new Anamnesis { Comment = Comment, Patient = Patient, Doctor = Doctor, Time = Time };
            appointment.Patient.MedicalRecord.AddAnamnesis(Anamnesis);
            PatientStorage ps = new PatientStorage();
            ps.Update(appointment.Patient);
            AppointmentStorage aps = new AppointmentStorage();
            ObservableCollection<Appointment> appointments = DoctorView.Appointments;
            foreach (Appointment appointment1 in appointments) { 
                if(appointment1.Patient.Jmbg.Equals(appointment.Patient.Jmbg))
                {
                    appointment1.Patient.MedicalRecord.AddAnamnesis(Anamnesis);
                    aps.Update(appointment1);
                }
            }
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
