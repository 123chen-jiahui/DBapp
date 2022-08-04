using Hospital.Database;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AppDbContext _context;

        public ResourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public Medicine GetMedicine(string medicineId)
        {
            return _context.Medicine.Where(m => m.Id == medicineId).FirstOrDefault();
        }

        public void AddShoppingCartItem(LineItem lineItem)
        {
            _context.LineItems.Add(lineItem);
        }
    }
}
