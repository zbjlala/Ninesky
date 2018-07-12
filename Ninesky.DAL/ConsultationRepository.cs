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
    /// 咨询仓库
    /// </summary>
    public class ConsultationRepository:BaseRepository<Consultation>,InterfaceConsultationRepository
    {
    }
}
