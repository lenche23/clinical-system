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
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        public EditPatient(Patient p)
        {
            InitializeComponent();
            if (p.IsGuest)
            {
                IsGuest.IsChecked = true;
            }
            Name.Text = p.Name;
            Surname.Text = p.Surname;
            Jmbg.Text = p.Jmbg;
            Jmbg.IsReadOnly = true;
            DateOfBirth.Text = p.DateOfBirth.ToString("dd.MM.yyyy.");
            if (p.Sex == Sex.male)
            {
                MSex.IsChecked = true;
            }
            else
            {
                FSex.IsChecked = true;
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
                    for (int i = 0; i < p.MedicalRecord.Allergen.Count; i++)
                    {
                        if (i != 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append(p.MedicalRecord.Allergen[i].Name);
                    }
                    Allergens.Text = sb.ToString();
                }
                
            }
            
            Username.Text = p.Username;
            Password.Text = p.Password;
        }

        private void Save_Changes_Button_Click(object sender, RoutedEventArgs e)
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
            string[] alls = Allergens.Text.Split(',');
            for (int i = 0; i < alls.Length; i++)
            {
                Ingridient a = new Ingridient(alls[i].Trim()); //?
                medRecord.AddAllergen(a);
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
            ps.Update(pat);

            var pa = SecretaryView.Patients.FirstOrDefault(p => p.Jmbg.Equals(jmbg));
            if(pa != null)
            {
                SecretaryView.Patients[SecretaryView.Patients.IndexOf(pa)] = pat;
            }



            //List<Patient> pl = ps.GetAll();
            //ObservableCollection<Patient> t = new ObservableCollection<Patient>(ps.GetAll());
            //SecretaryView.Patients = t;
            //MessageBox.Show(SecretaryView.Patients[0].EmergencyContact);

            this.Close();
        }
    }
}
