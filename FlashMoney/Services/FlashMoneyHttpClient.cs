using FlashMoney.Data;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlashMoney.Services
{
    public class FlashMoneyHttpClient : IFlashMoneyHttpClient
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();

        public FlashMoneyHttpClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public HttpClient GetClient()
        {

            //var client = new HttpClient();
            //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44333/");
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);

            //}
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "WebApp",
            //    ClientSecret = "secret",
            //    Scope = "FlashApi"
            //});

            //if (tokenResponse.IsError)
            //{
            //    Debug.WriteLine(tokenResponse.Error);

            //}
            //https://localhost:44344/
            //https://flashmoney.azurewebsites.net/Api
            var tOKEN = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "token").Value;
            _httpClient.BaseAddress = new Uri(_configuration["FlashApi:Url"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.SetBearerToken(tOKEN);
            return _httpClient;

        }
    }
}
