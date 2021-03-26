using System;

namespace Model
{
    public class MedicalRecord
    {
        public String HealthInsuranceNumber { get; set; }
        public String MedicalIdNumber { get; set; }

        public System.Collections.Generic.List<Allergen> allergen;

        public System.Collections.Generic.List<Allergen> Allergen
        {
            get
            {
                if (allergen == null)
                    allergen = new System.Collections.Generic.List<Allergen>();
                return allergen;
            }
            set
            {
                RemoveAllAllergen();
                if (value != null)
                {
                    foreach (Allergen oAllergen in value)
                        AddAllergen(oAllergen);
                }
            }
        }

        public void AddAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (this.allergen == null)
                this.allergen = new System.Collections.Generic.List<Allergen>();
            if (!this.allergen.Contains(newAllergen))
                this.allergen.Add(newAllergen);
        }

        public void RemoveAllergen(Allergen oldAllergen)
        {
            if (oldAllergen == null)
                return;
            if (this.allergen != null)
                if (this.allergen.Contains(oldAllergen))
                    this.allergen.Remove(oldAllergen);
        }

        public void RemoveAllAllergen()
        {
            if (allergen != null)
                allergen.Clear();
        }

    }
}