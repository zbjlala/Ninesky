using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.IBLL
{
    /// <summary>
    /// 附件服务接口
    /// </summary>
    public interface interfaceAttachmentService:interfaceBaseService<Attachment>
    {
        /// <summary>
        /// 查询附件列表
        /// </summary>
        /// <param name="modelID">公共模型ID</param>
        /// <param name="owner">所有者</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IQueryable<Attachment> FindList(Nullable<int> modelID,string owner,string type);
        /// <summary>
        /// 查询附件列表
        /// </summary>
        /// <param name="modelID">公共模型ID</param>
        /// <param name="owner">所有者</param>
        /// <param name="type">类型</param>
        /// <param name="withModelIDNull">包含ModelID为Null的</param>
        /// <returns></returns>
        IQueryable<Attachment> FindList(int modelID,string owner,string type,bool withModelIDNull);

    }
}
