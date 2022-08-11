using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class ScheduleOfOneDayForCreationDto
    {
        public int StaffId { get; set; }
        public int Day { get; set; }
        public int TimeSlotId { get; set; }
        public int Capacity { get; set; } // 根据工作时间自动生成
    }
}
