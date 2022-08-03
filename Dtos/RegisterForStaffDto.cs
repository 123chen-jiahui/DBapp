using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class RegisterForStaffDto
    {
        // 注意枚举类型Role和Gender，在http请求Body中的json应该是以数字而不是字符串发送
        // 换句话说，不要带双引号！！！

        // 还没有进行数据验证，不是主要矛盾，所以现在没写，后续会补上
        /*[Required]
        public string Id { get; set; }*/
        [Required]
        public string GlobalId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "密码输入不一致")]
        public string ConfirmPassword { get; set; }
        [Required]
        public Role Role { get; set; } // Role信息，前端提供选项选择，目前有Admin、Doctor和MedicineToken三种
        [Required]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; } // 性别信息，前端提供选项，1女2男
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int DepartmentId { get; set; } // 科室信息，前端提供选项列表
    }
}
