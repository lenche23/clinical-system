using Model;
using Service;
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

namespace vezba.SecretaryGUI
{
    public partial class SecretaryPatients : Page
    {
        public static ObservableCollection<Patient> Patients { get; set; }
        public SecretaryPatients()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientService ps = new PatientService();
            Patients = new ObservableCollection<Patient>(ps.GetAllPatients());
        }

        private void NewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewPatient w = new SecretaryNewPatient();
            w.Show();
        }

        private void ViewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (patientsTable.SelectedCells.Count > 0)
            {
                Patient selectedPatient = (Patient)patientsTable.SelectedItem;
                SecretaryViewPatient w = new SecretaryViewPatient(selectedPatient);
                w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali pacijenta!");
            }
        }

        private void EditPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (patientsTable.SelectedCells.Count > 0)
            {
                Patient selectedPatient = (Patient)patientsTable.SelectedItem;
                SecretaryEditPatient w = new SecretaryEditPatient(selectedPatient);
                w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali pacijenta!");
            }
        }

        private void DeletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (patientsTable.SelectedCells.Count > 0)
            {
                Patient selectedPatient = (Patient)patientsTable.SelectedItem;
                PatientService ps = new PatientService();
                ps.DeletePatient(selectedPatient.Jmbg);
                Patients.Remove(selectedPatient);
            }
            else
            {
                MessageBox.Show("Niste selektovali pacijenta!");
            }
        }
    }
}
