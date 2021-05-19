using Bolnica;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using vezba.Repository;

namespace vezba
{
    public partial class SecretaryView : Window
    {
        public static ObservableCollection<Patient> Patients { get; set; }
        public SecretaryView()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientFileRepository ps = new PatientFileRepository();
            List<Patient> temp = ps.GetAll();
            Patients = new ObservableCollection<Patient>(temp);

        }

        private void Regestration_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new NewPatient();
            s.Show();
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            if (patientsTable.SelectedCells.Count > 0)
            {
                /*PatientFileRepository ps = new PatientFileRepository();
                List<Patient> temp = ps.GetAll();
                Patients = new ObservableCollection<Patient>(temp);*/
                Patient p = (Patient)patientsTable.SelectedItem;
                var s = new ViewPatient(p);
                s.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali pacijenta!");
            }
            
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if(patientsTable.SelectedCells.Count > 0)
            {
                /*PatientFileRepository ps = new PatientFileRepository();
                List<Patient> temp = ps.GetAll();
                Patients = new ObservableCollection<Patient>(temp);*/
                Patient p = (Patient) patientsTable.SelectedItem;
                var s = new EditPatient(p);
                s.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali pacijenta!");
            }
            
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
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

        private void Announcements_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new Announcements();
            s.Show();
        }

        private void Appointments_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new SecretaryAppointments();
            s.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewMyAnnouncements(UserType.secretary);
            s.Show();
        }
    }
}
