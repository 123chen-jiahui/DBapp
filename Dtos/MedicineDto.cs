using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class MedicineDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // public int Inventory { get; set; }
        public string Indications { get; set; }
    }
}
