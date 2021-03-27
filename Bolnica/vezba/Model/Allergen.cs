using System;

namespace Model
{
    public class Allergen
    {
        public String Name { get; set; }

        public Allergen(string name) {
            this.Name = name;
        }
    }
}