using Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba
{
    /// <summary>
    /// Interaction logic for EditMedicinePage.xaml
    /// </summary>
    public partial class EditMedicinePage : Page
    {
        public Medicine Medicine { get; set; }

        public List<Medicine> MedicineForReplacement { get; set; }

        private MedicineStorage MedStorage;

        public List<Ingridient> Ingredients { get; set; }

        private DoctorView dw;

        private MedicinePageView mpw;

        public EditMedicinePage(Medicine medicine, DoctorView dw, MedicinePageView mpw)
        {
            InitializeComponent();
            DataContext = this;
            Medicine = medicine;
            this.dw = dw;
            this.mpw = mpw;
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

            MedStorage = new MedicineStorage();
            MedicineForReplacement = MedStorage.GetApproved();
            foreach (var replacement in MedicineForReplacement)
            {
                if (replacement.MedicineID == Medicine.MedicineID)
                {
                    MedicineForReplacement.Remove(replacement);
                    break;
                }
            }

            if (medicine.ReplacementMedicine != null)
                ReplacementMedicineCB.SelectedValue = medicine.ReplacementMedicine.MedicineID;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var Name = NameTB.Text;
            var Manufacturer = ManufacturerTB.Text;
            var Packaging = PackagingTB.Text;
            MedicineCondition Condition;
            switch (ConditionCMB.SelectedIndex)
            {
                case 0:
                    Condition = MedicineCondition.pill;
                    break;
                case 1:
                    Condition = MedicineCondition.capsule;
                    break;
                default:
                    Condition = MedicineCondition.syrup;
                    break;
            }

            var ReplacementMedicine = (Medicine)ReplacementMedicineCB.SelectedItem;
            Medicine.Name = Name;
            Medicine.Manufacturer = Manufacturer;
            Medicine.Packaging = Packaging;
            Medicine.Condition = Condition;
            Medicine.Ingridient = new List<Ingridient>(Ingredients);
            Medicine.ReplacementMedicine = ReplacementMedicine;
            MedStorage.Update(Medicine);
            mpw.approvedGrid.Items.Refresh();
            dw.Main.GoBack();
            dw.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }

        private void PlusButtonClick(object sender, RoutedEventArgs e)
        {
            var Name = TbAllergen.Text;
            var Allergen = new Ingridient(Name);
            Ingredients.Add(Allergen);
            ingredientGrid.ItemsSource = null;
            ingredientGrid.ItemsSource = Ingredients;
            TbAllergen.Clear();
        }

        private void MinusButtonClick(object sender, RoutedEventArgs e)
        {
            if (ingredientGrid.SelectedItems.Count > 0)
            {
                Ingridient Allergen = (Ingridient)ingredientGrid.SelectedItem;
                Ingredients.Remove(Allergen);
                ingredientGrid.ItemsSource = null;
                ingredientGrid.ItemsSource = Ingredients;
            }
        }
    }
}
