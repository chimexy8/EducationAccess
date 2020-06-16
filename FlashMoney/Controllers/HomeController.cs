using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlashMoney.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;
using System.Net.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using FlashMoney.Services;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using FlashMoney.DTO;
using System.Globalization;

namespace FlashMoney.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IFlashMoneyHttpClient _flashMoneyHttpClient;

        public  HomeController(IHttpContextAccessor httpContextAccessor, IFlashMoneyHttpClient flashMoneyHttpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _flashMoneyHttpClient = flashMoneyHttpClient;
        }
    
        public async Task<IActionResult> Index(string Pxt = null)
        {
            //var hj = Guid.NewGuid();
            //var hjhj = Guid.NewGuid();
            //Wellcome();
            //await UserInfo();
            //await CallAPi();
           
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;

            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response1 = await client.GetAsync($"HasResetPin/{phone}");
                if (response1.IsSuccessStatusCode)
                {
                    var b = await response1.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HomeOverviewModel>(b);
                    if (result.status=="true")
                    {
                        ViewBag.HasResetPin = true;
                    }
                    else
                    {
                        ViewBag.HasResetPin = false;
                    }
                 
                   
                }
                var response = await client.GetAsync($"Balance/{phone}");

                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HomeOverviewModel>(b);

                    var h = decimal.Parse(result.Balance).ToString(); // ("c", new CultureInfo("ha-Latn-NG"));

                    var bal = new OverviewModel { Balance = h, Activities = result.activities, TransactionCount  = result.TransactionCount, FundWalletCount = result.FundWalletCount, Phone = phone,LastFunded = !string.IsNullOrEmpty(result.LastFunded) ? result.LastFunded : "N/A" };
                    
                    return View(bal);
                }
            }
            var defaultBalance = "";
            if (Pxt == "New")
            {
                defaultBalance = decimal.Parse("0.00").ToString(); //("c", new CultureInfo("ha-Latn-NG"));
                return View(new OverviewModel { Balance = defaultBalance, Activities = new List<ActivityDTO>(), Pin = Pxt, Phone = phone, LastFunded = "N/A" });
            }
            return View(new OverviewModel { Balance = defaultBalance, Activities = new List<ActivityDTO>(), Phone = phone, LastFunded = "N/A" });
        }

        private void Wellcome()
        {
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"{claim.Type}: {claim.Value}");
            }
        }

        public async Task UserInfo()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44333/");
            if (disco.IsError) throw new Exception(disco.Error);

            var token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = token,
            });

            if (response.IsError) throw new Exception(response.Error);

            var claims = response.Claims.FirstOrDefault(p=>p.Type == "address")?.Value;

            Debug.WriteLine(claims);
        }

        public async Task  CallAPi()
        {

            var _client =  _flashMoneyHttpClient.GetClient();
            var response = await _client.GetAsync("api/values/Welcome");
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(JArray.Parse(content));
            }
        }

        public async Task<IActionResult> History()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;

            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var tOKEN = User.Claims.FirstOrDefault(p => p.Type == "token").Value;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.SetBearerToken(tOKEN);


                var response = await client.GetAsync($"History/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                   
                    return View();
                }
                ModelState.AddModelError(string.Empty, "Error");
            }
            return View();
        }

        public IActionResult ResetPin()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            var model = new ResetPinModel
            {
                Phone = phone
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPin(ResetPinModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var tOKEN = User.Claims.FirstOrDefault(p => p.Type == "token").Value;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(tOKEN);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("ResetPin", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return RedirectToAction("Success","Transaction");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }
            }
            return View(model);
        }
        
        public async Task<object> GetDailyChatData()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            var fakeObject = new
            {
                Quantity = new List<int>(),
            };

            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var tOKEN = User.Claims.FirstOrDefault(p => p.Type == "token").Value;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.SetBearerToken(tOKEN);
                var response = await client.GetAsync($"QetChartData/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ChartDataDTO>(b);

                    fakeObject.Quantity.Add(result.Tranfers);
                    fakeObject.Quantity.Add(result.FundWallet);
                }
            }

            return fakeObject;
        }


        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult FAQ()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Terms()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult ErrorPage()
        {
            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
