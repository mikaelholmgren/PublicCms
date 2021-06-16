using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public interface ISiteStatusService
    {
        bool GetDocDbStatus();
        void SetDocDbStatus(bool status);
    }
}
