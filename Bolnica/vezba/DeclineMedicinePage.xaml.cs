using Model;
using System.Windows;
using System.Windows.Controls;
using Service;
using vezba.Repository;

namespace vezba
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
            MedicineService medicineService = new MedicineService();
            medicineService.Delete(Medicine.MedicineID);

            var description = DescriptionTB.Text;
            var declinedMedicine = new DeclinedMedicine(0, Medicine, description);

            DeclinedMedicineService declinedMedicineService = new DeclinedMedicineService();
            declinedMedicineService.Save(declinedMedicine);

            UpdateMedicineView(declinedMedicine);
            _doctorView.Main.GoBack();
            _doctorView.Main.GoBack();
        }

        private void UpdateMedicineView(DeclinedMedicine declinedMedicine)
        {
            MedicinePageView.MedicineToApprove.Remove(Medicine);
            MedicinePageView.DeclinedMedicine.Add(declinedMedicine);
            _medicinePageView.revisionGrid.Items.Refresh();
            _medicinePageView.declinedGrid.Items.Refresh();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
