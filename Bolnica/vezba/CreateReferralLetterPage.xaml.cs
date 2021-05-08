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

namespace vezba
{
    /// <summary>
    /// Interaction logic for CreateReferralLetterPage.xaml
    /// </summary>
    public partial class CreateReferralLetterPage : Page
    {

        public DoctorStorage docstorage;
        public List<Doctor> Doctors { get; set; }

        public Patient patient;

        private DoctorView dw;

        public CreateReferralLetterPage(Patient patient, DoctorView dw)
        {
            InitializeComponent();
            this.DataContext = patient;
            docstorage = new DoctorStorage();
            Doctors = docstorage.GetAll();
            cmbDoctors.DataContext = this;
            cmbDoctors.SelectedIndex = 0;
            this.patient = patient;
            this.dw = dw;
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
            List<Appointment> appointments = aps.GetAll();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Patient.Jmbg.Equals(patient.Jmbg))
                {
                    appointment.Patient.MedicalRecord.AddReferralLetter(ReferralLtter);
                    aps.Update(appointment);
                }
            }
            foreach (Appointment appointment in Calendar.appointments)
            {
                if (appointment.Patient.Jmbg.Equals(patient.Jmbg))
                {
                    appointment.Patient.MedicalRecord.AddReferralLetter(ReferralLtter);
                }
            }
            dw.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }

        private void ValidateEntries()
        {

        }
    }
}
