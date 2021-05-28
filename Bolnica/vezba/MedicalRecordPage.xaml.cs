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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba
{
    /// <summary>
    /// Interaction logic for MedicalRecord.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page
    {
        private DoctorView doctorView;

        public MedicalRecordPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = patient;
            if (patient.MedicalRecord != null)
            {
                this.doctorView = doctorView;
            }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.GoBack();
        }
    }
}

