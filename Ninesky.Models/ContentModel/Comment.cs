using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Models.ArticeModel
{
    /// <summary>
    /// 评论模型
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int ModelID { get; set; }
        /// <summary>
        /// 评论标题
        /// </summary>
        [Display(Name ="评论标题")]
        [StringLength(255,ErrorMessage ="必须少于{0}个字符")]
        public string Tile { get; set; }
    }
}
