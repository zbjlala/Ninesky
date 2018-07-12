using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Models.ArticeModel
{
    /// <summary>
    /// 内容公共模型
    /// </summary>
    public class CommonModel
    {
        [Key]
        public int ModelID { get; set; }
        /// <summary>
        /// 栏目ID
        /// </summary>
        [Display(Name ="栏目ID")]
        [Required(ErrorMessage ="必填")]
        public int CategoryID { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name ="标题")]
        [Required(ErrorMessage ="必填")]
        [StringLength(255,ErrorMessage ="必须少于{0}个字符")]
        public string Title { get; set; }
        /// <summary>
        /// 录入者
        /// </summary>
        [Display(Name ="录入者")]
        [StringLength(50,ErrorMessage ="必须少于{0}个字符")]
        public string Inputer { get; set; }
        /// <summary>
        /// 点击
        /// </summary>
        [Display(Name = "点击")]
        public int Hits { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        [Display(Name ="发布日期")]
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// -3删除，-2退稿，-1草稿，0未审核，9正常
        [Display(Name ="状态")]
        public int Status { get; set; }
        /// <summary>
        /// 首页图片
        /// </summary>
        [Display(Name ="首页图片")]
        [StringLength(255,ErrorMessage ="必须少于{0}个字符")]
        public string DefaultPicUrl { get; set; }
        /// <summary>
        /// 文章
        /// </summary>
        public virtual Article Article { get; set; }
        /// <summary>
        /// 咨询
        /// </summary>
        public virtual Consultation Consultation { get; set; }
        /// <summary>
        /// 栏目
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>
        public virtual ICollection<Attachment> Attachment { get; set; }
        /// <summary>
        /// 状态列表
        /// </summary>
        public static Dictionary<int,string> StatusList
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    { -3,"删除"},
                    { -2,"退稿"},
                    { -1,"草稿"},
                    { 0,"未审核"},
                    { 9,"正常"},
                    { 20,"未回复"},
                    { 29,"已回复"}
                };
            }
        }

    }
}
