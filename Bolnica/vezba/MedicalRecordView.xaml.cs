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
using System.Windows.Shapes;

namespace vezba
{
    /// <summary>
    /// Interaction logic for MedicalRecordView.xaml
    /// </summary>
    public partial class MedicalRecordView : Window
    {
        public MedicalRecordView(Patient Patient)
        {
            InitializeComponent();
            this.DataContext = Patient;
            if (Patient.MedicalRecord != null) {
                listViewAlergens.ItemsSource = Patient.MedicalRecord.Allergen;
                listViewAnamnesis.ItemsSource = Patient.MedicalRecord.Anamnesis;
                listViewPrescriptions.ItemsSource = Patient.MedicalRecord.Prescription;
                listViewReferralLetters.ItemsSource = Patient.MedicalRecord.ReferralLetter;
            }
        }

    }
}
