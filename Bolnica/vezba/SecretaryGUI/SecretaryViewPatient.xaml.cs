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
        public SecretaryViewPatient(Patient p)
        {
            InitializeComponent();
            Allergens = new ObservableCollection<Ingridient>();
            this.DataContext = this;

            
            NameSurname.Content = p.Name + " " + p.Surname;
            if (p.IsGuest)
            {
                NameSurname.Content += " (gost)";
            }
            Jmbg.Content = p.Jmbg;
            DateOfBirth.Content = p.DateOfBirth.ToString("dd.MM.yyyy.");

            if (p.Sex == Model.Sex.male)
            {
                Sex.Content = "Muški";
            }
            else
            {
                Sex.Content = "Ženski";
            }
            PhoneNumber.Content = p.PhoneNumber;
            IdNumber.Content = p.IdCard;
            Adress.Content =  p.Adress;
            Email.Content =  p.Email;
            EmergencyContact.Content =  p.EmergencyContact;
            if (p.MedicalRecord != null)
            {
                HealthEnsuranceNumber.Content = p.MedicalRecord.HealthInsuranceNumber;
                MedicalIdNumber.Content = p.MedicalRecord.MedicalIdNumber;
                //StringBuilder sb = new StringBuilder();
                if (p.MedicalRecord.Allergen != null)
                {
                    /*if (p.MedicalRecord.Allergen.Count != 0)
                    {
                        for (int i = 0; i < p.MedicalRecord.Allergen.Count; i++)
                        {
                            if (i != 0)
                            {
                                sb.Append(", ");
                            }
                            sb.Append(p.MedicalRecord.Allergen[i].Name);
                        }
                        Allergens.Content = Allergens.Content + sb.ToString();
                    }
                    else
                    {
                        Allergens.Content = Allergens.Content + "Nema alergije";
                    }*/
                    foreach(Ingridient allergen in p.MedicalRecord.Allergen)
                    {
                        Allergens.Add(allergen);
                    }

                }

            }

            Username.Content = p.Username;
            Password.Content = p.Password;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }

    
}
