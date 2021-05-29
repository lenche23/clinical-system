using Model;
using Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba.SecretaryGUI
{
    public partial class SecretaryDoctors : Page
    {
        public ObservableCollection<Doctor> Doctors { get; set; }
        public SecretaryDoctors()
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorService doctorService = new DoctorService();
            Doctors = new ObservableCollection<Doctor>(doctorService.GetAllDoctors());
        }

        private void ViewDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (doctorsTable.SelectedCells.Count > 0)
            {
                Doctor selectedDoctor = (Doctor)doctorsTable.SelectedItem;
                SecretaryViewDoctor w = new SecretaryViewDoctor(selectedDoctor);
                w.Show();
            }
            else
                MessageBox.Show("Niste selektovali lekara!");
        }
    }
}
