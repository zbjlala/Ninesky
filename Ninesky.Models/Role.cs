using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage ="必填")]
        [StringLength(20,MinimumLength =2,ErrorMessage ="{1}到{0}个字")]
        [Display(Name ="名称")]
        public string Name { get; set; }
        /// <summary>
        /// 用户组类型
        /// 0普通用户，1特权类型，3管理类型
        /// </summary>
        [Required(ErrorMessage ="必填")]
        [Display(Name="用户组类型")]
        public int Type { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Required(ErrorMessage ="必填")]
        [StringLength(50,ErrorMessage ="少于{0}个字")]
        [Display(Name ="说明")]
        public string Description { get; set; }


        public string TypeToString()
        {
            switch(Type)
            {
                case 0:
                    return "普通";
                case 1:
                    return "特权";
                case 2:
                    return "管理";
                default:
                    return "未知";
            }
        }

        public virtual ICollection<UserRoleRelation> UserRoleRelations { get; set; }
    }
}
