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

namespace vezba
{
    /// <summary>
    /// Interaction logic for MedicinePageView.xaml
    /// </summary>
    public partial class MedicinePageView : Page
    {
        public static ObservableCollection<Medicine> MedicineToApprove { get; set; }

        public static ObservableCollection<Medicine> ApprovedMedicine { get; set; }

        public MedicinePageView()
        {
            InitializeComponent();
            MedicineStorage ms = new MedicineStorage();
            List<Medicine> medicineToApprove = ms.GetAwaiting();
            MedicineToApprove = new ObservableCollection<Medicine>(medicineToApprove);
            listViewMedicineRevision.ItemsSource = MedicineToApprove;

            List<Medicine> approvedMedicine = ms.GetApproved();
            ApprovedMedicine = new ObservableCollection<Medicine>(approvedMedicine);
            listViewMedicine.ItemsSource = ApprovedMedicine;
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (listViewMedicine.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)listViewMedicine.SelectedItem;
                //CHANGE
                //var s = new MedicineView(medicine, this);
                //s.Show();
            }
        }

        private void RevisionClick(object sender, RoutedEventArgs e)
        {
            if (listViewMedicineRevision.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)listViewMedicineRevision.SelectedItem;
                var s = new RevisionView(medicine);
                s.Show();
            }
        }
    }
}
