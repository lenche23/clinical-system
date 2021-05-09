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

namespace vezba
{
    public partial class DeclinedMedicineWindow : Window
    {
        private DeclinedMedicineStorage declinedMedicineStorage;
        private List<DeclinedMedicine> declinedMedicineList;
        private ObservableCollection<DeclinedMedicine> DeclinedMedicineList { get; set; }
        public DeclinedMedicineWindow()
        {
            InitializeComponent();
            declinedMedicineStorage = new DeclinedMedicineStorage();
            declinedMedicineList = declinedMedicineStorage.GetAll();
            DeclinedMedicineList = new ObservableCollection<DeclinedMedicine>(declinedMedicineList);
            DeclinedMedicineBinding.ItemsSource = DeclinedMedicineList;
        }

        private void ViewDetailButton(object sender, RoutedEventArgs e)
        {
            if (DeclinedMedicineBinding.SelectedIndex > -1)
            {
                DeclinedMedicine selected = (DeclinedMedicine) DeclinedMedicineBinding.SelectedItems[0];
                var s = new DetailDeclinedMedicineWindow(selected);
                s.Show();
            }

            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }
    }
}
