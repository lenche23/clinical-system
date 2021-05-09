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

namespace vezba.ManagerGUI
{
    /// <summary>
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        public static ObservableCollection<Equipment> EquipmentList { get; set; }
        public static List<Equipment> equipmentList;
        private EquipmentStorage storage;
        private MainManagerWindow mainManagerWindow;

        public InventoryPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            storage = new EquipmentStorage();
            equipmentList = storage.GetAll();
            EquipmentList = new ObservableCollection<Equipment>(equipmentList);
            InventaryBinding.ItemsSource = EquipmentList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(InventaryBinding.ItemsSource);
            view.Filter = EquipmentFilter;
        }

        private void Add_Equipment_Button_Click(object sender, RoutedEventArgs e)
        {
            //mainManagerWindow.MainManagerView.Content = new InventoryAddEquipmentPage();
            //equipmentList = storage.GetAll();
        }

        private void Remove_Equipment_Button_Click(object sender, RoutedEventArgs e)
        {
            if (InventaryBinding.SelectedIndex > -1)
            {
                Equipment equipment = (Equipment)InventaryBinding.SelectedItem;
                storage.Delete(equipment.Id);
                equipmentList = storage.GetAll();
                EquipmentList.Remove(equipment);
            }

            else
            {
                MessageBox.Show("Ni jedan artikal nije selektovan!");
            }
        }

        private void View_Equipment_Button_Click(object sender, RoutedEventArgs e)
        {
            if (InventaryBinding.SelectedIndex > -1)
            {
                Equipment equipment = (Equipment)InventaryBinding.SelectedItem;
                //mainManagerWindow.MainManagerView.Content = new InventoryViewEquipmentPage(equipment);
                var s = new InventaryViewEquipment(equipment);
                s.Show();
            }

            else
            {
                MessageBox.Show("Ni jedan artikal nije selektovan!");
            }
        }

        private void Change_Equipment_Button_Click(object sender, RoutedEventArgs e)
        {
            if (InventaryBinding.SelectedIndex > -1)
            {
                Equipment equipment = (Equipment)InventaryBinding.SelectedItem;
                //mainManagerWindow.MainManagerView.Content = new InventoryViewEquipmentPage(equipment);
                //var s = new InventaryChangeEquipment(equipment, this);
                //s.Show();
            }

            else
            {
                MessageBox.Show("Ni jedan artikal nije selektovan!");
            }
        }


        private void StaticCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (EquipmentList != null)
            {
                if (CheckedBoxStatic.IsChecked == true)

                {
                    if (equipmentList != null)
                    {
                        foreach (Equipment equipment in equipmentList)
                        {
                            if (equipment.Type == EquipmentType.statical)
                            {
                                EquipmentList.Add(equipment);
                                InventaryBinding.Items.Refresh();
                            }
                        }
                    }
                }
            }
        }

        private void DinamicCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (EquipmentList != null)
            {
                if (CheckedBoxDinamic.IsChecked == true)
                {

                    foreach (Equipment equipment in equipmentList)
                    {
                        if (equipment.Type == EquipmentType.dinamical)
                        {
                            EquipmentList.Add(equipment);
                            InventaryBinding.Items.Refresh();
                        }
                    }
                }
            }
        }

        private bool EquipmentFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Equipment).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(InventaryBinding.ItemsSource).Refresh();

        }

        private void StaticCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EquipmentList != null)
            {
                if (CheckedBoxStatic.IsChecked == false)
                {
                    foreach (Equipment equipment in equipmentList)
                    {
                        if (equipment.Type == EquipmentType.statical)
                        {
                            for (int i = 0; i < EquipmentList.Count; i++)
                            {
                                if (EquipmentList[i].Id == equipment.Id)
                                {
                                    EquipmentList.Remove(EquipmentList[i]);
                                    InventaryBinding.Items.Refresh();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DinamicCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EquipmentList != null)
            {
                if (CheckedBoxDinamic.IsChecked == false)
                {
                    foreach (Equipment equipment in equipmentList)
                    {
                        if (equipment.Type == EquipmentType.dinamical)
                        {
                            for (int i = 0; i < EquipmentList.Count; i++)
                            {
                                if (EquipmentList[i].Id == equipment.Id)
                                {
                                    EquipmentList.Remove(EquipmentList[i]);
                                    InventaryBinding.Items.Refresh();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
