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
        RoomInventory roomInventory;
        Equipment eq;
        ManagerView mv;

        public WindowExchangeEquipment(RoomInventory selected, WindowUpdateRoom wur, Room r_selected, ManagerView mv)
        {
            InitializeComponent();
            this.wur = wur;
            this.roomInventory = selected;
            this.room = r_selected;
            this.mv = mv;
        }

        private void Potvrdi_Button_Click(object sender, RoutedEventArgs e)
        {
            int id_sobe = int.Parse(BrojSobe.Text);
            int kolicina_robe = int.Parse(Količina.Text);
            int maks_kolicina = this.roomInventory.Quantity;

            RoomStorage rs = new RoomStorage();
            RoomInventoryStorage ris = new RoomInventoryStorage();
            Room room1 = rs.GetOne(id_sobe);

            if (room1 != null && room1 != this.room)
            {

                if (maks_kolicina >= kolicina_robe)
                {
                    this.roomInventory.Quantity -= kolicina_robe;
                    ris.Update(this.roomInventory);

                    wur.RoomInventoryBinding.Items.Refresh();

                    var exists = false;

                    foreach (RoomInventory ri in ris.GetAll())
                    {
                        if(ri.room.RoomNumber == id_sobe) {
                            if (ri.equipment.Id == roomInventory.equipment.Id)
                            {
                                ri.Quantity += kolicina_robe;
                                ris.Update(ri);
                                exists = true;
                            }
                        }
                    }

                    if (exists == false)
                    {
                        var endTime = new DateTime(2999, 12, 31);
                        var id = ris.GenerateNextId();
                        RoomInventory ri = new RoomInventory(DateTime.Now, endTime, kolicina_robe, id, roomInventory.equipment, room1);
                        ris.Save(ri);
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
