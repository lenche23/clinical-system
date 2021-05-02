using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezba.Model
{
    public class DeclinedMedicine
    {
        public int DeclinedMedicineID { get; set; }
        public Medicine Medicine { get; set; }
        public String Description { get; set; }
        public Boolean IsDeleted { get; set; }
        public DeclinedMedicine(int DeclinedMedicineID, Medicine Medicine, String Description)
        {
            this.DeclinedMedicineID = DeclinedMedicineID;
            this.Medicine = Medicine;
            this.Description = Description;
            this.IsDeleted = false;
        }
    }
}
