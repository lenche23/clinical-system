using Bolnica;
using Model;
using System;
using System.Collections.Generic;
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
using System.Timers;
using System.Threading;

namespace vezba
{
    /// <summary>
    /// Interaction logic for WindowExchangeStaticEquipment.xaml
    /// </summary>
    public partial class WindowExchangeStaticEquipment : Window
    {
        public WindowUpdateRoom wur;
        public Room room;
        public RoomInventory roomInventory;
        public Equipment eq;
        public ManagerView mv;

        public WindowExchangeStaticEquipment(RoomInventory selected, WindowUpdateRoom wur, Room r_selected, ManagerView mv)
        {
            InitializeComponent();
            this.wur = wur;
            this.roomInventory = selected;
            this.room = r_selected;
            this.mv = mv;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            int id_sobe = int.Parse(BrojSobe.Text);
            int kolicina_robe = int.Parse(Količina.Text);
            int maks_kolicina = this.roomInventory.Quantity;

            if (DateTime.Compare(Date.SelectedDate.Value.Date, DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je vec prosao!");
            }
            else
            {
                //DateTime datum = Date.SelectedDate.Value.Date;
                int newQuantity1 = this.roomInventory.Quantity - kolicina_robe;
                int newQuantity2 = 0;

                RoomStorage rs = new RoomStorage();
                RoomInventoryStorage ris = new RoomInventoryStorage();
                Room room1 = rs.GetOne(id_sobe);
                DateTime time = DateTime.Now.AddSeconds(10);
                var exists = false;

                if (room1 != null && room1 != this.room)
                {
                    if (maks_kolicina >= kolicina_robe)
                    {
                        this.roomInventory.EndTime = time;
                        ris.Update(this.roomInventory);

                        foreach (RoomInventory ri in ris.GetAll())
                        {
                            if (ri.room.RoomNumber == id_sobe && DateTime.Compare(ri.StartTime, DateTime.Now) <= 0 && DateTime.Compare(ri.EndTime, DateTime.Now) >= 0 && ri.equipment.Id == roomInventory.equipment.Id) 
                            {
                                 ri.EndTime = time;
                                 newQuantity2 = ri.Quantity + kolicina_robe;
                                 ris.Update(ri);
                                 exists = true;                   
                            }
                        }
         
                        if (exists == false)
                        {
                            newQuantity2 = kolicina_robe;
                        }
                    }

                    var endTime = new DateTime(2999, 12, 31);
                    var id1 = ris.GenerateNextId();
                    RoomInventory ri1 = new RoomInventory(time, endTime, newQuantity1, id1, roomInventory.equipment, room);
                    ris.Save(ri1);
                    var id2 = ris.GenerateNextId();
                    RoomInventory ri2 = new RoomInventory(time, endTime, newQuantity2, id2, roomInventory.equipment, room1);
                    ris.Save(ri2);
                    this.Close();
                }
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
