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

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for ViewPatient.xaml
    /// </summary>
    public partial class ViewPatient : Window
    {
        public ViewPatient(Patient p)
        {
            InitializeComponent();
            if (p.IsGuest)
            {
                Patient.Content = "GOSTUJUCI PACIJENT";
            }
            NameSurname.Content = p.Name + " " + p.Surname;
            Jmbg.Content = Jmbg.Content + p.Jmbg;
            DateOfBirth.Content = DateOfBirth.Content + p.DateOfBirth.ToString("dd.MM.yyyy.");
            
            if (p.Sex == Model.Sex.male)
            {
                Sex.Content = "Pol:   Muski";
            }
            else
            {
                Sex.Content = "Pol:   Zenski";
            }
            PhoneNumber.Content = PhoneNumber.Content + p.PhoneNumber;
            IdNumber.Content = IdNumber.Content + p.IdCard;
            Adress.Content = Adress.Content + p.Adress;
            Email.Content = Email.Content + p.Email;
            EmergencyContact.Content = EmergencyContact.Content + p.EmergencyContact;
            if (p.MedicalRecord != null)
            {
                HealthEnsuranceNumber.Content = HealthEnsuranceNumber.Content + p.MedicalRecord.HealthInsuranceNumber;
                MedicalIdNumber.Content = MedicalIdNumber.Content + p.MedicalRecord.MedicalIdNumber;
                StringBuilder sb = new StringBuilder();
                if (p.MedicalRecord.Allergen != null)
                {
                    if(p.MedicalRecord.Allergen.Count != 0)
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
                    }
                    
                }

            }

            Username.Content = Username.Content + p.Username;
            Password.Content = Password.Content + p.Password;
        }
    }
}
