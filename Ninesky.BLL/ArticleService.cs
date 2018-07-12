using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.IDAL;
using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.BLL
{
    /// <summary>
    /// 附件服务
    /// </summary>
    public class ArticleService : BaseService<Article>,interfaceArticleService
    {
        public ArticleService() :base(RepositoryFactory.ArticleRepository)
        {

        }
    }
}
