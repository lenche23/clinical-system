using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for DetailDeclinedMedicineWindow.xaml
    /// </summary>
    public partial class DetailDeclinedMedicineWindow : Window
    {
        private DeclinedMedicine declinedMedicine;
        public DetailDeclinedMedicineWindow(DeclinedMedicine declinedMedicine)
        {
            InitializeComponent();
            Proizvodjac.Text = Proizvodjac.Text + " " + declinedMedicine.MedicineManufacturer;
            Naziv.Text = Naziv.Text + " " + declinedMedicine.MedicineName;
            Oblik.Text = Oblik.Text + " " + declinedMedicine.Medicine.Condition;
            Pakovanje.Text = Pakovanje.Text + " " + declinedMedicine.Medicine.Packaging;
            if (declinedMedicine.Medicine.ReplacementMedicine!= null)
                Zamenski.Text = Zamenski.Text + " " + declinedMedicine.Medicine.ReplacementMedicine.Name;

            listViewAlergens.ItemsSource = declinedMedicine.Medicine.ingridient;
            Komentar.Text = Komentar.Text + " " + declinedMedicine.Description;
        }
    }
}
