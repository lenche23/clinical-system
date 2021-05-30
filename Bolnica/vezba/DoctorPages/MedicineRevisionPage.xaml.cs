using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for MedicineRevisionPage.xaml
    /// </summary>
    public partial class MedicineRevisionPage : Page
    {

        public Medicine Medicine { get; set; }

        private readonly DoctorView _doctorView;

        private readonly MedicinePageView _medicinePageView;

        public MedicineRevisionPage(Medicine medicine, DoctorView doctorView, MedicinePageView medicinePageView)
        {
            InitializeComponent();
            Medicine = medicine;
            DataContext = Medicine;
            _doctorView = doctorView;
            _medicinePageView = medicinePageView;
        }

        private void ApproveClick(object sender, RoutedEventArgs e)
        {
            var medicineService = new MedicineService();
            medicineService.ApproveMedicine(Medicine);
            UpdateMedicineView();
            _doctorView.Main.GoBack();
        }

        private void UpdateMedicineView()
        {
            MedicinePageView.MedicineToApprove.Remove(Medicine);
            MedicinePageView.ApprovedMedicine.Add(Medicine);
            _medicinePageView.revisionGrid.Items.Refresh();
            _medicinePageView.approvedGrid.Items.Refresh();
        }

        private void DeclineClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new DeclineMedicinePage(Medicine, _doctorView, _medicinePageView);
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
