using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class TimeSlotForCreationDto
    {
        public int StartTime { get; set; } // 起止时间

        public int EndTime { get; set; } // 结束时间


        // 需要对这两个时间进行合法性检查
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1. StartTime和EndTime必须在0-23之间
            if (StartTime < 0 || StartTime > 23 || EndTime < 0 || EndTime > 23)
            {
                yield return new ValidationResult(
                    "输入的时间非法",
                    new[] { "TimeSlotDto" }
                );
            }

            // 2. StartTime必须小于EndTime
            if (StartTime >= EndTime)
            {
                yield return new ValidationResult(
                    "StartTime必须小于EndTime",
                    new[] { "TimeSlotDto" }
                );
            }
        }
    }
}
