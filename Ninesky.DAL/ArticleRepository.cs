using Ninesky.IDAL;
using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL
{
    /// <summary>
    /// 文章仓库
    /// </summary>
    public class ArticleRepository:BaseRepository<Article>,InterfaceArticleRepository
    {
    }
}
