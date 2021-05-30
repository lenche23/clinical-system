using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace vezba.ManagerGUI
{
    public partial class MedicineDetailPage : Page
    {
        private Medicine medicine;
        public static ObservableCollection<Ingridient> IngredientList { get; set; }
        private MainManagerWindow mainManagerWindow;
        public MedicineDetailPage(MainManagerWindow mainManagerWindow, Medicine medicine)
        {
            InitializeComponent();
            this.medicine = medicine;
            this.mainManagerWindow = mainManagerWindow;
            idText.Content = idText.Content + " " + medicine.MedicineID;
            nazivText.Content = nazivText.Content + " " + medicine.Name;
            proizvodjacText.Content = proizvodjacText.Content + " " + medicine.Manufacturer;
            pakovanjeText.Content = pakovanjeText.Content + " " + medicine.Packaging;
            if (medicine.Condition == MedicineCondition.capsule)
            {
                oblikText.Content = oblikText.Content + " Kapsula";
            }
            else if (medicine.Condition == MedicineCondition.pill)
            {
                oblikText.Content = oblikText.Content + " Pilula";
            }
            else if (medicine.Condition == MedicineCondition.syrup)
            {
                oblikText.Content = oblikText.Content + " Sirup";
            }

            if (medicine.ReplacementMedicine == null)
            {
                zamenskiText.Content = " ";
            }
            else
            {
                zamenskiText.Content = zamenskiText.Content + " " + medicine.ReplacementMedicine.Name;
            }

            List<Ingridient> ingredientList = medicine.ingridient;
            IngredientList = new ObservableCollection<Ingridient>(ingredientList);
            IngredientsBinding.ItemsSource = IngredientList;
        }
    }
}

