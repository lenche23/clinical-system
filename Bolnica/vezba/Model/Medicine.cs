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

        public System.Collections.Generic.List<Ingridient> ingridient;

        public System.Collections.Generic.List<Ingridient> Ingridient
        {
            get
            {
                if (ingridient == null)
                    ingridient = new System.Collections.Generic.List<Ingridient>();
                return ingridient;
            }
            set
            {
                RemoveAllIngridient();
                if (value != null)
                {
                    foreach (Ingridient oIngridient in value)
                        AddIngridient(oIngridient);
                }
            }
        }

        public void AddIngridient(Ingridient newIngridient)
        {
            if (newIngridient == null)
                return;
            if (this.ingridient == null)
                ingridient = new System.Collections.Generic.List<Ingridient>();
            if (!this.ingridient.Contains(newIngridient))
                this.ingridient.Add(newIngridient);
        }

        public void RemoveIngridient(Ingridient oldIngridient)
        {
            if (oldIngridient == null)
                return;
            if (this.ingridient != null)
                if (this.ingridient.Contains(oldIngridient))
                    this.ingridient.Remove(oldIngridient);
        }

        public void RemoveAllIngridient()
        {
            if (ingridient != null)
                ingridient.Clear();
        }

    }

}
