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
    /// 附件服务
    /// </summary>
    public class AttachmentService:BaseService<Attachment>,interfaceAttachmentService
    {
        public AttachmentService():base(RepositoryFactory.AttachmentRepository)
        {

        }

        public IQueryable<Attachment> FindList(int? modelID, string owner, string type)
        {
            var _attachemts = CurrentRepository.Entities.Where(a => a.ModelID == modelID);
            if(!string.IsNullOrEmpty(owner))
            {
                _attachemts = _attachemts.Where(a => a.Owner == owner);
            }
            if(!string.IsNullOrEmpty(type))
            {
                _attachemts = _attachemts.Where(a => a.Type == type);
            }
            return _attachemts;
        }

        public IQueryable<Attachment> FindList(int modelID, string owner, string type, bool withModelIDNull)
        {
            var _attachemts = CurrentRepository.Entities;
            if(withModelIDNull)
            {
                _attachemts = _attachemts.Where(a => a.ModelID == modelID || a.ModelID == null);
            }
            else
            {
                _attachemts = _attachemts.Where(a =>a.ModelID == modelID);
            }
            if(!string.IsNullOrEmpty(owner))
            {
                _attachemts = _attachemts.Where(a => a.Owner == owner);
            }
            if(!string.IsNullOrEmpty(type))
            {
                _attachemts = _attachemts.Where(a => a.Type == type);
            }
            return _attachemts;
        }
    }
}
