using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    // 这是一个多值依赖
    [Table("PURCHASE_LIST_ITEMS")]
    public class PurchaseListItem
    {
        [Key]
        [Required]
        [Column("PURCHASE_LIST_ID")]
        public Guid PurchaseListId { get; set; }
        [Required]
        [Column("ITEM_ID")]
        public string ItemId { get; set; } // 购买物品的Id
        [Required]
        [Column("ITEM_NAME")]
        public string ItemName { get; set; } // 购买物品的名称
        [Required]
        [Column("UNIVALENT", TypeName = "decimal(18, 2)")]
        public decimal Univalent { get; set; } // 购买物品的单价
        [Required]
        [Column("NUMBER")]
        public int Number { get; set; } // 购买物品的数量
        [MaxLength]
        [Column("DESCRIPTION")]
        public string Description { get; set; } // 描述语言，非必须

        [ForeignKey("PurchaseListId")]
        public PurchaseList PurchaseList { get; set; }
    }
}
