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
using vezba.Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for DeclineMedicinePage.xaml
    /// </summary>
    public partial class DeclineMedicinePage : Page
    {
        public Medicine Medicine { get; set; }

        private DoctorView dw;

        public DeclinedMedicineStorage Storage;

        public MedicineStorage MedStorage;

        public DeclineMedicinePage(Medicine medicine, DoctorView dw)
        {
            InitializeComponent();
            Medicine = medicine;
            DataContext = Medicine;
            this.dw = dw;
            listViewAlergens.ItemsSource = medicine.Ingridient;
            Storage = new DeclinedMedicineStorage();
            MedStorage = new MedicineStorage();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            MedStorage.Delete(Medicine.MedicineID);
            var id = Storage.generateNextId();
            var Description = DescriptionTB.Text;
            var declinedMedicine = new DeclinedMedicine(id, Medicine, Description);
            Storage.Save(declinedMedicine);
            MedicinePageView mpw = new MedicinePageView(dw);
            mpw.MedicineTabs.SelectedIndex = 0;
            dw.Main.Content = mpw;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new MedicineRevisionPage(Medicine, dw);
        }
    }
}
