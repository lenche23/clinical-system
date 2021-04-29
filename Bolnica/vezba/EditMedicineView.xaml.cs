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
using System.Windows.Shapes;
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for EditMedicineView.xaml
    /// </summary>
    public partial class EditMedicineView : Window
    {
        public Medicine medicine;

        public DoctorView view;

        public List<Ingridient> Ingredients { get; set; }

        public EditMedicineView(Medicine medicine, DoctorView view)
        {
            InitializeComponent();
            this.DataContext = medicine;
            this.medicine = medicine;
            switch(medicine.Condition)
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
            this.view = view;
            Ingredients = new List<Ingridient>(medicine.ingridient);
            listViewAllergens.ItemsSource = Ingredients;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var Name = NameTB.Text;
            var Manufacturer = ManufacturerTB.Text;
            var Packaging = PackagingTB.Text;
            MedicineCondition Condition;
            switch(ConditionCMB.SelectedIndex)
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
            //var Medicine = new Medicine(Name, Manufacturer, Packaging, medicine.MedicineID, MedicineStatus.approved, Condition);
            //Medicine.Ingridient = new List<Ingridient>(Ingredients);
            medicine.Name = Name;
            medicine.Manufacturer = Manufacturer;
            medicine.Packaging = Packaging;
            medicine.Condition = Condition;
            medicine.Ingridient = new List<Ingridient>(Ingredients);
            view.listViewMedicine.Items.Refresh();
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            if(listViewAllergens.SelectedItems.Count>0)
            {
                Ingridient Allergen = (Ingridient)listViewAllergens.SelectedItem;
                Ingredients.Remove(Allergen);
                listViewAllergens.Items.Refresh();
            }
        }
    }
}
