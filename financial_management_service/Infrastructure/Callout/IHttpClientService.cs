
using financial_management_service.Core.Exceptions;
using financial_management_service.Extensions;
using Newtonsoft.Json;

namespace financial_management_service.Infrastructure.Callout
{
    public interface IHttpClientService
    {
        Task<T?> DeleteAsync<T>(string endPoint, string token);
        Task<T?> GetAsync<T>(string endPoint, string token);
        Task<T?> PostAsync<T, R>(string endPoint, R payload, string token);
        Task PostAsync<R>(string endPoint, R payload, string token);
        Task<T?> PostAsync<T>(string endPoint, string token);
        Task<T?> PutAsync<T, R>(string endPoint, R payload, string token);
        Task<T?> PostFormUrlencodedAsync<T, R>(string endPoint, R payload, string token);
        Task PostFormUrlencodedAsync(string endPoint, string token);
        Task<T?> PostFormUrlencodedBase64Async<T, R>(string endPoint, R payload, string token);
        Task<byte[]> DownloadFileAsync(string endPoint);
        Task<List<T>> PostToListAsync<T>(string endPoint, string token);
        Task<string?>PostAsyncWithSystem<T,R>(string endPoint, R payload, string token, string systemToken);
        Task<byte[]> PostDownloadFileAsync(string endPoint);
    }

    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _clientFactory;
        public HttpClientService(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        #region Public methods
        async public Task<T?> DeleteAsync<T>(string endPoint, string token) => await SendRequest<T>(MakeRequest(endPoint, HttpMethod.Delete, token));

        async public Task<T?> GetAsync<T>(string endPoint, string token) => await SendRequest<T>(MakeRequest(endPoint, HttpMethod.Get, token));

        async public Task<T?> PostAsync<T, R>(string endPoint, R payload, string token) => await SendRequest<T>(MakeRequestWithPayload(endPoint, payload, HttpMethod.Post, token));
        
        async public Task PostAsync<R>(string endPoint, R payload, string token) => await CallApiWithEmptyContent(MakeRequestWithPayload(endPoint, payload, HttpMethod.Post, token));

        async public Task<T?> PostAsync<T>(string endPoint, string token) => await SendRequest<T>(MakeRequestWithPayload(endPoint, HttpMethod.Post, token));

        async public Task<T?> PutAsync<T, R>(string endPoint, R payload, string token) => await SendRequest<T>(MakeRequestWithPayload(endPoint, payload, HttpMethod.Put, token));

        async public Task<byte[]> DownloadFileAsync(string endPoint)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(endPoint);

            var result = await response.Content.ReadAsByteArrayAsync();

            if (!response.IsSuccessStatusCode)
                throw new UnhandledException(await response.Content.ReadAsStringAsync());
            response.Dispose();
            return result;
        }

