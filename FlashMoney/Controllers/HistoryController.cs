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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FlashMoney.Controllers
{
    public class HistoryController : Controller
    {
        private IFlashMoneyHttpClient _flashMoneyHttpClient;

        public HistoryController(IFlashMoneyHttpClient flashMoneyHttpClient)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
        }

        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";
            
         
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.GetAsync($"History/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<TransactionHistoryModel>>(b);
                    //var result = results.AsQueryable();
                    switch (sortOrder)
                    {
                        case "date_asc":
                            result = result.OrderBy(s => s.Date).ToList();
                            break;
                        case "Amount":
                            result = result.OrderBy(s => s.Amount).ToList();
                            break;
                        case "amount_desc":
                            result = result.OrderByDescending(s => s.Amount).ToList();
                            break;
                        default:
                            result = result.OrderByDescending(s => s.Date).ToList();
                            break;
                    }

                    int pageSize = 10;
                    return View(PaginatedList<TransactionHistoryModel>.CreateAsync(result, pageNumber ?? 1, pageSize));
                
                }
            }
            return View();
        }
    }
}