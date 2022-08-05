using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("PATIENTS")]
    public class Patient
    {
        [Key]
        [Column("ID")]
        [Required]
        public int Id { get; set; } // 病人注册时自动生成，固定长度，11位，自动按照顺序编码（会有seq来实现），当做登录时的用户名使用
        [Column("GLOBAL_ID")]
        [StringLength(18, MinimumLength = 18)] // 数据验证不在此进行，而是在Dto中
        [Required]
        public string GlobalId { get; set; } // 表示注册用的身份证号，固定长度，18位
        [Column("WARD_ID")]
        public string WardId { get; set; } // 表示病床号，可为空
        [Column("PASSWORD")]
        [StringLength(15, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
        [Column("NAME")]
        [Required]
        public string Name { get; set; }
        [Column("GENDER")]
        [Required]
        public Gender Gender { get; set; }
        [Column("BIRTHDAY")]
        [Required]
        public DateTime Birthday { get; set; }
        [Column("PHONE")]
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Phone { get; set; }
        // 应该还需要加入病例

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>(); 
        public ShoppingCart ShoppingCart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
