using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public int PatientId { get; set; }
        // LineItem需要做数据映射
        public ICollection<LineItemDto> ShoppingCartItems { get; set; } // 保存商品（药品）列表，注意，它会自动被映射
    }
}
