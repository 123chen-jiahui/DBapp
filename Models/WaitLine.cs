using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("WAITLINES")]
    public class WaitLine
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }
        [Column("STAFF_ID")]
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
        [Column("PATIENT_ID")]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [Column("DAY")]
        public int Day { get; set; }
        [Column("ORDER")]
        public int Order { get; set; }
    }
}
