using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("PURCHASE_LISTS")]
    public class PurchaseList
    {
        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("DATE")]
        public DateTime Date { get; set; }
        [Required]
        [Column("COST", TypeName = "decimal(18, 2)")]
        public decimal Cost { get; set; }
        [Required]
        [Column("STAFF_ID")]
        public int StaffId { get; set; } // 采购负责人


        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        public ICollection<PurchaseListItem> PurchaseListItems { get; set; } = new List<PurchaseListItem>();
    }
}
