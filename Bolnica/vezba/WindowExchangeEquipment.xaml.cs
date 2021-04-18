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

        public WindowExchangeEquipment(Equipment selected, WindowUpdateRoom wur)
        {
            InitializeComponent();
            this.wur = wur;
            this.eq = selected;
        }

        private void Potvrdi_Button_Click(object sender, RoutedEventArgs e)
        {
            int id_sobe = int.Parse(BrojSobe.Text);
            int kolicina_robe = int.Parse(Količina.Text);
            eq.Quantity = kolicina_robe;
            RoomStorage rs = new RoomStorage();
            room = rs.GetOne(id_sobe);
            room.AddEquipment(eq);

            rs.Update(room);
            WindowUpdateRoom.EquipmentList.Add(eq);
            this.Close();
        }
        private void Odustani_Button_Click(object sender, RoutedEventArgs e)
        {
                    this.Close();
                }
    }
}
