using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace myapi.Services
{
    public class AppHttpService : IAppHttpService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AppHttpService> _logger;

        public AppHttpService(IHttpClientFactory httpClientFactory,
            ILogger<AppHttpService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<T> GetWithClient<T>(string api, string client)
        {
            var httpClient = _httpClientFactory.CreateClient(client);
            var request = new HttpRequestMessage(HttpMethod.Get, api);
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            T result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

            return result;
        }

    }
}
