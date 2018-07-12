using Ninesky.IDAL;
using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL
{
    public class CommonModelRepository:BaseRepository<CommonModel>,InterfaceCommonModelRepository
    {
        public  bool Delete(Models.ArticeModel.CommonModel commonModel,bool isSave = true)
        {
            if(commonModel.Attachment != null)
            {
                nContext.Attachments.RemoveRange(commonModel.Attachment);
            }
            if(commonModel.Article != null)
            {
                nContext.Articles.Remove(commonModel.Article);
            }
            if(commonModel.Consultation != null)
            {
                nContext.Consultations.Remove(commonModel.Consultation);
            }
            nContext.CommonModels.Remove(commonModel);
            return isSave ? nContext.SaveChanges() > 0 : true;
        }
    }
}
