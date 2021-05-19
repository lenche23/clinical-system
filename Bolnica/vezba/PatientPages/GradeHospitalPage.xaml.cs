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
    public partial class GradeHospitalPage : Page
    {
        public GradeHospitalPage()
        {
            InitializeComponent();
        }

        private void ButtonConfirmGradeHospital_Click(object sender, RoutedEventArgs e)
        {
            HospitalEvaluationFileRepository fileRepository = new HospitalEvaluationFileRepository();
            int gradeHospital = grade.SelectedIndex;
            int rating = 0;
            switch (gradeHospital)
            {
                case 0:
                    rating = 1;
                    break;
                case 1:
                    rating = 2;
                    break;
                case 2:
                    rating = 3;
                    break;
                case 3:
                    rating = 4;
                    break;
                case 4:
                    rating = 5;
                    break;
                default:
                    rating = 1;
                    break;
            }
            String comm = comment.Text;
            int id = fileRepository.GenerateNextId();
            Boolean deleted = false;
            HospitalEvaluation hospitalEvaluation = new HospitalEvaluation(rating, comm, id, deleted);
            fileRepository.Save(hospitalEvaluation);
            var s = new SuccessfulGradeHospital();
            s.Show();
        }
    }
}
