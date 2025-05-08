using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBridg.Service
{
    public interface IWapMessageService
    {
        int GetNoRedMsgCount(int id);
    }
}