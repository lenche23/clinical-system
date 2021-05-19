using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
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

        public static ObservableCollection<RoomInventory> RoomInventoryList { get; set; }
        private RoomInventoryFileRepository _roomInventoryFileRepository;

        public WindowUpdateRoom(Room selected, ManagerView mv)
        {
            InitializeComponent();
            this.selected = selected;
            this.mv = mv;
            this.DataContext = selected;
            if (selected.RoomFloor == Floor.first)
            {
                Prvi.IsChecked = true;
            }
            else if (selected.RoomFloor == Floor.second)
            {
                Drugi.IsChecked = true;
            }
            else if (selected.RoomFloor == Floor.third)
            {
                Treci.IsChecked = true;
            }


            if (selected.RoomType == RoomType.examinationRoom)
            {
                Pregled.IsChecked = true;
            }
            else if (selected.RoomType == RoomType.operatingRoom)
            {
                Operacija.IsChecked = true;
            }
            else if (selected.RoomType == RoomType.recoveryRoom)
            {
                Odmor.IsChecked = true;
            }


            _roomInventoryFileRepository = new RoomInventoryFileRepository();

            List<RoomInventory> roomInventoryList = new List<RoomInventory>();
            foreach (RoomInventory roomInventory in _roomInventoryFileRepository.GetAll())
            {
                if (roomInventory.room.RoomNumber == selected.RoomNumber)
                    if (DateTime.Compare(roomInventory.StartTime, DateTime.Now) <= 0 && DateTime.Compare(roomInventory.EndTime, DateTime.Now) >= 0)
                    {
                        {
                            roomInventoryList.Add(roomInventory);
                        }
                    }
            }

            RoomInventoryList = new ObservableCollection<RoomInventory>(roomInventoryList);
            RoomInventoryBinding.ItemsSource = RoomInventoryList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(RoomInventoryBinding.ItemsSource);
            RoomInventoryBinding.Items.Refresh();
            view.Filter = EquipmentFilter;

        }

        private void Potvrda_Button_Click(object sender, RoutedEventArgs e)
        {
            var roomNumber = int.Parse(BrojSobe.Text);
            selected.RoomNumber = roomNumber;



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
            if (RoomInventoryBinding.SelectedIndex > -1)
            {
                RoomInventory ri = (RoomInventory)RoomInventoryBinding.SelectedItem;
                _roomInventoryFileRepository.Delete(ri.Id);
                RoomInventoryList.Remove(ri);
            }

            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }

        }

        private void Izmeni_Količinu_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoomInventoryBinding.SelectedIndex > -1)
            {

                RoomInventory selectedRoomInventory = (RoomInventory)RoomInventoryBinding.SelectedItems[0];
                var s = new WindowChangeItemQuantity(selectedRoomInventory, this, this.selected);
                s.Show();
            }

            else
            {
                MessageBox.Show("Ni jedan proizvod nije selektovan!");
            }
        }

        private void Razmena_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoomInventoryBinding.SelectedIndex > -1)
            {
                RoomInventory roomInventorySelected = (RoomInventory)RoomInventoryBinding.SelectedItems[0];
                if (roomInventorySelected.equipment.Type == EquipmentType.dinamical)
                {
                    var s = new WindowExchangeEquipment(roomInventorySelected, this, this.selected);
                    s.Show();
                }
                else if (roomInventorySelected.equipment.Type == EquipmentType.statical)
                {
                    var s = new WindowExchangeStaticEquipment(roomInventorySelected, this.selected);
                    s.Show();
                }

            }

            else
            {
                MessageBox.Show("Ni jedan proizvod nije selektovan!");
            }
        }

        private void StaticCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            /* if (RoomInventoryList != null)
             {
                 if (CheckedBoxStatic.IsChecked == true)
                 {
                     foreach (RoomInventory roomInventory in _roomInventoryFileRepository.GetAll())
                     {
                         if (roomInventory.room.RoomNumber == selected.RoomNumber)
                         {
                             if (roomInventory.equipment.Type == EquipmentType.statical)
                             {
                                 RoomInventoryList.Add(roomInventory);
                                 RoomInventoryBinding.Items.Refresh();
                             }
                         }
                     }
                 }
             }*/
        }

        private void DinamicCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            /*if (RoomInventoryList != null)
            {
                if (CheckedBoxDinamic.IsChecked == true)
                {
                    foreach (RoomInventory roomInventory in _roomInventoryFileRepository.GetAll())
                    {
                        if (roomInventory.room.RoomNumber == selected.RoomNumber)
                        {
                            if (roomInventory.equipment.Type == EquipmentType.dinamical)
                            {
                                RoomInventoryList.Add(roomInventory);
                                RoomInventoryBinding.Items.Refresh();
                            }
                        }
                    }
                }
            }*/
        }

        private bool EquipmentFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as RoomInventory).equipment.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RoomInventoryBinding.ItemsSource).Refresh();

        }

        private void StaticCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            /*if (RoomInventoryList != null)
            {
                if (CheckedBoxStatic.IsChecked == false)
                {
                    foreach (RoomInventory roomInventory in _roomInventoryFileRepository.GetAll())
                    {
                        if (roomInventory.room.RoomNumber == selected.RoomNumber)
                        {
                            if (roomInventory.equipment.Type == EquipmentType.statical)
                            {
                                for (int i = 0; i < RoomInventoryList.Count; i++)
                                {
                                    if (RoomInventoryList[i].Id == roomInventory.Id)
                                    {
                                        RoomInventoryList.Remove(RoomInventoryList[i]);
                                        RoomInventoryBinding.Items.Refresh();
                                    }
                                }
                            }
                        }
                    }
                }
            }*/
        }

        private void DinamicCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            /* if (RoomInventoryList != null)
             {
                 if (CheckedBoxDinamic.IsChecked == false)
                 {
                     foreach (RoomInventory roomInventory in _roomInventoryFileRepository.GetAll())
                     {
                         if (roomInventory.room.RoomNumber == selected.RoomNumber)
                         {
                             if (roomInventory.equipment.Type == EquipmentType.dinamical)
                             {
                                 for (int i=0; i<RoomInventoryList.Count; i++)
                                 {
                                     if(RoomInventoryList[i].Id == roomInventory.Id)
                                     {
                                         RoomInventoryList.Remove(RoomInventoryList[i]);
                                         RoomInventoryBinding.Items.Refresh();
                                     }
                                 }
                             }
                         }
                     }
                 }
             }*/
        }
    }
}
