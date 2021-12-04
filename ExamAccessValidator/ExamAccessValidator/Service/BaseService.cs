using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExamAccessValidator.Service
{
    public static class BaseService
    {
        /// <summary>
        /// Set up the http client to get ready for use.
        /// </summary>
        /// <returns><see cref="HttpClient"/> the setted up http client.</returns>
        private static HttpClient ConfigureClient()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (s, c, ch, ssl) => true;
            var client = new HttpClient(clientHandler);
            return client;
        }

        public static async Task<TResult> GetAsync<TResult>(string url, string data)
        {
            var client = ConfigureClient();
            HttpResponseMessage httpResponse = await client.GetAsync(url + data);

            TResult result = default;

            if (httpResponse.IsSuccessStatusCode)
            {
                var d = await httpResponse.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(d);
                result = JsonConvert.DeserializeObject<TResult>(d);
            }
            else
            {
                var error = await httpResponse.Content.ReadAsStringAsync();
                throw new ApplicationException($"API ERROR: {error}");
            }

            return result;
        }
    }
}
