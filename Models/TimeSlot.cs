using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("TIME_SLOTS")]
    public class TimeSlot
    {
        [Key]
        [Column("ID")]
        [Required]
        public int Id { get; set; } // 自增长
        [Column("START_TIME")]
        [Required]
        public int StartTime { get; set; } // 起止时间
        [Column("END_TIME")]
        [Required]
        public int EndTime { get; set; } // 结束时间

        public ICollection<Staff> Staff { get; set; }
    }
}
