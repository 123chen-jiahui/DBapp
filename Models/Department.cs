using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("DEPARTMENTS")]
    public class Department
    {
        [Key]
        [Required]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAME")]
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        [Column("BUILDING")]
        [MaxLength(20)]
        [Required]
        public string Building { get; set; }
        [Column("PHONE")]
        [StringLength(11, MinimumLength = 11)]
        [Required]
        public string Phone { get; set; }

        public ICollection<Staff> Staff { get; set; } = new List<Staff>();
        public ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
}
