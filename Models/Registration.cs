using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    // 注意该model的名称可能会产生歧义，这里的registration表示挂号，而不是zhuce
    [Table("REGISTRATIONS")]
    public class Registration
    {
        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("TIME")]
        public DateTime Time { get; set; }
        [Required]
        [Column("FEE", TypeName = "decimal(18, 2)")]
        public decimal fee { get; set; }
        [Required]
        [Column("PATIENT_ID")]
        public int PatientId { get; set; }
        [Required]
        [Column("STAFF_ID")]
        public int StaffId { get; set; }
        // 我觉得还需要加入地点，否则病人找不到看病位置
        // 当然地点应该是由医生的信息自动生成的


        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
    }
}
