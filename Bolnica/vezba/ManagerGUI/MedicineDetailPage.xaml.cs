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
            IdLabel.Content = IdLabel.Content + " " + medicine.MedicineID;
            NameLabel.Content = NameLabel.Content + " " + medicine.Name;
            ManufacturerLabel.Content = ManufacturerLabel.Content + " " + medicine.Manufacturer;
            PackagingLabel.Content = PackagingLabel.Content + " " + medicine.Packaging;

            if (medicine.Condition == MedicineCondition.capsule) ConditionLabel.Content = ConditionLabel.Content + " Kapsula";
            else if (medicine.Condition == MedicineCondition.pill) ConditionLabel.Content = ConditionLabel.Content + " Pilula";
            else if (medicine.Condition == MedicineCondition.syrup) ConditionLabel.Content = ConditionLabel.Content + " Sirup";

            if (medicine.ReplacementMedicine == null) ReplacementMedicineLabel.Content = " ";
            else ReplacementMedicineLabel.Content = ReplacementMedicineLabel.Content + " " + medicine.ReplacementMedicine.Name;
            
            List<Ingridient> ingredientList = medicine.ingridient;
            IngredientList = new ObservableCollection<Ingridient>(ingredientList);
            IngredientsBinding.ItemsSource = IngredientList;
        }
    }
}

