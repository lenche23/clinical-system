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

        public List<Medicine> AllMedicine { get; set; }

        private MedicineStorage MedStorage;

        public List<Ingridient> Ingredients { get; set; }

        private DoctorView dw;

        public EditMedicinePage(Medicine medicine, DoctorView dw)
        {
            InitializeComponent();
            DataContext = this;
            Medicine = medicine;
            this.dw = dw;
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
            Ingredients = new List<Ingridient>(medicine.ingridient);
            listViewAllergens.ItemsSource = Ingredients;
            MedStorage = new MedicineStorage();
            AllMedicine = MedStorage.GetApproved();
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
            Medicine.Name = Name;
            Medicine.Manufacturer = Manufacturer;
            Medicine.Packaging = Packaging;
            Medicine.Condition = Condition;
            Medicine.Ingridient = new List<Ingridient>(Ingredients);
            MedStorage.Update(Medicine);
            dw.Main.Content = new ViewMedicinePage(Medicine, dw);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new ViewMedicinePage(Medicine, dw);
        }

        private void PlusButtonClick(object sender, RoutedEventArgs e)
        {
            var Name = TbAllergen.Text;
            var Allergen = new Ingridient(Name);
            Ingredients.Add(Allergen);
            listViewAllergens.Items.Refresh();
            TbAllergen.Clear();
        }

        private void MinusButtonClick(object sender, RoutedEventArgs e)
        {
            if (listViewAllergens.SelectedItems.Count > 0)
            {
                Ingridient Allergen = (Ingridient)listViewAllergens.SelectedItem;
                Ingredients.Remove(Allergen);
                listViewAllergens.Items.Refresh();
            }
        }
    }
}
