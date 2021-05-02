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
using System.Windows.Shapes;

namespace vezba
{
    /// <summary>
    /// Interaction logic for Inventary.xaml
    /// </summary>
    public partial class Inventary : Window
    {
        public static ObservableCollection<Equipment> EquipmentList { get; set; }
        public static List<Equipment> equipmentList;
        private EquipmentStorage storage;

        public Inventary()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new EquipmentStorage();
            equipmentList = storage.GetAll();
            EquipmentList = new ObservableCollection<Equipment>(equipmentList);
            InventaryBinding.ItemsSource = EquipmentList;
        }
    }
}
