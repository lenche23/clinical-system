﻿using Model;
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
using System.Windows.Shapes;

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryEditPatient.xaml
    /// </summary>
    public partial class SecretaryEditPatient : Window
    {
        public static ObservableCollection<Ingridient> Allergens { get; set; }
        public SecretaryEditPatient(Patient selectedPatient)
        {
            InitializeComponent();
            Allergens = new ObservableCollection<Ingridient>();
            this.DataContext = this;
            List<string> sexes = new List<string>();
            sexes.Add("Muško");
            sexes.Add("Žensko");
            Sex.ItemsSource = sexes;

            if (selectedPatient.IsGuest)
            {
                IsGuest.IsChecked = true;
            }
            Name.Text = selectedPatient.Name;
            Surname.Text = selectedPatient.Surname;
            Jmbg.Text = selectedPatient.Jmbg;
            Jmbg.IsReadOnly = true;
            DateOfBirth.SelectedDate = selectedPatient.DateOfBirth;
            if (selectedPatient.Sex == Model.Sex.male)
            {
                Sex.SelectedIndex = 0;
            }
            else
            {
                Sex.SelectedIndex = 1;
            }
            PhoneNumber.Text = selectedPatient.PhoneNumber;
            IdNumber.Text = selectedPatient.IdCard;
            Adress.Text = selectedPatient.Adress;
            Email.Text = selectedPatient.Email;
            EmergencyContact.Text = selectedPatient.EmergencyContact;
            if (selectedPatient.MedicalRecord != null)
            {
                HealthEnsuranceNumber.Text = selectedPatient.MedicalRecord.HealthInsuranceNumber;
                MedicalIdNumber.Text = selectedPatient.MedicalRecord.MedicalIdNumber;
                
                if (selectedPatient.MedicalRecord.Allergen != null && selectedPatient.MedicalRecord.Allergen.Count != 0)
                {
                    foreach(Ingridient allergen in selectedPatient.MedicalRecord.Allergen)
                    {
                        Allergens.Add(allergen);
                    }
                }
            }

            Username.Text = selectedPatient.Username;
            Password.Text = selectedPatient.Password;
        }

        private void NewAlergenButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewAllergen s = new SecretaryNewAllergen(1);
            s.Show();
        }

        private void DeleteAllergenButton_Click(object sender, RoutedEventArgs e)
        {
            if (allergensTable.SelectedCells.Count > 0)
            {
                Ingridient a = (Ingridient)allergensTable.SelectedItem;
                Allergens.Remove(a);
            }
            else
            {
                MessageBox.Show("Niste selektovali alergen!");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Boolean isGuest = Convert.ToBoolean(IsGuest.IsChecked);
            if (isGuest == false && (((Name.Text).Trim().Equals("")) || ((Surname.Text).Trim().Equals("")) || ((Jmbg.Text).Trim().Equals("")) || ((PhoneNumber.Text).Trim().Equals("")) || ((Adress.Text).Trim().Equals("")) || ((Email.Text).Trim().Equals("")) || ((Username.Text).Trim().Equals("")) || ((Password.Text).Trim().Equals("")) || ((IdNumber.Text).Trim().Equals(""))))
            {
                MessageBox.Show("Nalog nije gostujuci! Morate popuniti sva polja!");
                return;
            }
            else if (isGuest == true && ((Jmbg.Text).Trim().Equals("")))
            {
                MessageBox.Show("JMBG pacijenta nije unet!");
                return;
            }

            string mid = MedicalIdNumber.Text;
            string hin = HealthEnsuranceNumber.Text;
            MedicalRecord medRecord = new MedicalRecord(hin, mid);
            foreach (Ingridient allergen in Allergens)
            {
                medRecord.AddAllergen(allergen);
            }

            string name = Name.Text;
            string surname = Surname.Text;
            string jmbg = Jmbg.Text;
            DateTime selectedDate = new DateTime(1900, 1, 1);
            try
            {
                selectedDate = DateOfBirth.SelectedDate.Value.Date;
            }
            catch { }

            Sex sex = Model.Sex.male;
            if (Sex.SelectedIndex == 1)
            {
                sex = Model.Sex.female;
            }

            string phoneNumber = PhoneNumber.Text;
            string adress = Adress.Text;
            string email = Email.Text;
            string idNum = IdNumber.Text;
            string emContact = EmergencyContact.Text;
            string username = Username.Text;
            string password = Password.Text;

            Patient editedPatient = new Patient(isGuest, name, surname, jmbg, selectedDate, sex, phoneNumber, adress, email, idNum, emContact, medRecord, username, password);
            PatientService ps = new PatientService();
            ps.EditPatient(editedPatient);

            var previousPatient = SecretaryPatients.Patients.FirstOrDefault(p => p.Jmbg.Equals(jmbg));
            if (previousPatient != null)
            {
                SecretaryPatients.Patients[SecretaryPatients.Patients.IndexOf(previousPatient)] = editedPatient;
            }

            this.Close();
            return;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
