using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户配置
    /// </summary>
    public class UserConfig
    {
        [Key]
        public int ConfigID { get; set; }
        /// <summary>
        /// 启用注册
        /// </summary>
        [Display(Name ="启用注册")]
        [Required(ErrorMessage ="必填")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 禁止使用的用户名
        /// 用户名之间用“|”分隔
        /// </summary>
        [Display(Name ="禁止使用的用户名")]
        public string ProhibitUserName { get; set; }
        /// <summary>
        /// 启用管理员验证
        /// </summary>
        [Display(Name ="启用管理员验证")]
        [Required(ErrorMessage ="必填")]
        public bool EnableAdminVerify { get; set; }
        [Display(Name ="启用邮件验证")]
        [Required(ErrorMessage ="必填")]
        public bool EnableEmailVerify { get; set; }
        [Display(Name ="默认用户组Id")]
        [Required(ErrorMessage ="必填")]
        public int DefaultGroupId { get; set; }
    }
}
