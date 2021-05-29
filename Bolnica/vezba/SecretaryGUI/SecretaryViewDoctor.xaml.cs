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
    /// Interaction logic for SecretaryViewDoctor.xaml
    /// </summary>
    public partial class SecretaryViewDoctor : Window
    {
        public ObservableCollection<WorkingHours> WorkingHours { get; set; }
        public ObservableCollection<VacationDays> VacationDays { get; set; }
        private Doctor Doctor { get; }
        public SecretaryViewDoctor(Doctor selectedDoctor)
        {
            InitializeComponent();
            Doctor = selectedDoctor;

            WorkingHours = new ObservableCollection<WorkingHours>();
            VacationDays = new ObservableCollection<VacationDays>();
            this.DataContext = this;

            NameSurname.Content = Doctor.Name + " " + Doctor.Surname;
            Speciality.Content = Doctor.SpecialityName;
            Jmbg.Content = Doctor.Jmbg;
            DateOfBirth.Content = Doctor.DateOfBirth.ToString("dd.MM.yyyy.");

            if (Doctor.Sex == Model.Sex.male)
                Sex.Content = "Muški";
            else
                Sex.Content = "Ženski";
            PhoneNumber.Content = Doctor.PhoneNumber;
            IdNumber.Content = Doctor.IdCard;
            Adress.Content = Doctor.Adress;
            Email.Content = Doctor.Email;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddWorkingHoursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveWorkingHoursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddVacationDaysButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveVacationDaysButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
