using PublicCms.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicCms.Web.Gateways
{
    public interface IVisitorCounterGateway
    {
        Task AddVisitToPageAsync(Guid pageId);
        Task<IEnumerable<VisitorModel>> GetAllVisitorStatsAsync();
    }
}