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
    /// 咨询服务
    /// </summary>
    public class ConsultationService : BaseService<Consultation>,interfaceConsultationService
    {
        public ConsultationService() : base(RepositoryFactory.ConsultationRepository)
        {

        }
    }
}
