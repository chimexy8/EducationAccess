using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlashMoneyApi.Services
{
    public class FlashHttpClient : IFlashHttpClient
    {
        private HttpClient _httpClient = new HttpClient();

        public HttpClient GetClient()
        {
            try
            {
                _httpClient.BaseAddress = new Uri("https://cyhermes.fcmb.com:2217/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return _httpClient;
            }
            catch (Exception ex)
            {
            }

            return _httpClient;
        }
    }
}
