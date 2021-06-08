using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicCms.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PublicCms.Web.Gateways
{
    public class VisitorCounterGateway : IVisitorCounterGateway
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILogger<VisitorCounterGateway> _logger;
        private readonly string apiURL;

        public VisitorCounterGateway(HttpClient httpClient, IConfiguration config, ILogger<VisitorCounterGateway> logger)
        {
            this._httpClient = httpClient;
            this._config = config;
            this._logger = logger;
            apiURL = _config["APISettings:VisitorsAPI"];
        }
        public async Task AddVisitToPageAsync(Guid pageId)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{apiURL}/{pageId}", new SubmitModel() { Increment = true });
                if (!response.IsSuccessStatusCode) _logger.LogError($"An error ocurred while adding vissitor to counter (statuscode: {response.StatusCode})");
            }
            catch (Exception ex)
            {

                _logger.LogError($"Adding visitor {ex.Message}");
            }
        }
        public async Task<IEnumerable<VisitorModel>> GetAllVisitorStatsAsync()
        {
            var list = await _httpClient.GetFromJsonAsync<IEnumerable<VisitorModel>>(apiURL);
            return list;
        }
    }
    public class SubmitModel
    {
        public bool Increment { get; set; }
    }
}
