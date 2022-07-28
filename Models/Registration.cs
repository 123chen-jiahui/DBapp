using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("REGISTRATIONS")]
    public class Registration
    {
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
        public string StaffId { get; set; }


        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
    }
}
