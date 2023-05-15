using GT_Core.Application.Common.Interfaces;

namespace GT_Core.Application.Common.Extensions
{
    public static class HttpClientFactoryExtensions
    {
        public static HttpClient CreateClient(this IHttpClientFactory _clientFactory, ITokenHandler _tokenService)
        {
            var client = new HttpClient();

            if (_tokenService.IsAuthenticated())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { _tokenService.GetHttpHeader()}");
            }

            return client;
        }
    }
}
