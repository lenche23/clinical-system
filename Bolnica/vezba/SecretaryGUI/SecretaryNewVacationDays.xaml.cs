using Model;
using Service;
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

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryNewVacationDays.xaml
    /// </summary>
    public partial class SecretaryNewVacationDays : Window
    {
        public Doctor Doctor { get; }
        public SecretaryNewVacationDays(Doctor doctor)
        {
            InitializeComponent();
            DoctorService doctorService = new DoctorService();
            Doctor = doctorService.GetDoctorByJmbg(doctor.Jmbg);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = new DateTime(1900, 1, 1);
            try
            {
                start = From.SelectedDate.Value.Date;
            }
            catch
            {
                MessageBox.Show("Niste izabrali datum pocetka!");
                return;
            }
            DateTime end = new DateTime(1900, 1, 1);
            try
            {
                end = To.SelectedDate.Value.Date;
            }
            catch
            {
                MessageBox.Show("Niste izabrali datum zavrsetka!");
                return;
            }
            if (start < DateTime.Now)
            {
                MessageBox.Show("Datum pocetka je vec prosao!");
                return;
            }
            if (end < start)
            {
                MessageBox.Show("Datum zavrsetka je pre datuma pocetka!");
                return;
            }


            VacationDays newVacationDays = new VacationDays(start, end);
            DoctorService doctorService = new DoctorService();
            Boolean isSuccess = doctorService.AddVacationDaysToDoctor(Doctor.Jmbg, newVacationDays);
            if (isSuccess)
            {
                SecretaryViewDoctor.VacationDays.Add(newVacationDays);
                this.Close();
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
