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

namespace vezba.ManagerGUI

{
    public partial class MedicineAddPage : Page
    {
        public List<Medicine> MedicineList { get; set; }
        public MedicineStorage storage;
        private Medicine newMedicine;
        public static ObservableCollection<Ingridient> IngredientList { get; set; }
        public List<Ingridient> ingredientTemporaryList { get; set; }

        public MedicineAddPage()
        {
            InitializeComponent();
            this.DataContext = this;
            MedicineStorage ms = new MedicineStorage();
            List<Medicine> medicineList = ms.GetAll();
            comboReplacementMedicine.ItemsSource = medicineList;
            List<string> condition = new List<string> { "Kapsula", "Pilula", "Sirup" };
            comboCondition.ItemsSource = condition;
            newMedicine = new Medicine("Naziv", "Naziv", "Naziv", 0, MedicineCondition.pill);
            ingredientTemporaryList = new List<Ingridient>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ingredientName = NoviSastojak.Text;
            var newIngredient = new Ingridient(ingredientName);

            ingredientTemporaryList.Add(newIngredient);
            IngredientList = new ObservableCollection<Ingridient>(ingredientTemporaryList);
            IngredientsBinding.ItemsSource = IngredientList;
            IngredientsBinding.Items.Refresh();

        }

        private void Odobravanje_Button_Click(object sender, RoutedEventArgs e)
        {
            var Name = nazivTB.Text;
            var Manufacturer = proizvodjacTB.Text;
            var Packaging = pakovanjeTB.Text;
            var Condition = MedicineCondition.pill;

            if (comboCondition.SelectedIndex == 1)
            {
                Condition = MedicineCondition.pill;
            }

            else if (comboCondition.SelectedIndex == 0)
            {
                Condition = MedicineCondition.capsule;
            }

            else if (comboCondition.SelectedIndex == 2)
            {
                Condition = MedicineCondition.syrup;
            }

            Medicine replacementMedicine = (Medicine)comboReplacementMedicine.SelectedItem;

            MedicineStorage storage = new MedicineStorage();
            int MedicineID = storage.GenerateNextId();

            newMedicine = new Medicine(Name, Manufacturer, Packaging, MedicineID, Condition) { ReplacementMedicine = replacementMedicine };

            MedicinePage.MedicineList.Add(newMedicine);
            newMedicine.ingridient = ingredientTemporaryList;
            storage.Save(newMedicine);
            NavigationService.GoBack();

        }

        private void Odustani_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
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
    }
}
