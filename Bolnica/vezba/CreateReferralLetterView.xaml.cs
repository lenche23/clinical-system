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
    /// Interaction logic for CreateReferralLetterView.xaml
    /// </summary>
    public partial class CreateReferralLetterView : Window
    {
        public DoctorStorage docstorage;
        public List<Doctor> Doctors { get; set; }

        public Patient patient;

        public CreateReferralLetterView(Patient patient)
        {
            InitializeComponent();
            this.DataContext = patient;
            docstorage = new DoctorStorage();
            Doctors = docstorage.GetAll();
            cmbDoctors.DataContext = this;
            cmbDoctors.SelectedIndex = 0;
            this.patient = patient;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var StartDate = DpStartDate.SelectedDate;
            var DurationPeriodInDays = int.Parse(TbDuration.Text);
            var Doctor = cmbDoctors.SelectedItem;
            var ReferralLtter = new ReferralLetter((DateTime)StartDate, DurationPeriodInDays, (Doctor)Doctor);
            patient.MedicalRecord.AddReferralLetter(ReferralLtter);
            PatientStorage ps = new PatientStorage();
            ps.Update(patient);

            AppointmentStorage aps = new AppointmentStorage();
            ObservableCollection<Appointment> appointments = CalendarView.Appointments;
            foreach (Appointment appointment1 in appointments)
            {
                if (appointment1.Patient.Jmbg.Equals(patient.Jmbg))
                {
                    appointment1.Patient.MedicalRecord.AddReferralLetter(ReferralLtter);
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
