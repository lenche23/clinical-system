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
using vezba.Repository;

namespace vezba.SecretaryGUI
{
    public partial class SecretaryPatients : Page
    {
        public static ObservableCollection<Patient> Patients { get; set; }
        public SecretaryPatients()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientFileRepository ps = new PatientFileRepository();
            List<Patient> temp = ps.GetAll();
            Patients = new ObservableCollection<Patient>(temp);
        }

        private void NewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewPatient s = new SecretaryNewPatient();
            s.Show();
        }

        private void ViewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (patientsTable.SelectedCells.Count > 0)
            {
                Patient p = (Patient)patientsTable.SelectedItem;
                //var s = new ViewPatient(p);
                SecretaryViewPatient s = new SecretaryViewPatient(p);
                s.Show();
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
                Patient p = (Patient)patientsTable.SelectedItem;
                SecretaryEditPatient s = new SecretaryEditPatient(p);
                //var s = new EditPatient(p);
                s.Show();
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
                Patient p = (Patient)patientsTable.SelectedItem;
                PatientFileRepository ps = new PatientFileRepository();
                ps.Delete(p.Jmbg);
                Patients.Remove(p);
            }
            else
            {
                MessageBox.Show("Niste selektovali pacijenta!");
            }
        }
    }
}
