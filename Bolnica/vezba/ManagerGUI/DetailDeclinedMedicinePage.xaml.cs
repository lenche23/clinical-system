using System.Windows.Controls;
using Model;

namespace vezba.ManagerGUI
{

    public partial class DetailDeclinedMedicinePage : Page
    {
        private DeclinedMedicine declinedMedicine;
        private MainManagerWindow mainManagerWindow;
        public DetailDeclinedMedicinePage(MainManagerWindow mainManagerWindow, DeclinedMedicine declinedMedicine)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            Proizvodjac.Text = Proizvodjac.Text + " " + declinedMedicine.MedicineManufacturer;
            Naziv.Text = Naziv.Text + " " + declinedMedicine.MedicineName;
            Oblik.Text = Oblik.Text + " " + declinedMedicine.Medicine.Condition;
            Pakovanje.Text = Pakovanje.Text + " " + declinedMedicine.Medicine.Packaging;
            if (declinedMedicine.Medicine.ReplacementMedicine != null)
                Zamenski.Text = Zamenski.Text + " " + declinedMedicine.Medicine.ReplacementMedicine.Name;

            listViewAlergens.ItemsSource = declinedMedicine.Medicine.ingridient;
            Komentar.Text = Komentar.Text + " " + declinedMedicine.Description;
        }
    }
}

