using Hospital.Database;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
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

        public LineItem GetShoppingCartItemByItemId(int lineItemId)
        {
            return _context.LineItems.Where(li => li.Id == lineItemId).FirstOrDefault();
        }

        public void DeleteShoppingCartItem(LineItem lineItem)
        {
            _context.LineItems.Remove(lineItem);
        }

        public async Task<IEnumerable<LineItem>> GetShoppingCartItemsByItemIdListAsync(IEnumerable<int> lineItemIds)
        {
            return await _context.LineItems
                .Where(li => lineItemIds.Contains(li.Id))
                .ToListAsync();
        }

        public void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems)
        {
            _context.LineItems.RemoveRange(lineItems);
        }
    }
}
