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
    /// Interaction logic for SecretaryNewWorkingHours.xaml
    /// </summary>
    public partial class SecretaryNewWorkingHours : Window
    {
        public Doctor Doctor { get; }
        public WorkingHours WorkingHours { get; set; }
        public SecretaryNewWorkingHours(Doctor doctor)
        {
            InitializeComponent();
            DoctorService doctorService = new DoctorService();
            Doctor = doctorService.GetDoctorByJmbg(doctor.Jmbg);
            DateTime beginingDate = doctorService.FindNextWorkingHoursBeginningDateForDoctor(doctor.Jmbg);
            From.Content = beginingDate.ToString("dd.MM.yyyy.");
            To.Content = beginingDate.AddDays(7).ToString("dd.MM.yyyy.");
            List<string> shifts = new List<string> { "Prva: 07:00 - 14:00", "Druga: 14:00 - 21:00" };
            Shift.ItemsSource = shifts;

            WorkingHours = new WorkingHours(beginingDate, Model.Shift.firstShift);
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        { 
            DoctorService doctorService = new DoctorService();
            if (Shift.SelectedIndex == 1)
                WorkingHours.Shift = Model.Shift.secondShift;
            doctorService.AddWorkingHoursToDoctor(Doctor.Jmbg, WorkingHours);
            SecretaryViewDoctor.WorkingHours.Add(WorkingHours);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
