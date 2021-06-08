using System;
using System.Threading.Tasks;

namespace PublicCms.Web.Gateways
{
    public interface IVisitorCounterGateway
    {
        Task AddVisitToPageAsync(Guid pageId);
    }
}