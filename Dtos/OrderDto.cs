using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int PatientId { get; set; }
        public ICollection<LineItemDto> OrderItems { get; set; } // 保存商品（药品）列表
        public string State { get; set; }
        public DateTime CreateDateUTC { get; set; } // 支付时间
        public string TransactionMetadata { get; set; } // 第三方支付信息
    }
}
