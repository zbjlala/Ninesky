using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNinesky.Areas.Member.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage ="必填")]
        [StringLength(20,MinimumLength =4,ErrorMessage ="{2}到{1}个字符")]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage ="必填")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="{2}到{1}个字符")]
        [Display(Name ="密码")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Display(Name ="记住我")]
        public bool RememberMe { get; set; }
    }
}