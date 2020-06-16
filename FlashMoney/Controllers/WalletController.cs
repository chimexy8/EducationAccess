using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FlashMoney.DTO;
using FlashMoney.Models;
using FlashMoney.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FlashMoney.Controllers
{
    public class WalletController : Controller
    {
        private IFlashMoneyHttpClient _flashMoneyHttpClient;
        private IConfiguration _configuration;

        public WalletController(IFlashMoneyHttpClient flashMoneyHttpClient, IConfiguration configuration)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> QueryCards()
        {
            if (ModelState.IsValid)
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var response = await client.GetAsync($"QueryUserCard/{phone}");
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<UserCardsDTO>(b);

                        if (result.Cards.Count > 0)
                        {
                            if (decimal.Parse(_configuration["Transaction:TransactionFee"])>0)
                            {
                                result.Charge = decimal.Parse(_configuration["Transaction:TransactionFee"]);
                            }
                           
                            return PartialView("_FundAmount", result);
                        }
                            
                        else
                            return PartialView("_AddNewCard");
                    }
                }
            }
            return PartialView("_FundFailure");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pin(UserCardsDTO userCardsDTO)
        {
            var fundWalletDTO = new FundWalletDTO { Amount = userCardsDTO.Amount };
            return PartialView("_PinValidation", fundWalletDTO);
        }

        public async Task<IActionResult> CreateAuthPin(string pin)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            var Pin = $"{pin}";
            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.GetAsync($"CreatePin/{Pin}/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UserCheckInfo>(b);
                    if (result.Status == "successfull")
                    {
                        return Json(new { result = "success", message=result.Message });
                    }
                    else
                    {
                        return Json(new { result = "failed", message = result.Message });
                    }
                }
                else
                {
                    return Json(new { result = "network" });
                }
            }
        }
      
        public async Task<IActionResult> AddCardInitiated(string reference)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            using (var client2 = _flashMoneyHttpClient.GetClient())
            {
                //var json = JsonConvert.SerializeObject(reference);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client2.GetAsync($"LogCardAttempt/{reference}/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                return BadRequest();


            }

        }
        public async Task<IActionResult> Paystackresponse(string reference)
        {
            if (!string.IsNullOrEmpty(reference))
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                var obj = new AddCardReferenceDTO { SourcePhone = phone, Reference = reference };
                using (var client2 = _flashMoneyHttpClient.GetClient())
                {
                    var json2 = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(json2, Encoding.UTF8, "application/json");
                    var flashapiresponse = await client2.PostAsync("CheckCard", content);
                }
                return Ok();
            }
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FundWallet(FundWalletDTO fundWalletDTO, string id)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    Guid cardId = Guid.Empty;
                    if (!string.IsNullOrEmpty(id))
                        cardId = Guid.Parse(id);

                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    fundWalletDTO.SourcePhone = phone;
                    var otp = $"{fundWalletDTO.One}{fundWalletDTO.Two}{fundWalletDTO.Three}{fundWalletDTO.Four}";

                    var transaction = new PinTransferModel
                    {
                        Amount = fundWalletDTO.Amount,
                        AuthType = fundWalletDTO.AuthType,
                        SourcePhone = fundWalletDTO.SourcePhone,
                        Pin = otp,
                        CardId = cardId
                    };
                    var json = JsonConvert.SerializeObject(transaction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("FundWallet", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return PartialView("_FundSuccess", fundWalletDTO);
                        }
                        else if (result.status == "failed")
                        {
                            return PartialView("_FundFailure");
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            return PartialView("_FundFailure");
                        }
                    }
                }
            }
            return PartialView("_FundFailure");
        }
    }
}