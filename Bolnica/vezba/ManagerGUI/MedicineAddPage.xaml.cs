using Model;
using Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace vezba.ManagerGUI
{
    public partial class MedicineAddPage : Page
    {
        public List<Medicine> medicineList { get; set; }
        private Medicine newMedicine;
        public static ObservableCollection<Ingridient> IngredientList { get; set; }
        public List<Ingridient> ingredientTemporaryList { get; set; }
        private MainManagerWindow mainManagerWindow;
        public MedicineAddPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            MedicineService medicineService = new MedicineService();
            medicineList = medicineService.GetApproved();
            comboReplacementMedicine.ItemsSource = medicineList;
            List<string> condition = new List<string> { "Kapsula", "Pilula", "Sirup" };
            comboCondition.ItemsSource = condition;
            newMedicine = new Medicine("Naziv", "Naziv", "Naziv", 0, MedicineCondition.pill);
            ingredientTemporaryList = new List<Ingridient>();
        }
        private void AddIngredientButtonClick(object sender, RoutedEventArgs e)
        {
            var ingredientName = NewIngredientTextBox.Text;
            var newIngredient = new Ingridient(ingredientName);
            ingredientTemporaryList.Add(newIngredient);
            IngredientList = new ObservableCollection<Ingridient>(ingredientTemporaryList);
            IngredientsBinding.ItemsSource = IngredientList;
            IngredientsBinding.Items.Refresh();
        }
        private void RemoveIngredientButtonClick(object sender, RoutedEventArgs e)
        {
            if (IngredientsBinding.SelectedIndex > -1)
            {
                Ingridient ingredient = (Ingridient)IngredientsBinding.SelectedItem;
                ingredientTemporaryList.Remove(ingredient);
                IngredientList = new ObservableCollection<Ingridient>(ingredientTemporaryList);
                IngredientsBinding.ItemsSource = IngredientList;
                IngredientsBinding.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var Name = NameTextBox.Text;
            var Manufacturer = ManufacturerTextBox.Text;
            var Packaging = PackagingTextBox.Text;
            var Condition = MedicineCondition.pill;

            if (comboCondition.SelectedIndex == 1) Condition = MedicineCondition.pill;
            else if (comboCondition.SelectedIndex == 0) Condition = MedicineCondition.capsule;
            else if (comboCondition.SelectedIndex == 2) Condition = MedicineCondition.syrup;
            
            Medicine replacementMedicine = (Medicine)comboReplacementMedicine.SelectedItem;
            MedicineService medicineService = new MedicineService();
            newMedicine = new Medicine(Name, Manufacturer, Packaging, 0, Condition) { ReplacementMedicine = replacementMedicine };
            MedicinePage.MedicineList.Add(newMedicine);
            newMedicine.ingridient = ingredientTemporaryList;
            medicineService.SaveMedicine(newMedicine);
            NavigationService.GoBack();
        }
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonMedicineClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicinePage(mainManagerWindow);
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }
    }
}
