using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.IBLL
{
    /// <summary>
    /// 公共模型服务类
    /// </summary>
    public interface interfaceCommentModelService : interfaceBaseService<CommonModel>
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="entitys">数据实体集</param>
        /// <param name="roderCode">排序代码(默认ID降序)</param>
        /// <returns></returns>
        IQueryable<CommonModel> Order(IQueryable<CommonModel>entitys,int orderCode);

        IQueryable<CommonModel> FindPageList(out int totalRecord, int pageindex, int pageSize, string model, string title, int categoryID, string inputer, Nullable<DateTime> formDate, Nullable<DateTime> toDate, int orderCode);
    }
}
