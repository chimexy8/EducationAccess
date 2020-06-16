using FlashMoney.Controllers;
using FlashMoney.DTO;
using FlashMoney.Models;
using FlashMoney.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FlashMoney.Filters
{
    public class SingleLogonActionFilter : IAsyncActionFilter
    {
        private IFlashMoneyHttpClient _flashMoneyHttpClient;
        private IConfiguration _configuration;
        public SingleLogonActionFilter(IFlashMoneyHttpClient flashMoneyHttpClient, IConfiguration configuration)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _configuration = configuration;
        }
        public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            if (context.Controller.GetType().Equals(typeof(AuthController)) || await CheckSession(context.HttpContext.User) == "success")
            {
                var resultContext = await next();
                // You can do something with resultContext.Result.
            }
            else
            {
                context.Result = new UnauthorizedResult();
                bool isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (!isAjax)
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", "Logout");
                    redirectTargetDictionary.Add("controller", "Auth");

                    context.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
                await context.Result.ExecuteResultAsync(context);
                
            }
        }

        public async Task<string> CheckSession(System.Security.Claims.ClaimsPrincipal User)
        {
            if (User.Identity.IsAuthenticated)
            {
                string status = "fail";

                HttpClient _httpClient = new HttpClient();
                var tOKEN = User.Claims.FirstOrDefault(p => p.Type == "token").Value;
                _httpClient.BaseAddress = new Uri(_configuration["FlashApi:Url"]);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.SetBearerToken(tOKEN);

                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                var log = new Login { SessionId = User.Claims.FirstOrDefault(p => p.Type == "sessionId").Value, UserId = phone };

                var json = JsonConvert.SerializeObject(log);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("CheckSession", content);
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HomeOverviewModel>(b);
                    if (result.status == "ok")
                    {
                        status = "success";
                    }
                        
                }
                _httpClient.Dispose();
                
                return status;
            }
            else
            {
                return "ok";
            }

            
        }
    }
}
