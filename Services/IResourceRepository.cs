using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IResourceRepository
    {
        Medicine GetMedicine(string medicineId);
        void AddShoppingCartItem(LineItem lineItem);
        LineItem GetShoppingCartItemByItemId(int lineItemId);
        void DeleteShoppingCartItem(LineItem lineItem);
        Task<IEnumerable<LineItem>> GetShoppingCartItemsByItemIdListAsync(IEnumerable<int> lineItemIds);
        void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems);
    }
}
