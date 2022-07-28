using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("WARDS")]
    public class Ward
    {
        [Key]
        [Column("WARD_ID")]
        [StringLength(4, MinimumLength = 4)]
        public string WardId { get; set; } // 1001这样的数字
        [Column("DEPARTMENT_ID")]
        [Required]
        public int DepartmentId { get; set; }
        [Column("BUILDING")]
        [MaxLength(20)]
        [Required]
        public string Building { get; set; }
        [Column("TYPE")]
        [Required]
        public WardType Type { get; set; }
        [Column("STARTNUM")]
        [Required]
        public int StartNum { get; set; }
        [Column("ENDNUM")]
        [Required]
        public int EndNum { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
