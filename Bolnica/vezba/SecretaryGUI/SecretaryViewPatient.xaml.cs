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
    
    public partial class SecretaryViewPatient : Window
    {
        public ObservableCollection<Ingridient> Allergens { get; set; }
        public SecretaryViewPatient(Patient patient)
        {
            InitializeComponent();
            Allergens = new ObservableCollection<Ingridient>();
            this.DataContext = this;

            
            NameSurname.Content = patient.Name + " " + patient.Surname;
            if (patient.IsGuest)
            {
                NameSurname.Content += " (gost)";
            }
            Jmbg.Content = patient.Jmbg;
            DateOfBirth.Content = patient.DateOfBirth.ToString("dd.MM.yyyy.");

            if (patient.Sex == Model.Sex.male)
            {
                Sex.Content = "Muški";
            }
            else
            {
                Sex.Content = "Ženski";
            }
            PhoneNumber.Content = patient.PhoneNumber;
            IdNumber.Content = patient.IdCard;
            Adress.Content =  patient.Adress;
            Email.Content =  patient.Email;
            EmergencyContact.Content =  patient.EmergencyContact;
            if (patient.MedicalRecord != null)
            {
                HealthEnsuranceNumber.Content = patient.MedicalRecord.HealthInsuranceNumber;
                MedicalIdNumber.Content = patient.MedicalRecord.MedicalIdNumber;
                if (patient.MedicalRecord.Allergen != null)
                {
                    foreach(Ingridient allergen in patient.MedicalRecord.Allergen)
                    {
                        Allergens.Add(allergen);
                    }

                }

            }

            Username.Content = patient.Username;
            Password.Content = patient.Password;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }

    
}
