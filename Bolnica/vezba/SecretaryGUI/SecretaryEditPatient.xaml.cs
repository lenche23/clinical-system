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

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryEditPatient.xaml
    /// </summary>
    public partial class SecretaryEditPatient : Window
    {
        public static ObservableCollection<Ingridient> Allergens { get; set; }
        public SecretaryEditPatient(Patient p)
        {
            InitializeComponent();
            Allergens = new ObservableCollection<Ingridient>();
            this.DataContext = this;
            List<string> sexes = new List<string>();
            sexes.Add("Muško");
            sexes.Add("Žensko");
            Sex.ItemsSource = sexes;

            if (p.IsGuest)
            {
                IsGuest.IsChecked = true;
            }
            Name.Text = p.Name;
            Surname.Text = p.Surname;
            Jmbg.Text = p.Jmbg;
            Jmbg.IsReadOnly = true;
            DateOfBirth.SelectedDate = p.DateOfBirth;
            if (p.Sex == Model.Sex.male)
            {
                Sex.SelectedIndex = 0;
            }
            else
            {
                Sex.SelectedIndex = 1;
            }
            PhoneNumber.Text = p.PhoneNumber;
            IdNumber.Text = p.IdCard;
            Adress.Text = p.Adress;
            Email.Text = p.Email;
            EmergencyContact.Text = p.EmergencyContact;
            if (p.MedicalRecord != null)
            {
                HealthEnsuranceNumber.Text = p.MedicalRecord.HealthInsuranceNumber;
                MedicalIdNumber.Text = p.MedicalRecord.MedicalIdNumber;
                
                StringBuilder sb = new StringBuilder();
                if (p.MedicalRecord.Allergen != null && p.MedicalRecord.Allergen.Count != 0)
                {
                    foreach(Ingridient allergen in p.MedicalRecord.Allergen)
                    {
                        Allergens.Add(allergen);
                    }
                }
            }

            Username.Text = p.Username;
            Password.Text = p.Password;
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

            Patient pat = new Patient(isGuest, name, surname, jmbg, selectedDate, sex, phoneNumber, adress, email, idNum, emContact, medRecord, username, password);
            PatientFileRepository ps = new PatientFileRepository();
            ps.Update(pat);

            var pa = SecretaryPatients.Patients.FirstOrDefault(p => p.Jmbg.Equals(jmbg));
            if (pa != null)
            {
                SecretaryPatients.Patients[SecretaryPatients.Patients.IndexOf(pa)] = pat;
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
