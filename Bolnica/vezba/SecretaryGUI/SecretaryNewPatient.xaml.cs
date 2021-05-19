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
using vezba.Repository;

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryNewPatient.xaml
    /// </summary>
    public partial class SecretaryNewPatient : Window
    {
        public static ObservableCollection<Ingridient> Allergens { get; set; }
        public SecretaryNewPatient()
        {
            InitializeComponent();
            Allergens = new ObservableCollection<Ingridient>();
            this.DataContext = this;
            List<string> sexes = new List<string>();
            sexes.Add("Muško");
            sexes.Add("Žensko");
            Sex.ItemsSource = sexes;
        }

        private void NewAlergenButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewAllergen s = new SecretaryNewAllergen(0);
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
                MessageBox.Show("Nalog nije gostujući! Morate popuniti sva polja!");
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
            /*
            string text = Allergenss.Text.Trim();
            if (!text.Equals(""))
            {
                string[] alls = text.Split(',');
                for (int i = 0; i < alls.Length; i++)
                {
                    Ingridient a = new Ingridient(alls[i].Trim()); //?
                    medRecord.AddAllergen(a);
                    MessageBox.Show(Convert.ToString(i));
                }
            }
            */
            foreach(Ingridient allergen in Allergens)
            {
                medRecord.AddAllergen(allergen);
            }

            string name = Name.Text;
            string surname = Surname.Text;
            string jmbg = Jmbg.Text;
            /*DateTime date = new DateTime(1900, 1, 1);
            try
            {
                date = DateTime.ParseExact(DateOfBirth.Text, "dd.MM.yyyy.", null);
            }
            catch { }*/
            DateTime selectedDate = new DateTime(1900, 1, 1);
            try
            {
                selectedDate = DateOfBirth.SelectedDate.Value.Date;
            }
            catch {}

            Sex sex = Model.Sex.male;
            if(Sex.SelectedIndex == 1)
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
            ps.Save(pat);
            SecretaryPatients.Patients.Add(pat);
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
