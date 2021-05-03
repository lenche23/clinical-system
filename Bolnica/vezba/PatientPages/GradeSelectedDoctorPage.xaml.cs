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

namespace vezba.PatientPages
{
    public partial class GradeSelectedDoctorPage : Page
    {
        public static Doctor Doctor { get; set; }
        public GradeSelectedDoctorPage(Doctor doctor)
        {
            InitializeComponent();
            this.DataContext = this;
            Doctor = doctor;
        }

        private void ButtonConfirmGradeDoctor_Click(object sender, RoutedEventArgs e)
        {
            int gradeDoctor = grade.SelectedIndex;
            switch (gradeDoctor)
            {
                /*case 0:

                case 1:

                case 2:

                case 3:

                case 4:*/

            }
                

        }
    }
}
