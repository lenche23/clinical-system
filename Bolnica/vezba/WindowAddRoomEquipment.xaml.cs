using Bolnica;
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
    /// Interaction logic for WindowAddRoomEquipment.xaml
    /// </summary>
    public partial class WindowAddRoomEquipment : Window
    {
        private Room selected;
        public WindowAddRoomEquipment(Room selected)
        {
            InitializeComponent();
            this.selected = selected;
        }

        private void PotvrdiDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            EquipmentType type = EquipmentType.statical;

            if (Convert.ToBoolean(Statička.IsChecked))
            {
                type = EquipmentType.statical;
            }

            else if (Convert.ToBoolean(Dinamička.IsChecked))
            {
                type = EquipmentType.dinamical;
            }

            var newEquipment = new Equipment(int.Parse(ID.Text), NazivOpreme.Text, int.Parse(Količina.Text), type) { };

            int id = int.Parse(ID.Text);

            ObservableCollection<Equipment> EquipmentList = Inventary.EquipmentList;
            EquipmentStorage es = new EquipmentStorage();

            if (EquipmentList == null)
            {
                es.Save(newEquipment);
            }
            else
            {
                foreach (Equipment equipment in EquipmentList)
                {
                    if (equipment.Id != id)
                    {
                        es.Save(newEquipment);
                    }
                    else
                    {
                        es.Update(newEquipment);
                    }
                }
            }
            this.selected.AddEquipment(newEquipment);
            RoomStorage rs = new RoomStorage();
            rs.Update(this.selected);
            WindowUpdateRoom.EquipmentList.Add(newEquipment);
            this.Close();
        }

        private void OdustaniDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
