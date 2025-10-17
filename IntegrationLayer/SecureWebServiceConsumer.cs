
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace School.IntegrationLayer
{
 

    public class SecureWebServiceConsumer
    {
        private readonly HttpClient _httpClient;

        public SecureWebServiceConsumer()
        {
            // Configure HTTP client with TLS 1.2/1.3
            var handler = new HttpClientHandler
            {
                // Let the OS choose the best protocol (recommended approach)
                SslProtocols = SslProtocols.None,

                // For specific protocol enforcement (if required):
                // SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,

                // Additional security settings
                UseCookies = false,
                AllowAutoRedirect = false
            };

            _httpClient = new HttpClient(handler);

            // Set default headers
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
        }

        public async Task<string> GetSecureDataAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
