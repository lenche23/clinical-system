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
using System.Windows.Shapes;
using Model;
using vezba.Repository;
namespace vezba.ManagerGUI
{

    public partial class DeclinedMedicineManagerPage : Page
    {
        private DeclinedMedicineFileRepository _declinedMedicineFileRepository;
        private List<DeclinedMedicine> declinedMedicineList;
        private ObservableCollection<DeclinedMedicine> DeclinedMedicineList { get; set; }
        private MainManagerWindow mainManagerWindow;
        public DeclinedMedicineManagerPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            _declinedMedicineFileRepository = new DeclinedMedicineFileRepository();
            declinedMedicineList = _declinedMedicineFileRepository.GetAll();
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
