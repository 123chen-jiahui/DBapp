using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class ScheduleForCreationDto
    {
        public int StaffId { get; set; }
        public int[] Day { get; set; } = new int[7];
        public int[] TimeSlotId { get; set; } = new int[7];
        public string[] RoomId { get; set; } = new string[7];
    }
}
