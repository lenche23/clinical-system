using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezba.Service
{
    public class ValidMedicineGenerator
    {
        private List<Medicine> allApprovedMedicine;
        private MedicalRecord medicalRecord;
        public ValidMedicineGenerator(List<Medicine> allApprovedMedicine, MedicalRecord medicalRecord)
        {
            this.allApprovedMedicine = allApprovedMedicine;
            this.medicalRecord = medicalRecord;
        }

        public List<Medicine> GenerateValidMedicineForPatient()
        {
            List<Medicine> validMedicine = new List<Medicine>();
            foreach (var medicine in allApprovedMedicine)
            {
                if (!AllergenMatchFound(medicine))
                    validMedicine.Add(medicine);
            }

            return validMedicine;
        }

        private bool AllergenMatchFound(Medicine medicine)
        {
            var allergenMatchFound = false;
            foreach (var ingredient in medicine.ingridient)
            {
                foreach (var allergen in medicalRecord.Allergen)
                {
                    if (ingredient.Name.Equals(allergen.Name))
                    {
                        allergenMatchFound = true;
                    }
                }
            }

            return allergenMatchFound;
        }
    }
}
