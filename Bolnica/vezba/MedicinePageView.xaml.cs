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
using vezba.Model;

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

        private DoctorView dw;

        public MedicinePageView(DoctorView dw)
        {
            InitializeComponent();
            MedicineStorage ms = new MedicineStorage();
            List<Medicine> medicineToApprove = ms.GetAwaiting();
            MedicineToApprove = new ObservableCollection<Medicine>(medicineToApprove);
            listViewMedicineRevision.ItemsSource = MedicineToApprove;

            List<Medicine> approvedMedicine = ms.GetApproved();
            ApprovedMedicine = new ObservableCollection<Medicine>(approvedMedicine);
            listViewMedicine.ItemsSource = ApprovedMedicine;

            DeclinedMedicineStorage dms = new DeclinedMedicineStorage();
            List<DeclinedMedicine> declinedMedicine = dms.GetAll();
            DeclinedMedicine = new ObservableCollection<DeclinedMedicine>(declinedMedicine);
            listViewDeclinedMedicine.ItemsSource = DeclinedMedicine;

            this.dw = dw;
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (listViewMedicine.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)listViewMedicine.SelectedItem;
                dw.Main.Content = new ViewMedicinePage(medicine,dw);
            }
        }

        private void RevisionClick(object sender, RoutedEventArgs e)
        {
            if (listViewMedicineRevision.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)listViewMedicineRevision.SelectedItem;
                dw.Main.Content = new MedicineRevisionPage(medicine, dw);
            }
        }

        private void ViewDeclinedClick(object sender, RoutedEventArgs e)
        {
            if(listViewDeclinedMedicine.SelectedItems.Count > 0)
            {
                DeclinedMedicine declinedMedicine = (DeclinedMedicine)listViewDeclinedMedicine.SelectedItem;
                dw.Main.Content = new ViewDeclinedMedicine(declinedMedicine, dw);
            }
        }
    }
}
