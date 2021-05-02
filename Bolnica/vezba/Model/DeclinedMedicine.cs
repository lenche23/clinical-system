using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezba.Model
{
    class DeclinedMedicine
    {
        private Medicine Medicine { get; set; }
        private String Description { get; set; }

        public DeclinedMedicine(Medicine Medicine, String Description)
        {
            this.Medicine = Medicine;
            this.Description = Description;
        }
    }
}
