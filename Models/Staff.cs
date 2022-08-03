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
        // 我觉得还需要加一个员工号来唯一表示员工
        // 因为虽然用户名能唯一表示员工，但是总觉得很奇怪，不够统一
        [Key]
        [Column("ID")]
        [Required]
        public int Id { get; set; } // 员工号

        /*// [Key]
        [Column("USERNAME")]
        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string UserName { get; set; } // 用户名*/
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

        [Column("DEPARTMENT_ID")]
        [Required]
        public int DepartmentId { get; set; } // 这是一个外键，注意添加上去

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public ICollection<Registration> Registrations { get; set; }/* = new List<Registration>();*/ 
        public ICollection<PurchaseList> PurchaseLists { get; set; }
    }
}
