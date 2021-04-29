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
    /// Interaction logic for RevisionView.xaml
    /// </summary>
    public partial class RevisionView : Window
    {
        public Medicine medicine;

        public RevisionView(Medicine medicine)
        {
            InitializeComponent();
            this.medicine = medicine;
            this.DataContext = medicine;
        }

        private void ApproveClick(object sender, RoutedEventArgs e)
        {
            medicine.Status = MedicineStatus.approved;
            MedicinePageView.MedicineToApprove.Remove(medicine);
            MedicinePageView.ApprovedMedicine.Add(medicine);
            this.Close();
        }

        private void Decline(object sender, RoutedEventArgs e)
        {
            medicine.Status = MedicineStatus.declined;
            MedicinePageView.MedicineToApprove.Remove(medicine);
            this.Close();
        }
    }
}
