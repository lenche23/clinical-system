using System.Windows.Controls;
using Model;

namespace vezba.ManagerGUI
{
    public partial class DetailDeclinedMedicinePage : Page
    {
        private MainManagerWindow mainManagerWindow;
        private DeclinedMedicine declinedMedicine;
        private MedicinePage medicinePage;
        public DetailDeclinedMedicinePage(MainManagerWindow mainManagerWindow, DeclinedMedicine declinedMedicine, MedicinePage medicinePage)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            this.medicinePage = medicinePage;
            this.declinedMedicine = declinedMedicine;
            Proizvodjac.Text = Proizvodjac.Text + " " + declinedMedicine.MedicineManufacturer;
            Naziv.Text = Naziv.Text + " " + declinedMedicine.MedicineName;
            Oblik.Text = Oblik.Text + " " + declinedMedicine.Medicine.Condition;
            Pakovanje.Text = Pakovanje.Text + " " + declinedMedicine.Medicine.Packaging;
            if (declinedMedicine.Medicine.ReplacementMedicine != null) Zamenski.Text = Zamenski.Text + " " + declinedMedicine.Medicine.ReplacementMedicine.Name;
            listViewAlergens.ItemsSource = declinedMedicine.Medicine.ingridient;
            Komentar.Text = Komentar.Text + " " + declinedMedicine.Description;
        }

        private void EditButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new DeclinedMedicineEditPage(mainManagerWindow, declinedMedicine, medicinePage);
        }

        private void ButtonBackClick(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

