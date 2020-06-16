using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlashMoney.Services
{
    public class FlashHttpClient : IFlashHttpClient
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();

        public FlashHttpClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }


        public HttpClient GetClient()
        {
            _httpClient.BaseAddress = new Uri(_configuration["FlashApi:Url"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _httpClient;

        }
    }
}
