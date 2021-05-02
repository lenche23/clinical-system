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
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for WindowUpdateRoom.xaml
    /// </summary>
    /// 
    public partial class WindowUpdateRoom : Window
    {
        private Room selected;
        private ManagerView mv;

        public static ObservableCollection<Equipment> EquipmentList { get; set; }
        private EquipmentStorage eq_storage;

        public WindowUpdateRoom(Room selected, ManagerView mv)
        {
            InitializeComponent();
            this.selected = selected;
            this.mv = mv;
            this.DataContext = selected;
            if(selected.RoomFloor == Floor.first)
            {
                Prvi.IsChecked = true;
            }
            else if(selected.RoomFloor == Floor.second)
            {
                Drugi.IsChecked = true;
            }
            else if(selected.RoomFloor == Floor.third)
            {
                Treci.IsChecked = true;
            }


            if(selected.RoomType == RoomType.examinationRoom)
            {
                Pregled.IsChecked = true;
            }
            else if(selected.RoomType == RoomType.operatingRoom)
            {
                Operacija.IsChecked = true;
            }
            else if(selected.RoomType == RoomType.recoveryRoom)
            {
                Odmor.IsChecked = true;
            }


            List<Equipment> equipmentList = selected.equipment;
            EquipmentList = new ObservableCollection<Equipment>(equipmentList);
            EquipmentBinding.ItemsSource = EquipmentList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(EquipmentBinding.ItemsSource);
            view.Filter = EquipmentFilter;

        }

        private void Potvrda_Button_Click(object sender, RoutedEventArgs e)
        {
            var RoomNumber = int.Parse(BrojSobe.Text);
            selected.RoomNumber = RoomNumber;

            

            if (Convert.ToBoolean(Prvi.IsChecked))
            {
                selected.RoomFloor = Floor.first;
            }

            else if (Convert.ToBoolean(Drugi.IsChecked))
            {
                selected.RoomFloor = Floor.second;
            }

            else if (Convert.ToBoolean(Treci.IsChecked))
            {
                selected.RoomFloor = Floor.third;
            }


            if (Convert.ToBoolean(Pregled.IsChecked))
            {
                selected.RoomType = RoomType.examinationRoom;
            }

            else if (Convert.ToBoolean(Operacija.IsChecked))
            {
                selected.RoomType = RoomType.operatingRoom;
            }

            else if (Convert.ToBoolean(Odmor.IsChecked))
            {
                selected.RoomType = RoomType.recoveryRoom;
            }
            mv.lvDataBinding.Items.Refresh();
            this.Close();
        }

        private void Odustanak_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dodaj_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new WindowAddRoomEquipment(this.selected);
            s.Show();
        }

        private void Izbriši_Button_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentBinding.SelectedIndex > -1)
            {
                Equipment eq = (Equipment)EquipmentBinding.SelectedItem;
                 this.selected.equipment.Remove(eq);
                 EquipmentList.Remove(eq);
                RoomStorage rs = new RoomStorage();
                 rs.Update(this.selected);
            }

            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }

        }

        private void Izmeni_Količinu_Button_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentBinding.SelectedIndex > -1)
            {
                Equipment selected_e = (Equipment)EquipmentBinding.SelectedItems[0];
                var s = new WindowChangeItemQuantity(selected_e, this, this.selected);
                s.Show();
            }

            else
            {
                MessageBox.Show("Ni jedan proizvod nije selektovan!");
            }
        }

        private void Razmena_Button_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentBinding.SelectedIndex > -1)
            {
                Equipment eq_selected = (Equipment)EquipmentBinding.SelectedItems[0];
            var s = new WindowExchangeEquipment(eq_selected, this, this.selected, this.mv);
            s.Show();

            }

            else
            {
                MessageBox.Show("Ni jedan proizvod nije selektovan!");
            }
        }

        private void StaticCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (EquipmentList != null)
            {
                if (CheckedBoxStatic.IsChecked == true)
                {
                    foreach (Equipment equipment in selected.equipment)
                    {
                        if (equipment.Type == EquipmentType.statical)
                        {
                            EquipmentList.Add(equipment);
                            EquipmentBinding.Items.Refresh();
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
                    foreach (Equipment equipment in selected.equipment)
                    {
                        if (equipment.Type == EquipmentType.dinamical)
                        {
                            EquipmentList.Add(equipment);
                            EquipmentBinding.Items.Refresh();
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
            CollectionViewSource.GetDefaultView(EquipmentBinding.ItemsSource).Refresh();
        }

        private void StaticCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EquipmentList != null)
            {
                if (CheckedBoxStatic.IsChecked == false)
                {
                    foreach (Equipment equipment in selected.equipment)
                    {
                        if (equipment.Type == EquipmentType.statical)
                        {
                            Equipment eq = equipment;
                            EquipmentList.Remove(eq);
                            EquipmentBinding.Items.Refresh();
                        }
                    }
                }
            }
        }

        private void DinamicCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EquipmentList != null) { 
            if (CheckedBoxDinamic.IsChecked == false)
            {
                foreach (Equipment equipment in selected.equipment)
                {
                    if (equipment.Type == EquipmentType.dinamical)
                    {
                        EquipmentList.Remove(equipment);
                        EquipmentBinding.Items.Refresh();
                        }
                    }
                }
            }
         }
    }
}
