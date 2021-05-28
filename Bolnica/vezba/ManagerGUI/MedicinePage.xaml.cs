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
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class MedicinePage : Page
    {
        public static ObservableCollection<Medicine> MedicineList { get; set; }
        public static List<Medicine> medicineList;
        private MedicineFileRepository _medicineFileRepository;
        private MainManagerWindow mainManagerWindow;
        public MedicinePage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            _medicineFileRepository = new MedicineFileRepository();
            medicineList = _medicineFileRepository.GetAll();
            MedicineList = new ObservableCollection<Medicine>(medicineList);
            MedicineBinding.ItemsSource = MedicineList;
        }

        private void Add_Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicineAddPage();
        }

        private void Remove_Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MedicineBinding.SelectedIndex > -1)
            {
                Medicine m = (Medicine)MedicineBinding.SelectedItem;
                MedicineFileRepository ms = new MedicineFileRepository();
                ms.Delete(m.MedicineID);
                MedicineList.Remove(m);
            }

            else
            {
                MessageBox.Show("Ni jedan lek nije selektovan!");
            }
        }

        private void Detail_Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MedicineBinding.SelectedIndex > -1)
            {
                Medicine medicine = (Medicine)MedicineBinding.SelectedItems[0];
                mainManagerWindow.MainManagerView.Content = new MedicineDetailPage(mainManagerWindow, medicine);
            }

            else
            {
                MessageBox.Show("Ni jedan lek nije selektovan!");
            }
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage(mainManagerWindow);
        }

        private void Declined_Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new DeclinedMedicineManagerPage(mainManagerWindow);
        }

        private void Edit_Medicine_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
