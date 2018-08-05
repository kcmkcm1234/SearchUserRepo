namespace IDOBusTech.NETTech.Test.Service
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IDOBusTech.NETTech.Test.Service.Interface;
    using System.Net;

    public class IDOBusHttpClient : IIDOBusHttpClient, IDisposable
    {
        private bool _disposed { get; set; }
        private HttpClient _httpClient { get; set; }

        public IDOBusHttpClient()
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public virtual async Task<T> GetAsync<T>(string url)
        {
            var json = await ExecuteGetAsync(url);

            if(!string.IsNullOrEmpty(json))
            {
                return (T)(object)JsonConvert.DeserializeObject<T>(json);
            }
          
            return default(T);
        }

        private async Task<string> ExecuteGetAsync(string url)
        {
            using (var response = await _httpClient.GetAsync(url).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }

                _httpClient.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
