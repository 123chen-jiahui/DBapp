using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class LoginForStaffDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }

        // 这里不需要进行数据验证，因为这是登录，等一下数据库会进行验证
    }
}
