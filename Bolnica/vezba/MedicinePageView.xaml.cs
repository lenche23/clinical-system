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

            List<Medicine> approvedMedicine = ms.GetApproved();
            ApprovedMedicine = new ObservableCollection<Medicine>(approvedMedicine);

            DeclinedMedicineStorage dms = new DeclinedMedicineStorage();
            List<DeclinedMedicine> declinedMedicine = dms.GetAll();
            DeclinedMedicine = new ObservableCollection<DeclinedMedicine>(declinedMedicine);

            DataContext = this;
            this.dw = dw;
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (approvedGrid.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)approvedGrid.SelectedItem;
                dw.Main.Content = new ViewMedicinePage(medicine, dw, this);
            }
        }

        private void RevisionClick(object sender, RoutedEventArgs e)
        {
            if (revisionGrid.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)revisionGrid.SelectedItem;
                dw.Main.Content = new MedicineRevisionPage(medicine, dw, this);
            }
        }

        private void ViewDeclinedClick(object sender, RoutedEventArgs e)
        {
            if(declinedGrid.SelectedItems.Count > 0)
            {
                DeclinedMedicine declinedMedicine = (DeclinedMedicine)declinedGrid.SelectedItem;
                dw.Main.Content = new ViewDeclinedMedicine(declinedMedicine, dw);
            }
        }
    }
}
