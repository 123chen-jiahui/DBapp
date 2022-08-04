using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class LineItemDto
    {
        public int Id { get; set; }
        public string MedicineId { get; set; }
        public MedicineDto Medicine { get; set; }
        public Guid? ShoppingCartId { get; set; }
        public decimal Price { get; set; }

    }
}
