using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("MEDICAL_EQUIPMENTS")]
    public class MedicalEquipment
    {
        [Key]
        [Column("ID")]
        [Required]
        public Guid Id { get; set; } // 设备编号，Guid
        [Column("NAME")]
        [Required]
        public string Name { get; set; } // 设备名称
        [Column("PRODUCER")]
        [Required]
        public string Producer { get; set; } // 生产商
        [Column("START_USE_TIME")]
        public DateTime? StartUseTime { get; set; } // 启用时间，可为空
        [Required]
        [Column("ROOM_ID")]
        public string RoomId { get; set; } // 外键

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
