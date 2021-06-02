using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for DeclineMedicinePage.xaml
    /// </summary>
    public partial class DeclineMedicinePage : Page
    {
        public Medicine Medicine { get; set; }

        private readonly DoctorView _doctorView;

        private readonly MedicinePageView _medicinePageView;
        public DeclineMedicinePage(Medicine medicine, DoctorView doctorView, MedicinePageView medicinePageView)
        {
            InitializeComponent();
            Medicine = medicine;
            DataContext = Medicine;
            _doctorView = doctorView;
            _medicinePageView = medicinePageView;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var medicineService = new MedicineService();

            var description = DescriptionTB.Text;
            var declinedMedicine = medicineService.DeclineMedicine(Medicine, description);

            UpdateMedicineView(declinedMedicine);
            _doctorView.Main.GoBack();
            _doctorView.Main.GoBack();
        }

        private void UpdateMedicineView(DeclinedMedicine declinedMedicine)
        {
            MedicinePageView.MedicineToApprove.Remove(Medicine);
            MedicinePageView.DeclinedMedicine.Add(declinedMedicine);
            _medicinePageView.RevisionListView.Items.Refresh();
            _medicinePageView.declinedGrid.Items.Refresh();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
