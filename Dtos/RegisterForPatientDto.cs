using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    // 注意枚举类型Role和Gender，在http请求Body中的json应该是以数字而不是字符串发送
    // 换句话说，不要带双引号！！！
    public class RegisterForPatientDto
    {
        [Required]
        public string GlobalId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "密码输入不一致")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; } // 性别信息，前端提供选项，1女2男
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Phone { get; set; }

        // 自定义数据验证，直接套模板即可
        // 个人认为身份证、手机号合法性由前端完成比较好
        // 而密码检查则由后端完成比较好
        // 此外，性别通过前端的选择框来选择男还是女，所以无需验证
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Password.Length < 8 || Password.Length > 15)
            {
                yield return new ValidationResult(
                    "密码长度需要在8-15个字符之间",
                    new[] { "RegisterForPatientDto" }
                );
            }
            /*Regex regex = new Regex(@"^\d{11}$");
            Match match = regex.Match(Phone);
            if (!match.Success)
            {
                yield return new ValidationResult(
                    "手机号输入有误",
                    new[] { "RegisterForPatientDto" }
                );
            }*/
        }
    }
}
