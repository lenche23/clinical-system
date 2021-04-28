using System;

namespace Model
{
   public class Medicine
   {

        public Medicine(String Name, String Manufacturer, String Packaging, int MedicineID, MedicineStatus Status, MedicineCondition Condition)
        {
            this.Name = Name;
            this.Manufacturer = Manufacturer;
            this.Packaging = Packaging;
            this.MedicineID = MedicineID;
            this.Status = Status;
            this.Condition = Condition;
        }

        public String Name { get; set; }

        //******** Dodati deo - ako nesto ne radi
        public String Manufacturer { get; set; }
        public String Packaging { get; set; }
        public int MedicineID { get; set; }
        public MedicineStatus Status { get; set; }
        public MedicineCondition Condition { get; set; }

        public Medicine ReplacementMedicine { get; set; }
        //********


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
