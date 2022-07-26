using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
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
        public Gender Gender { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Phone { get; set; }

        // 自定义数据验证，直接套模板即可
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Password.Length < 8 || Password.Length > 15)
            {
                yield return new ValidationResult(
                    "密码长度需要在8-15个字符之间",
                    new[] { "RegisterForPatientDto" }
                );
            }
            // 性别由前端验证，通过选择框来选择男还是女

            Regex regex = new Regex(@"^\d{11}$");
            Match match = regex.Match(Phone);
            if (!match.Success)
            {
                yield return new ValidationResult(
                    "手机号输入有误",
                    new[] { "RegisterForPatientDto" }
                );
            }
        }
    }
}
