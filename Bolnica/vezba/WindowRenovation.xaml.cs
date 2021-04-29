using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for WindowRenovation.xaml
    /// </summary>
    public partial class WindowRenovation : Window
    {
        private Room selected;
        public WindowRenovation(Room selected)
        {
            InitializeComponent();
            this.selected = selected;
            BrojProstorije.Content = BrojProstorije.Content + " " + selected.RoomNumber;
        }

        private void PotvrdiDodavanje1_Button_Click(object sender, RoutedEventArgs e)
        {
            var format = "dd/MM/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var StartTime = DateTime.ParseExact(PocetniDatum.Text, format, provider);
            var trajanje = int.Parse(Trajanje.Text);
            var id = int.Parse(Id.Text);

            var newRenovation = new Renovation(StartTime, trajanje, id) {};

            selected.AddRenovation(newRenovation);
            RoomStorage rs = new RoomStorage();
            rs.Update(this.selected);
            WindowRenovations.RenovationList.Add(newRenovation);
            this.Close();
        }

        private void OdustaniDodavanje1_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
