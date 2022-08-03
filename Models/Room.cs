using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("ROOMS")]
    public class Room
    {
        [Key]
        [Column("ID")]
        [Required]
        public string Id { get; set; } // 房间编号
        [Column("BUILDING")]
        [MaxLength(20)]
        [Required]
        public string Building { get; set; } // 所在楼宇
        [Column("ROOMTYPE")]
        [Required]
        public RoomType RoomType { get; set; } // 房间用途


        public ICollection<MedicalEquipment> MedicalEquipments { get; set; }
    }
}
