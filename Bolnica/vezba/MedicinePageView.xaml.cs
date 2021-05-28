using Model;
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
using Model;
using Service;
using vezba.Repository;

namespace vezba
{
    /// <summary>
    /// Interaction logic for MedicinePageView.xaml
    /// </summary>
    public partial class MedicinePageView : Page
    {
        public static ObservableCollection<Medicine> MedicineToApprove { get; set; }

        public static ObservableCollection<Medicine> ApprovedMedicine { get; set; }

        public static ObservableCollection<DeclinedMedicine> DeclinedMedicine { get; set; }

        private readonly DoctorView _doctorView;

        public MedicinePageView(DoctorView doctorView)
        {
            InitializeComponent();

            MedicineService medicineService = new MedicineService();
            List<Medicine> medicineToApprove = medicineService.GetAwaiting();
            MedicineToApprove = new ObservableCollection<Medicine>(medicineToApprove);

            List<Medicine> approvedMedicine = medicineService.GetApproved();
            ApprovedMedicine = new ObservableCollection<Medicine>(approvedMedicine);

            DeclinedMedicineService declinedMedicineService = new DeclinedMedicineService();
            List<DeclinedMedicine> declinedMedicine = declinedMedicineService.GetAll();
            DeclinedMedicine = new ObservableCollection<DeclinedMedicine>(declinedMedicine);

            DataContext = this;
            _doctorView = doctorView;
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (approvedGrid.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)approvedGrid.SelectedItem;
                _doctorView.Main.Content = new ViewMedicinePage(medicine, _doctorView, this);
            }
        }

        private void RevisionClick(object sender, RoutedEventArgs e)
        {
            if (revisionGrid.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)revisionGrid.SelectedItem;
                _doctorView.Main.Content = new MedicineRevisionPage(medicine, _doctorView, this);
            }
        }

        private void ViewDeclinedClick(object sender, RoutedEventArgs e)
        {
            if(declinedGrid.SelectedItems.Count > 0)
            {
                DeclinedMedicine declinedMedicine = (DeclinedMedicine)declinedGrid.SelectedItem;
                _doctorView.Main.Content = new ViewDeclinedMedicine(declinedMedicine, _doctorView);
            }
        }
    }
}
