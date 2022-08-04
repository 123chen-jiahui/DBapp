using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("SHOPPINGCART")]
    public class ShoppingCart
    {
        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("PATIENT_ID")]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } // 并不会到数据库中，而是由EF保存，表示外键关系
        public ICollection<LineItem> ShoppingCartItems {get; set;} // 保存商品（药品）列表
    }
}
