using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlashMoney.DTO;
using FlashMoney.Models;
using FlashMoney.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FlashMoney.Controllers
{
    public class CardController : Controller
    {
        private IFlashMoneyHttpClient _flashMoneyHttpClient;
        private IConfiguration _configuration;

        public CardController(IFlashMoneyHttpClient flashMoneyHttpClient, IConfiguration configuration)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _configuration = configuration;
        }

        
        public async Task<IActionResult> Index()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.GetAsync($"QueryUserCard/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UserCardsDTO>(b);
                    if (decimal.Parse(_configuration["Transaction:TransactionFee"]) > 0)
                    {
                        result.Charge = decimal.Parse(_configuration["Transaction:TransactionFee"]);
                    }
                    return View(result);
                }
            }
            return View(new UserCardsDTO());
        }

        public async Task<IActionResult> SetDefault(Guid Id)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.GetAsync($"MakeCardPrimary/{Id}/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> GetCard()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.GetAsync($"GetCards/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CardDetailViewModel>>(b);
                    if (result.Count() != 0)
                    {
                        ViewBag.SavedCards = result.ToList();
                        ViewBag.Phone = phone;
                    }
                   
                    return PartialView("_GetCard");
                }
                return RedirectToAction("Index");
            }
        }
        public IActionResult GetPhone(string amount)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            if (phone != null)
            {
                ViewBag.Phone = phone;
                ViewBag.Amount = amount;
                return PartialView();
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.DeleteAsync($"Card/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pin(UserCardsDTO userCardsDTO)
        {
            var addcard = userCardsDTO.AddCardDTO;
            var h = addcard.CardNumber.Substring(addcard.CardNumber.Length - 4);

            addcard.CardNumberFux = $"**** **** **** {h}";
            return PartialView("_CardPin", addcard);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCard(AddCardDTO fundWalletDTO)
        {
            var h = fundWalletDTO.CardNumber.Substring(fundWalletDTO.CardNumber.Length - 4);
            fundWalletDTO.CardNumberFux = $"** {h}";

            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    fundWalletDTO.SourcePhone = phone;
                    var otp = $"{fundWalletDTO.One}{fundWalletDTO.Two}{fundWalletDTO.Three}{fundWalletDTO.Four}";

                    fundWalletDTO.Pin = otp;

                     var json = JsonConvert.SerializeObject(fundWalletDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("AddCard", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);
                    
                        if (result.status == "success")
                        {
                            return PartialView("_AddSuccess", fundWalletDTO);
                        }
                        else if (result.status == "failed")
                        {
                            return PartialView("_AddFailure", fundWalletDTO);
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            return PartialView("_AddFailure", fundWalletDTO);
                        }
                    }
                }
            }
            return PartialView("_AddFailure", fundWalletDTO);
        }
    }
}