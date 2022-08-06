using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{   
    public class GuahaoDto
    {
        // Id应该是数据库自动生成
        // [Required]
        // public Guid Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public decimal fee { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int StaffId { get; set; }
    }
}
