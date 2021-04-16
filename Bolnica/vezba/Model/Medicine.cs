// File:    Medicine.cs
// Author:  graho
// Created: 16 April 2021 17:26:55
// Purpose: Definition of Class Medicine

using System;

namespace Model
{
   public class Medicine
   {
        public String Name { get; set; }

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
                allergen = new System.Collections.Generic.List<Allergen>();
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
