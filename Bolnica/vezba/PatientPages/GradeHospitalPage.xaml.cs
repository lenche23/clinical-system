using System.Windows.Controls;
using vezba.ViewModel.PatientViewModel;

namespace vezba.PatientPages
{
    public partial class GradeHospitalPage : Page
    {
        public GradeHospitalPage(GradeHospitalViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
