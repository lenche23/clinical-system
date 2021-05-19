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

namespace vezba
{
    /// <summary>
    /// Interaction logic for MedicineRevisionPage.xaml
    /// </summary>
    public partial class MedicineRevisionPage : Page
    {

        public Medicine Medicine { get; set; }

        private DoctorView dw;

        public MedicineFileRepository medFileRepository;

        private MedicinePageView mpw;

        public MedicineRevisionPage(Medicine medicine, DoctorView dw, MedicinePageView mpw)
        {
            InitializeComponent();
            Medicine = medicine;
            DataContext = Medicine;
            this.dw = dw;
            this.mpw = mpw;
        }

        private void ApproveClick(object sender, RoutedEventArgs e)
        {
            Medicine.Status = MedicineStatus.approved;
            medFileRepository = new MedicineFileRepository();
            medFileRepository.Update(Medicine);
            MedicinePageView.MedicineToApprove.Remove(Medicine);
            MedicinePageView.ApprovedMedicine.Add(Medicine);
            mpw.revisionGrid.Items.Refresh();
            mpw.approvedGrid.Items.Refresh();
            dw.Main.GoBack();
        }

        private void DeclineClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new DeclineMedicinePage(Medicine, dw, mpw);
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
