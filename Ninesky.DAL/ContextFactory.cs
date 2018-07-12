using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL
{
    public class ContextFactory
    {
        public static NineskyDbContext GetCurrentContext()
        {
            NineskyDbContext _nContext = CallContext.GetData("NineskyContext") as NineskyDbContext;
            if (_nContext == null)
            {
                _nContext = new NineskyDbContext();
                CallContext.SetData("NineskyContext", _nContext);
            }
            return _nContext;
        }
    }
}
