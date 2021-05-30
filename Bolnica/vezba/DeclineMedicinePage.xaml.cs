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
using Model;
using vezba.Repository;

namespace vezba
{
    /// <summary>
    /// Interaction logic for DeclineMedicinePage.xaml
    /// </summary>
    public partial class DeclineMedicinePage : Page
    {
        public Medicine Medicine { get; set; }

        private DoctorView dw;

        public DeclinedMedicineFileRepository fileRepository;

        public MedicineFileRepository medFileRepository;

        private MedicinePageView mpw;
        public DeclineMedicinePage(Medicine medicine, DoctorView dw, MedicinePageView mpw)
        {
            InitializeComponent();
            Medicine = medicine;
            DataContext = Medicine;
            this.dw = dw;
            this.mpw = mpw;
            fileRepository = new DeclinedMedicineFileRepository();
            medFileRepository = new MedicineFileRepository();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            medFileRepository.Delete(Medicine.MedicineID);
            var id = fileRepository.GenerateNextId();
            var Description = DescriptionTB.Text;
            var declinedMedicine = new DeclinedMedicine(id, Medicine, Description);
            fileRepository.Save(declinedMedicine);
            MedicinePageView.MedicineToApprove.Remove(Medicine);
            MedicinePageView.DeclinedMedicine.Add(declinedMedicine);
            mpw.revisionGrid.Items.Refresh();
            mpw.declinedGrid.Items.Refresh();
            dw.Main.GoBack();
            dw.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
