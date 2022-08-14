using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("SCHEDULES")]
    public class Schedule
    {
        // [Key]
        [Column("STAFF_ID")]
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
        // [Key]
        [Column("DAY")]
        public int Day { get; set; }

        [Column("TIMESLOT_ID")]
        public int TimeSlotId { get; set; }
        [ForeignKey("TimeSlotId")]
        public TimeSlot TimeSlot { get; set; }

        [Column("ROOM_ID")]
        public string RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [Column("CAPACITY")]
        public int Capacity { get; set; } // 根据工作时间自动生成

        [Column("TOTAL")]
        public int Total { get; set; }
    }
}
