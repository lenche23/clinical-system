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
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for EditMedicineWindow.xaml
    /// </summary>
    public partial class EditMedicineWindow : Window
    {
        private Medicine selected;
        private MedicineStorage medicineStorage;
        private AddMedicineWindow addMedicineWindow;
        public static ObservableCollection<Ingridient> IngredientList { get; set; }
        public List<Ingridient> ingredientTemporaryList { get; set; }
        public EditMedicineWindow(Medicine medicine, AddMedicineWindow addMedicineWindow)
        {
            InitializeComponent();
            selected = medicine;
            this.DataContext = selected;
            this.addMedicineWindow = addMedicineWindow;

            medicineStorage = new MedicineStorage();
            List<Medicine> medicineList = medicineStorage.GetAll();
            List<Medicine> temporary = new List<Medicine>();
            for (int i = 0; i < medicineList.Count; i++)
            {
                if (medicineList[i].Status == MedicineStatus.approved && medicineList[i].Name != medicine.Name)
                {
                    temporary.Add(medicineList[i]);
                }
            }

            comboReplacementMedicine.ItemsSource = temporary;
            List<string> condition = new List<string> { "Kapsula", "Pilula", "Sirup" };
            comboCondition.ItemsSource = condition;

            ingredientTemporaryList = medicine.ingridient;

            nazivTB.Text = medicine.Name;
            proizvodjacTB.Text = medicine.Manufacturer;
            pakovanjeTB.Text = medicine.Packaging;

            if (medicine.Condition == MedicineCondition.capsule)
            {
                comboCondition.SelectedIndex = 0;
            }
            else if (medicine.Condition == MedicineCondition.pill)
            {
                comboCondition.SelectedIndex = 1;
            }
            else if (medicine.Condition == MedicineCondition.syrup)
            {
                comboCondition.SelectedIndex = 2;
            }

            if (medicine.ReplacementMedicine == null)
            {

            }
            else
            {
                for (int i = 0; i < temporary.Count; i++)
                {
                    if (temporary[i].Name == medicine.ReplacementMedicine.Name)
                    {
                        comboReplacementMedicine.SelectedIndex = i;
                    }
                }
            }

            List<Ingridient> ingredientList = medicine.ingridient;
            IngredientList = new ObservableCollection<Ingridient>(ingredientList);
            IngredientsBinding.ItemsSource = IngredientList;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            selected.Name = nazivTB.Text;
            selected.Manufacturer = proizvodjacTB.Text;
            selected.Packaging = pakovanjeTB.Text;

            if (comboCondition.SelectedIndex == 1)
            {
                selected.Condition = MedicineCondition.pill;
            }

            else if (comboCondition.SelectedIndex == 0)
            {
                selected.Condition = MedicineCondition.capsule;
            }

            else if (comboCondition.SelectedIndex == 2)
            {
                selected.Condition = MedicineCondition.syrup;
            }

            selected.ReplacementMedicine = (Medicine)comboReplacementMedicine.SelectedItem;

            
            medicineStorage.Update(selected);
            addMedicineWindow.MedicineBinding.Items.Refresh();

            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ingredientName = NoviSastojak.Text;
            var newIngredient = new Ingridient(ingredientName);

            ingredientTemporaryList.Add(newIngredient);
            IngredientList = new ObservableCollection<Ingridient>(ingredientTemporaryList);
            IngredientsBinding.ItemsSource = IngredientList;
            IngredientsBinding.Items.Refresh();

        }
    }
}
