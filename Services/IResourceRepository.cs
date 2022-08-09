using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IResourceRepository
    {
        Task<Medicine> GetMedicineAsync(string medicineId);
        Task<IEnumerable<Medicine>> GetMedicinesAsync(string keyWord);
        void AddShoppingCartItem(LineItem lineItem);
        Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId);
        void DeleteShoppingCartItem(LineItem lineItem);
        Task<IEnumerable<LineItem>> GetShoppingCartItemsByItemIdListAsync(IEnumerable<int> lineItemIds);
        void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems);
        Task<ICollection<LineItem>> GetShoppingCartItemsByShoppingCartIdAsync(Guid shoppingCartId);
        Task<Order> GetOrderByOrderIdAsync(Guid orderId);
    }
}
