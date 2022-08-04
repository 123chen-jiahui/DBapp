using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("MEDICINE")]
    public class Medicine
    {
        [Key]
        [Required]
        [Column("ID")]
        [StringLength(9, MinimumLength = 9)]
        public string Id { get; set; } // 这里用国药准字
        [Required]
        [Column("NAME")]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Column("PRICE", TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        [Column("INVENTORY")]
        public int Inventory { get; set; } // 库存
        [Required]
        [Column("INDICATIONS")]
        [MaxLength(40)]
        public string Indications { get; set; } // 适应症
        [Required]
        [Column("MANUFACTURER")]
        public string Manufacturer { get; set; }
        /*[Required]
        [Column("ENTRY_TIME")]
        public DateTime EntryTime { get; set; } // 入库时间*/
    }
}
