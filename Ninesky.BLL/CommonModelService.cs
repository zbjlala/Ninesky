using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.BLL
{
    /// <summary>
    /// 公共服务
    /// </summary>
    public class CommonModelService : BaseService<CommonModel>, interfaceCommentModelService
    {
        public CommonModelService() : base(RepositoryFactory.CommonModelRepository)
        {

        }

        public IQueryable<CommonModel> FindPageList(out int totalRecord, int pageindex, int pageSize, string model, string title, int categoryID, string inputer, DateTime? formDate, DateTime? toDate, int orderCode)
        {
            //获取实体列表
            IQueryable<CommonModel> _commonModels = CurrentRepository.Entities;
            if(model == null || model!="All")
            {
              _commonModels = _commonModels.Where(cm =>cm.Model == model);
            }
            if(!string.IsNullOrEmpty(title))
            {
                _commonModels = _commonModels.Where(cm =>cm.Title.Contains(title));
            }
            if(categoryID > 0)
            {
                _commonModels = _commonModels.Where(cm => cm.CategoryID == categoryID);
            }
            if(!string.IsNullOrEmpty(inputer))
            {
                _commonModels = _commonModels.Where(cm =>cm.Inputer.Contains(inputer));
            }
            if(formDate!=null)
            {
                _commonModels = _commonModels.Where(cm => cm.ReleaseDate >= formDate);
            }
            if(toDate != null)
            {
                _commonModels = _commonModels.Where(cm =>cm.ReleaseDate <= toDate);
            }
            _commonModels = Order(_commonModels,orderCode);
            totalRecord = _commonModels.Count();
            return PageList(_commonModels,pageindex,pageSize).AsQueryable();
        }

        public IQueryable<CommonModel> Order(IQueryable<CommonModel> entitys, int orderCode)
        {
            switch(orderCode)
            {
                //默认排序
                default:
                    entitys = entitys.OrderByDescending(p => p.ReleaseDate);
                    break;

            }
            return entitys;
        }
    }
}
