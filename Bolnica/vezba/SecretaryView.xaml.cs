using Bolnica;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace vezba
{
    public partial class SecretaryView : Window
    {
        public static ObservableCollection<Patient> Patients { get; set; }
        public SecretaryView()
        {
            InitializeComponent();
            this.DataContext = this;
            //Patients = new ObservableCollection<Patient>();
            PatientStorage ps = new PatientStorage();
            List<Patient> temp = ps.GetAll();
            Patients = new ObservableCollection<Patient>(temp);
            MessageBox.Show(Convert.ToString(Patients[0].Name + " " + Patients[1].Name));

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
                //izbaci ga
                Patient p = (Patient)patientsTable.SelectedItem;
                PatientStorage ps = new PatientStorage();
                ps.Delete(p.Jmbg);
                Patients.Remove(p);

            }
            else
            {
                MessageBox.Show("Niste selektovali pacijenta!");
            }
            //DialogResult res = MessageBox.Show("Da li ste sigurni da zelite da izbrisete pacijenta?", "Brisanje pacijenta");
        }
    }
}
