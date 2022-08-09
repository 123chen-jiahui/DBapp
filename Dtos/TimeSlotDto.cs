using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class TimeSlotDto
    {
        public int Id { get; set; } // 自增长
        public int StartTime { get; set; } // 起止时间
        public int EndTime { get; set; } // 结束时间
    }
}
