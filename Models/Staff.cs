using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("STAFF")]
    public class Staff
    {
        [Key]
        [Column("ID")]
        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string Id { get; set; } // 用户名
        [Column("GLOBAL_ID")]
        [StringLength(18, MinimumLength = 18)]
        [Required]
        public string GlobalId { get; set; }
        [Column("PASSWORD")]
        [StringLength(15, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
        [Column("ROLE")]
        [Required]
        public Role Role { get; set; }
        [Column("NAME")]
        [Required]
        public string Name { get; set; }
        [Column("GENDER")]
        [Required]
        public Gender Gender { get; set; }
        [Column("BIRTHDAY")]
        [Required]
        public DateTime Birthday { get; set; }
        [Column("ADDRESS")]
        [MaxLength(80)]
        [Required]
        public string Address { get; set; }
        [Column("PHONE")]
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Phone { get; set; }

        [Column("DEPARTMENT_NAME")]
        [Required]
        public string DepartmentName { get; set; } // 这是一个外键，注意添加上去

        [ForeignKey("DepartmentName")]
        public Department Department { get; set; }
        
    }
}
