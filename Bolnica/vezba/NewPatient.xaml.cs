using Model;
using System;
using System.Collections.Generic;
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
using vezba;

namespace Bolnica
{
    public partial class NewPatient : Window
    {
        public NewPatient()
        {
            InitializeComponent();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
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
            string text = Allergens.Text.Trim();
            if (!text.Equals(""))
            {
                string[] alls = text.Split(',');
                for (int i = 0; i < alls.Length; i++) {
                    Allergen a = new Allergen(alls[i].Trim()); //?
                    medRecord.AddAllergen(a);
                    MessageBox.Show(Convert.ToString(i));
                }
            }

            string name = Name.Text;
            string surname = Surname.Text;
            string jmbg = Jmbg.Text;
            DateTime date = new DateTime(1900, 1, 1);
            try
            {
                date = DateTime.ParseExact(DateOfBirth.Text, "dd.MM.yyyy.", null);
            }
            catch { }
           
            Sex sex = Sex.male;
            if (Convert.ToBoolean(MSex.IsChecked)) 
            {
                sex = Sex.male;
            }
            else if (Convert.ToBoolean(FSex.IsChecked))
            {
                sex = Sex.female;
            }
            string phoneNumber = PhoneNumber.Text;
            string adress = Adress.Text;
            string email = Email.Text;
            string idNum = IdNumber.Text;
            string emContact = EmergencyContact.Text;
            string username = Username.Text;
            string password = Password.Text;

            Patient pat = new Patient(isGuest, name, surname, jmbg, date, sex, phoneNumber, adress, email, idNum, emContact, medRecord, username, password);

            PatientStorage ps = new PatientStorage();
            ps.Save(pat);
            SecretaryView.Patients.Add(pat);
            this.Close();

            return;
        }
    }
}