        async public Task<T?> PostFormUrlencodedAsync<T, R>(string endPoint, R payload, string token)
        {
            var request = InitFormUrlendcoded(endPoint, HttpMethod.Post, token);

            if (payload != null)
                request.Content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)payload);

            return await SendRequest<T>(request);
        }

        async public Task PostFormUrlencodedAsync(string endPoint, string token) => await CallApiWithEmptyContent(InitFormUrlendcoded(endPoint, HttpMethod.Post, token));

        async public Task<T?> PostFormUrlencodedBase64Async<T, R>(string endPoint, R payload, string token)
        {
            var request = InitFormUrlendcoded(endPoint, HttpMethod.Post, token);

            request.Headers.Remove("Authorization");
            if (!token.IsNullOrEmpty())
                request.Headers.TryAddWithoutValidation("Authorization", "Basic " + token);

            if (payload != null)
                request.Content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)payload);

            return await SendRequest<T>(request);
        }

        #endregion

        #region Private methods
        private async Task<T?> SendRequest<T>(HttpRequestMessage request)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            var message = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new UnhandledException(message);

            if (message.IsNullOrEmpty() || JsonConvert.DeserializeObject<T>(message) == null)
                return default;

            var result = JsonConvert.DeserializeObject<T>(message);

            return result != null ? result : default;
        }
        private static HttpRequestMessage InitFormUrlendcoded(string endPoint, HttpMethod method, string token)
        {
            var request = InitRequest(endPoint, method, token);

            request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            return request;
        }

        private async Task CallApiWithEmptyContent(HttpRequestMessage request)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            var message = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new UnhandledException(message);
        }

        private static HttpRequestMessage InitJsonRequest(string endPoint, HttpMethod method, string token) => InitRequest(endPoint, method, token);

        private static HttpRequestMessage InitRequest(string endPoint, HttpMethod method, string token)
        {
            var request = new HttpRequestMessage(method, endPoint);
            request.Headers.Clear();
            if (!token.IsNullOrEmpty())
            {
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
            }
            return request;
        }

        private static HttpRequestMessage MakeRequestWithPayload<R>(string endPoint, R payload, HttpMethod method, string token)
        {

            var request = InitJsonRequest(endPoint, method, token);
            request.Content = new StringContent(payload != null ? JsonConvert.SerializeObject(payload) : "", System.Text.Encoding.UTF8, "application/json");
            return request;
        }

        private static HttpRequestMessage MakeRequestWithPayload(string endPoint, HttpMethod method, string token)
        {
            var request = InitJsonRequest(endPoint, method, token);
            request.Content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
            return request;
        }

        private static HttpRequestMessage MakeRequest(string endPoint, HttpMethod method, string token) => InitJsonRequest(endPoint, method, token);


        private async Task<List<T>> SendRequestToList<T>(HttpRequestMessage request)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            var message = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new UnhandledException(message);

            if (message.IsNullOrEmpty() || System.Text.Json.JsonSerializer.Deserialize<List<T>>(message) == null)
                return default;

            var result = System.Text.Json.JsonSerializer.Deserialize<List<T>>(message);

            return result!.Count > 0 ? result : default;
        }

        public async Task<List<T>> PostToListAsync<T>(string endPoint, string token) => await SendRequestToList<T>(MakeRequestWithPayload(endPoint, HttpMethod.Post, token));

        private async Task<string?> SendRequestWithSystem(HttpRequestMessage request)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            var message = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new UnhandledException(message);

            if (message.IsNullOrEmpty())
                return default;

            return message.ToString();
        }

        private static HttpRequestMessage MakeRequestWithPayloadBySystem<R>(string endPoint, R payload, HttpMethod method, string token, string systemToken)
        {
            var request = InitRequestSystemToken(endPoint, method, token, systemToken);
            request.Content = new StringContent(payload != null ? JsonConvert.SerializeObject(payload) : "", System.Text.Encoding.UTF8, "application/json");
            return request;
        }

        private static HttpRequestMessage InitRequestSystemToken(string endPoint, HttpMethod method, string token, string systemToken)
        {
            var request = new HttpRequestMessage(method, endPoint);
            request.Headers.Clear();
            if (!token.IsNullOrEmpty() && !systemToken.IsNullOrEmpty())
            {
                request.Headers.TryAddWithoutValidation("x-system-token", systemToken);
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
            }
            return request;
        }

		public async Task<string?> PostAsyncWithSystem<T, R>(string endPoint, R payload, string token, string systemToken) => await SendRequestWithSystem(MakeRequestWithPayloadBySystem(endPoint, payload, HttpMethod.Post, token, systemToken));

        async public Task<byte[]> PostDownloadFileAsync(string endPoint)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsync(endPoint, null);

            var result = await response.Content.ReadAsByteArrayAsync();

            if (!response.IsSuccessStatusCode)
                throw new UnhandledException(await response.Content.ReadAsStringAsync());
            response.Dispose();
            return result;
        }

        #endregion
    }
}



