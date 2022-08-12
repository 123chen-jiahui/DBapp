using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class RegistrationDto
    {
        public Guid Id { get; set; }
        public int StaffId { get; set; }
        public int Day { get; set; }
        public DateTime Time { get; set; }
        public string RoomId { get; set; }
        public int Order { get; set; }
        public int fee { get; set; }
        public string State { get; set; }
        public DateTime CreateDateLocal { get; set; }
    }
}
