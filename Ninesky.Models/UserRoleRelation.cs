using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class UserRoleRelation
    {
        [Key]
        public int RelationID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required()]
        public int UserID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required()]
        public int RoleID { get; set; }


        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
