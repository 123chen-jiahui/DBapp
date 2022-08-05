using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("LINEITEM")]
    public class LineItem
    {
        [Key]
        [Column("ID")]
        // 这个属性需要自增，在AppDbContext.cs中添加SEQ
        public int Id { get; set; }
        // 三个外键关系
        // 1.使用商品（药品）Id连接产品信息
        [Required]
        [Column("MEDICINE_ID")]
        public string MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
        // 2.
        // [Required]
        [Column("SHOPPINGCART_ID")]
        public Guid? ShoppingCartId { get; set; } // 可以为空，因为LineItem会在ShoppingCart和Order之间转移
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
        // 3.
        [Column("ORDER_ID")]
        public Guid? OrderId { get; set; } // 可以为空，因为LineItem会在ShoppingCart和Order之间转移
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [Required]
        [Column("PRICE", TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

    }
}
