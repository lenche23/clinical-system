using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for EditMedicinePage.xaml
    /// </summary>
    public partial class EditMedicinePage : Page
    {
        public Medicine Medicine { get; set; }

        public List<Medicine> MedicineForReplacement { get; set; }

        private readonly MedicineService _medicineService;

        public List<Ingridient> Ingredients { get; set; }

        private readonly DoctorView _doctorView;

        private readonly MedicinePageView _medicinePageView;

        public EditMedicinePage(Medicine medicine, DoctorView doctorView, MedicinePageView medicinePageView)
        {
            InitializeComponent();
            DataContext = this;
            Medicine = medicine;
            _doctorView = doctorView;
            _medicinePageView = medicinePageView;
            switch (medicine.Condition)
            {
                case MedicineCondition.pill:
                    ConditionCMB.SelectedIndex = 0;
                    break;
                case MedicineCondition.capsule:
                    ConditionCMB.SelectedIndex = 1;
                    break;
                case MedicineCondition.syrup:
                    ConditionCMB.SelectedIndex = 2;
                    break;
            }
            Ingredients = new List<Ingridient>(medicine.Ingridient);

            _medicineService = new MedicineService();
            MedicineForReplacement = _medicineService.GetPossibleReplacements(Medicine);

            if (medicine.ReplacementMedicine != null)
                ReplacementMedicineCB.SelectedValue = medicine.ReplacementMedicine.MedicineID;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            EditMedicine();
            _medicineService.UpdateMedicine(Medicine);
            _medicinePageView.ApprovedMedicineListView.Items.Refresh();
            _doctorView.Main.GoBack();
            _doctorView.Main.GoBack();
        }

        private void EditMedicine()
        {
            var medicineName = NameTB.Text;
            var manufacturer = ManufacturerTB.Text;
            var packaging = PackagingTB.Text;
            MedicineCondition condition;
            switch (ConditionCMB.SelectedIndex)
            {
                case 0:
                    condition = MedicineCondition.pill;
                    break;
                case 1:
                    condition = MedicineCondition.capsule;
                    break;
                default:
                    condition = MedicineCondition.syrup;
                    break;
            }

            var replacementMedicine = (Medicine) ReplacementMedicineCB.SelectedItem;
            Medicine.Name = medicineName;
            Medicine.Manufacturer = manufacturer;
            Medicine.Packaging = packaging;
            Medicine.Condition = condition;
            Medicine.Ingridient = new List<Ingridient>(Ingredients);
            Medicine.ReplacementMedicine = replacementMedicine;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }

        private void PlusButtonClick(object sender, RoutedEventArgs e)
        {
            var ingredientName = TbAllergen.Text;
            var ingredient = new Ingridient(ingredientName);
            Ingredients.Add(ingredient);
            ingredientGrid.ItemsSource = null;
            ingredientGrid.ItemsSource = Ingredients;
            TbAllergen.Clear();
        }

        private void MinusButtonClick(object sender, RoutedEventArgs e)
        {
            if (ingredientGrid.SelectedItems.Count > 0)
            {
                var ingredient = (Ingridient)ingredientGrid.SelectedItem;
                Ingredients.Remove(ingredient);
                ingredientGrid.ItemsSource = null;
                ingredientGrid.ItemsSource = Ingredients;
            }
        }
    }
}
