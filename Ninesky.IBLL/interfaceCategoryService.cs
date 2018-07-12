using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.IBLL
{
    /// <summary>
    /// 栏目服务接口
    /// </summary>
    public interface interfaceCategoryService : interfaceBaseService<Category>
    {
        /// <summary>
        /// 获取easyuiTree数据
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        List<Models.EasyuiTreeNodeViewModel> EasyuiTreeData(string model);
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        List<Models.ArticeModel.Category> FindList(string model);
    }
}
