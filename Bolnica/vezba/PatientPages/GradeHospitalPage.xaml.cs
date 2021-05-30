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
using System.Windows.Navigation;
using System.Windows.Shapes;
using vezba.Repository;

namespace vezba.PatientPages
{
    public partial class GradeHospitalPage : Page
    {
        private HospitalEvaluationService HospitalEvaluationService { get; set; }
        public GradeHospitalPage()
        {
            InitializeComponent();
            HospitalEvaluationService = new HospitalEvaluationService();
        }

        private void ButtonConfirmGradeHospital_Click(object sender, RoutedEventArgs e)
        {
            HospitalEvaluation hospitalEvaluation = AddEvaluation();
            Boolean saved = HospitalEvaluationService.SaveEvaluation(hospitalEvaluation);
            if (saved)
            {
                var s = new SuccessfulGradeHospital();
                s.Show();
            }
        }

        private HospitalEvaluation AddEvaluation()
        {
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
            int id = HospitalEvaluationService.EvaluationGenerateNextId();
            HospitalEvaluation hospitalEvaluation = new HospitalEvaluation(rating, comm, id, false);
            return hospitalEvaluation;
        }
    }
}
