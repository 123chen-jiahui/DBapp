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

        public async Task<Medicine> GetMedicineAsync(string medicineId)
        {
            return await _context.Medicine.Where(m => m.Id == medicineId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Medicine>> GetMedicinesAsync(string keyWord)
        {
            return await _context.Medicine.Where(m => m.Name.Contains(keyWord)).ToListAsync();
        }

        public void AddShoppingCartItem(LineItem lineItem)
        {
            _context.LineItems.Add(lineItem);
        }

        public async Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId)
        {
            return await _context.LineItems.Where(li => li.Id == lineItemId).FirstOrDefaultAsync();
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

        public async Task<ICollection<LineItem>> GetShoppingCartItemsByShoppingCartIdAsync(Guid shoppingCartId)
        {
            return await _context.LineItems.Where(li => li.ShoppingCartId == shoppingCartId).ToListAsync();
        }

        public async Task<Order> GetOrderByOrderIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Medicine)
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
        }
    }
}
