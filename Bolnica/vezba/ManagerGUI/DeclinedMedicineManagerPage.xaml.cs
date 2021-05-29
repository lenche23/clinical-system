using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;
namespace vezba.ManagerGUI
{

    public partial class DeclinedMedicineManagerPage : Page
    {
        private List<DeclinedMedicine> declinedMedicineList;
        private ObservableCollection<DeclinedMedicine> DeclinedMedicineList { get; set; }
        private MainManagerWindow mainManagerWindow;
        public DeclinedMedicineManagerPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            MedicineService medicineService = new MedicineService();
            declinedMedicineList = medicineService.GetAllDeclinedMedicine();
            DeclinedMedicineList = new ObservableCollection<DeclinedMedicine>(declinedMedicineList);
            DeclinedMedicineBinding.ItemsSource = DeclinedMedicineList;
        }

        private void ViewDetailButton(object sender, RoutedEventArgs e)
        {
            if (DeclinedMedicineBinding.SelectedIndex > -1)
            {
                DeclinedMedicine selected = (DeclinedMedicine)DeclinedMedicineBinding.SelectedItems[0];
                mainManagerWindow.MainManagerView.Content = new DetailDeclinedMedicinePage(mainManagerWindow, selected);
            }

            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }
    }
}
