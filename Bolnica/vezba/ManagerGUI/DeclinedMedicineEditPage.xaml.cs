using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class DeclinedMedicineEditPage : Page, INotifyPropertyChanged
    {
        private MainManagerWindow mainManagerWindow;
        private DeclinedMedicine declinedMedicine;
        private MedicinePage medicinePage;
        public static ObservableCollection<Ingridient> IngredientList { get; set; }
        public List<Ingridient> ingredientTemporaryList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private String nazivRobe;

        public String NazivRobe
        {
            get { return nazivRobe; }
            set
            {
                if (value != nazivRobe)
                {
                    nazivRobe = value;
                    OnPropertyChanged("NazivRobe");
                }
            }
        }

        private String nazivProizvodjaca;

        public String NazivProizvodjaca
        {
            get { return nazivProizvodjaca; }
            set
            {
                if (value != nazivProizvodjaca)
                {
                    nazivProizvodjaca = value;
                    OnPropertyChanged("NazivProizvodjaca");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public DeclinedMedicineEditPage(MainManagerWindow mainManagerWindow, DeclinedMedicine declinedMedicine, MedicinePage medicinePage)
        {
            InitializeComponent();
            DataContext = this;
            this.medicinePage = medicinePage;
            this.declinedMedicine = declinedMedicine;
            this.mainManagerWindow = mainManagerWindow;
            MedicineService medicineService = new MedicineService(new MedicineFileRepository());
            List<Medicine> replacementMedicineList = medicineService.GetApproved();
            comboReplacementMedicine.ItemsSource = replacementMedicineList;

            List<string> condition = new List<string> { "Kapsula", "Pilula", "Sirup" };
            comboCondition.ItemsSource = condition;

            ingredientTemporaryList = declinedMedicine.Medicine.ingridient;
            NameTextBox.Text = declinedMedicine.MedicineName;
            ManufacturerTextBox.Text = declinedMedicine.MedicineManufacturer;
            PackagingTextBox.Text = declinedMedicine.Medicine.Packaging;

            if (declinedMedicine.Medicine.Condition == MedicineCondition.capsule) comboCondition.SelectedIndex = 0;
            else if (declinedMedicine.Medicine.Condition == MedicineCondition.pill) comboCondition.SelectedIndex = 1;
            else if (declinedMedicine.Medicine.Condition == MedicineCondition.syrup) comboCondition.SelectedIndex = 2;
            if (declinedMedicine.Medicine.ReplacementMedicine == null) { }
            else
            {
                for (int i = 0; i < replacementMedicineList.Count; i++)
                {
                    if (replacementMedicineList[i].Name == declinedMedicine.Medicine.ReplacementMedicine.Name)
                    {
                        comboReplacementMedicine.SelectedIndex = i;
                    }
                }
            }
            List<Ingridient> ingredientList = declinedMedicine.Medicine.ingridient;
            IngredientList = new ObservableCollection<Ingridient>(ingredientList);
            IngredientsBinding.ItemsSource = IngredientList;

            NazivRobe = declinedMedicine.MedicineName;
            NazivProizvodjaca = declinedMedicine.MedicineManufacturer;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateEntries()) return;
            declinedMedicine.Medicine.Name = NameTextBox.Text;
            declinedMedicine.Medicine.Manufacturer = ManufacturerTextBox.Text;
            declinedMedicine.Medicine.Packaging = PackagingTextBox.Text;

            if (comboCondition.SelectedIndex == 1)
            {
                declinedMedicine.Medicine.Condition = MedicineCondition.pill;
            }

            else if (comboCondition.SelectedIndex == 0)
            {
                declinedMedicine.Medicine.Condition = MedicineCondition.capsule;
            }

            else if (comboCondition.SelectedIndex == 2)
            {
                declinedMedicine.Medicine.Condition = MedicineCondition.syrup;
            }

            declinedMedicine.Medicine.ReplacementMedicine = (Medicine)comboReplacementMedicine.SelectedItem;
            var declinedMedicineService = new DeclinedMedicineService(new DeclinedMedicineFileRepository());
            declinedMedicineService.DeleteDeclinedMedicine(declinedMedicine.DeclinedMedicineID);
            var medicineService = new MedicineService(new MedicineFileRepository());
            medicineService.SaveMedicine(declinedMedicine.Medicine);
            medicinePage.MedicineBinding.Items.Refresh(); 
            mainManagerWindow.MainManagerView.Content = new DeclinedMedicineManagerPage(mainManagerWindow, medicinePage);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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


            if (NameTextBox.Text == "" || ManufacturerTextBox.Text == "" || ingredientTemporaryList.Count == 0)
            {
                OkButton.IsEnabled = false;
            }
            else OkButton.IsEnabled = true;
        }
        private void AddIngredientButtonClick(object sender, RoutedEventArgs e)
        {
            var ingredientName = NewIngredientTextBox.Text;
            var newIngredient = new Ingridient(ingredientName);
            ingredientTemporaryList.Add(newIngredient);
            IngredientList = new ObservableCollection<Ingridient>(ingredientTemporaryList);
            IngredientsBinding.ItemsSource = IngredientList;
            IngredientsBinding.Items.Refresh();


            if (NameTextBox.Text == "" || ManufacturerTextBox.Text == "" || ingredientTemporaryList.Count == 0)
            {
                OkButton.IsEnabled = false;
            }
            else OkButton.IsEnabled = true;
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

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text == "" || ManufacturerTextBox.Text == "" || ingredientTemporaryList.Count == 0)
            {
                OkButton.IsEnabled = false;
            }
            else OkButton.IsEnabled = true;
        }

        private void ManufacturerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text == "" || ManufacturerTextBox.Text == "" || ingredientTemporaryList.Count == 0)
            {
                OkButton.IsEnabled = false;
            }
            else OkButton.IsEnabled = true;
        }

        private void NewIngredientTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text == "" || ManufacturerTextBox.Text == "" || ingredientTemporaryList.Count == 0)
            {
                OkButton.IsEnabled = false;
            }
            else OkButton.IsEnabled = true;

            if (NewIngredientTextBox.Text == "") { AddIngredientButton.IsEnabled = false; }
            else { AddIngredientButton.IsEnabled = true; }
        }

        private Boolean ValidateEntries()
        {
            NameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(NameTextBox))
            {
                return false;
            }

            return true;
        }
    }
}