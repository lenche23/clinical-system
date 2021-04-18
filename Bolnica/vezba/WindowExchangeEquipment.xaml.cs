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
    /// Interaction logic for WindowExchangeEquipment.xaml
    /// </summary>
    public partial class WindowExchangeEquipment : Window
    {
        WindowUpdateRoom wur;
        Room room;
        Equipment eq;
        ManagerView mv;

        public WindowExchangeEquipment(Equipment selected, WindowUpdateRoom wur, Room r_selected, ManagerView mv)
        {
            InitializeComponent();
            this.wur = wur;
            this.eq = selected;
            this.room = r_selected;
            this.mv = mv;
        }

        private void Potvrdi_Button_Click(object sender, RoutedEventArgs e)
        {
            int id_sobe = int.Parse(BrojSobe.Text);
            int kolicina_robe = int.Parse(Količina.Text);
            int maks_kolicina = this.eq.Quantity;

            RoomStorage rs = new RoomStorage();
            Room room1 = rs.GetOne(id_sobe);

            if (room1 != null && room1 != this.room)
            {

                if (maks_kolicina >= kolicina_robe)
                {
                    this.room.QuantityEquipment(eq, -kolicina_robe);
                    rs.Update(this.room);
                    wur.EquipmentBinding.Items.Refresh();

                    ObservableCollection<Room> Rooms = ManagerView.Rooms;

                    foreach (Room r in Rooms)
                    {
                        if (r.RoomNumber == room1.RoomNumber)
                        {
                            r.QuantityEquipment(eq, kolicina_robe);
                            rs.Update(r);
                        }
                    }

                    this.Close();
                }
            }
        }
        private void Odustani_Button_Click(object sender, RoutedEventArgs e)
        {
                    this.Close();
                }
    }
}
