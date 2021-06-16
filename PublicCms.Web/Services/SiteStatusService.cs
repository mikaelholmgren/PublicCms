using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public class SiteStatusService : ISiteStatusService
    {
        private bool _docDbStatus;
        public bool GetDocDbStatus()
        {
            return _docDbStatus;
        }

        public void SetDocDbStatus(bool status)
        {
            _docDbStatus = status;
        }
    }
}
