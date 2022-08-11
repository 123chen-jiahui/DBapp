﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("STAFF_TIMESLOT")]
    public class Staff_TimeSlot
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
    }
}