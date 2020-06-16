using FlashMoney.DTO;
using FlashMoney.Models;
using FlashMoney.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlashMoney.Controllers
{
    public class TransactionController : Controller
    {
        private IFlashMoneyHttpClient _flashMoneyHttpClient;
        private IConfiguration _configuration;

        public TransactionController(IFlashMoneyHttpClient flashMoneyHttpClient,IConfiguration configuration)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _configuration = configuration;
        }

        public IActionResult Transfer()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                var gg = new TransactionModel { SourcePhone = phone };
                return View(gg);
        }

        public async Task<string> QueryRecipient(string DestinationPhone)
        {
            var phonee = $"234{DestinationPhone.Substring(DestinationPhone.Length - 10)}";

            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var json = JsonConvert.SerializeObject(new { Phone = phonee });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("QueryReceipient", content);
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UserCheckInfo>(b);

                    if (!string.IsNullOrEmpty(result?.FirstName))
                    {
                        return result.FirstName;
                    }
                
                }
            }
            return "";
        }
        public async Task<IActionResult> RepeatTransaction(Guid Id)
        {
            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var stringID = Id.ToString();
                var PhoneT = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                var json = JsonConvert.SerializeObject(Id);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.GetAsync($"HistoryById/{stringID}/{PhoneT}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HistoryStatandTrans>(b);
                    if (result.status == "Transfer")
                    {
                        OverviewModel overview = new OverviewModel();
                        TransactionModel trans = new TransactionModel();
                        trans.Amount= result.Transaction.Amount;
                        trans.DestinationPhone= result.Transaction.DestinationPhone;
                        overview.TransactionModel = trans;
                        var json2 = JsonConvert.SerializeObject(new { Phone = trans.DestinationPhone, Senderphone =PhoneT});
                        var content2 = new StringContent(json2, Encoding.UTF8, "application/json");

                        var response2 = await client.PostAsync("QueryReceipient", content2);
                        if (response2.IsSuccessStatusCode)
                        {
                            var b2 = await response2.Content.ReadAsStringAsync();
                            var result2 = JsonConvert.DeserializeObject<UserCheckInfo>(b2);
                            overview.TransactionModel.Receipient = result2?.FirstName;
                            overview.TransactionModel.TransacrtionCharge = decimal.Parse(_configuration["Transaction:Charge"]);
                            if (overview.TransactionModel.Amount > 20000M)
                            {
                                if (result2.HasSetTap)
                                {
                                    ViewBag.HasTap = 1;
                                }
                               
                                return PartialView("_TransferAuthType", overview.TransactionModel);
                            }
                            else
                            {
                            overview.TransactionModel.AuthType = "PIN";
                                return PartialView("_PINAuthentication", overview.TransactionModel);
                            }


                        }
                    }
                    else if (result.status == "Wallet Funded")
                    {
                        return Json(new { amount = result.Transaction.Amount, cardId = result.Transaction.CardId });
                    }
                    
                }
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GoBackFund()
        {
            return PartialView();
        }
        
        [HttpGet]
        public IActionResult GoBackUpload()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult GoBackWallet()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(OverviewModel transaction)
        {
            if (ModelState.IsValid)
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                var subphone= $"234{phone.Substring(phone.Length - 10)}";
                var phonee = $"234{transaction.TransactionModel.DestinationPhone.Substring(transaction.TransactionModel.DestinationPhone.Length - 10)}";
                if (subphone==phonee)
                {
                    ViewBag.Sendingtome = "You are attempting to transfer money to yourself,please choose another user";
                    return PartialView("_TransError");
                }
                else if (transaction.TransactionModel.Amount > 50000M)
                {
                    ViewBag.Sendingtome = "Transfer Limit of 50000 Exceeded,Reduce Amount";
                    return PartialView("_TransError");
                }
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(new {Phone = phonee, Senderphone =phone});
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("QueryReceipient", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<UserCheckInfo>(b);
                        transaction.TransactionModel.Receipient = result?.FirstName;
                        transaction.TransactionModel.TransacrtionCharge = decimal.Parse(_configuration["Transaction:Charge"]);
                        if (transaction.TransactionModel.Amount > 20000M)
                        {
                            if (result != null && result.HasSetTap)
                            {
                                ViewBag.HasTap = 1;
                            }

                            return PartialView("_TransferAuthType", transaction.TransactionModel);
                        }
                       
                        else
                        {
                            transaction.TransactionModel.AuthType = "PIN";
                            return PartialView("_PINAuthentication", transaction.TransactionModel);
                        }

                     
                    }
                }
             
            }
            return PartialView("_TransError");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TransferTAPAuth(TransactionModel tran)
        {
            if (ModelState.IsValid)
            {
                var otp = $"{tran.PinOne}{tran.PinTwo}{tran.PinThree}{tran.PinFour}";
                if (string.IsNullOrEmpty(otp) || otp.Length < 4)
                    return PartialView("_TransError");
                tran.Pin = otp;
                tran.AuthType = "TAP";
                return PartialView("_TapAuthentication", tran);
            }
            return PartialView("_TransError");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransferOTPAuth(TransactionModel transaction)
        {
            if (ModelState.IsValid)
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                transaction.SourcePhone = phone;
                if (!string.IsNullOrEmpty(transaction.SourcePhone))
                {
                   var otp = $"{transaction.PinOne}{transaction.PinTwo}{transaction.PinThree}{transaction.PinFour}";
                    if (string.IsNullOrEmpty(otp) || otp.Length < 4)
                        return PartialView("_TransError");
                    using (var client = _flashMoneyHttpClient.GetClient())
                    {
                        var json = JsonConvert.SerializeObject(new { Phone = transaction.SourcePhone });
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("ResendCode", content);

                      
                        transaction.Pin = otp;
                        transaction.AuthType = "OTP";
                        return PartialView("_OTPAuthentication", transaction);
                    }
                }
            }
            return PartialView("_TransError");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PinTransfer(TransactionModel tran)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    tran.SourcePhone = phone;
                    var otp = $"{tran.One}{tran.Two}{tran.Three}{tran.Four}{tran.Five}{tran.Six}";
                    if(string.IsNullOrEmpty(otp) || otp.Length < 4)
                        return PartialView("_TransError");
                    var transaction = new PinTransferModel
                    {
                        Amount = tran.Amount,
                        AuthType = tran.AuthType,
                        DestinationPhone = tran.DestinationPhone,
                        SourcePhone = tran.SourcePhone,
                        Narration = tran.Narration,
                        Pin = otp,
                        TransferCharge = tran.TransacrtionCharge
                    };
                    var json = JsonConvert.SerializeObject(transaction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("Transfer", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return PartialView("_TransferSuccess", tran);
                        }
                        else if (result.status == "fail")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_TransferFailure", Status);
                        }
                        else if (result.status == "failed")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_TransferFailure", Status);
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_TransferFailure", Status);
                        }
                    }
                }
            }
            return PartialView("_TransError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OTPTransfer(TransactionModel tran)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    tran.SourcePhone = phone;
                    var otp = $"{tran.One}{tran.Two}{tran.Three}{tran.Four}{tran.Five}{tran.Six}";
                    if (string.IsNullOrEmpty(otp) || otp.Length < 6)
                        return PartialView("_TransError");
                    var transaction = new PinTransferModel
                    {
                        Amount = tran.Amount,
                        AuthType = tran.AuthType,
                        DestinationPhone = tran.DestinationPhone,
                        SourcePhone = tran.SourcePhone,
                        Narration = tran.Narration,
                        Pin = tran.Pin,
                        Otp = otp,
                        TransferCharge = tran.TransacrtionCharge
                    };
                    var json = JsonConvert.SerializeObject(transaction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("Transfer", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return PartialView("_TransferSuccess", tran);
                        }
                        else if (result.status == "fail")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_TransferFailure", Status);
                        }
                        else if (result.status == "failed")
                        {
                            return PartialView("_TransferFailure");
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            return PartialView("_TransferFailure");
                        }
                    }
                }
            }
            return PartialView("_TransferFailure");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TAPTransfer(TransactionModel tran)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    tran.SourcePhone = phone;
                    var otp = $"{tran.One}{tran.Two}{tran.Three}{tran.Four}{tran.Five}{tran.Six}";
                    if (string.IsNullOrEmpty(otp) || otp.Length < 4)
                        return PartialView("_TransError");
                    var transaction = new PinTransferModel
                    {
                        Amount = tran.Amount,
                        AuthType = tran.AuthType,
                        DestinationPhone = tran.DestinationPhone,
                        SourcePhone = tran.SourcePhone,
                        Narration = tran.Narration,
                        Pin = tran.Pin,
                        Tap = otp,
                        TransferCharge = tran.TransacrtionCharge
                    };
                    var json = JsonConvert.SerializeObject(transaction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("Transfer", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return PartialView("_TransferSuccess", tran);
                        }
                        else if (result.status == "fail")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_TransferFailure", Status);
                        }
                        else if (result.status == "failed")
                        {
                            return PartialView("_TransferFailure");
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            return PartialView("_TransferFailure");
                        }
                        else if (result.status == "fail" && result.message == "Wrong TAP")
                        {
                            return PartialView("_TransferFailure");
                        }
                    }
                }
            }
            return PartialView("_TransferFailure");
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRecipient(MultipleTransModel transaction)
        {
            return PartialView("_MultipleAddRecipient");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecipientList(OverviewModel transaction)
        {
            if (ModelState.IsValid)
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                var subphone = $"234{phone.Substring(phone.Length - 10)}";
                 if (transaction.Recipients.Sum(p => p.Amount) > 50000M)
                {
                    ViewBag.Sendingtome = "Transfer Limit of 50000 Exceeded,Reduce Amount";
                    return PartialView("_MultipleTransferFailure");
                }
                foreach (var item in transaction.Recipients)
                {
                    var receiverphone = $"234{item.DestinationPhone.Substring(item.DestinationPhone.Length - 10)}";
                    if (subphone == receiverphone)
                    {
                        ViewBag.Sendingtome = "Your account is included in the recipients, please remove and try again";
                        return PartialView("_MultipleTransferFailure");
                    }
                    else if (item.Amount < 0)
                    {
                        ViewBag.Sendingtome = "Invalid Amount";
                        return PartialView("_MultipleTransferFailure");
                    }
                    

                }
                var transModel = new MultipleTransModel();
                    var json = JsonConvert.SerializeObject(transaction.Recipients);
                    transModel.Rps = json;
                    transModel.TransferCharge = decimal.Parse(_configuration["Transaction:Charge"]);
                     transModel.Sum = transaction.Recipients.Sum(p => p.Amount);
                    transModel.RecipientCount = transaction.Recipients.Count;
                        if (transaction.Recipients.Sum(p=>p.Amount) > 20000M)
                        {
                            return PartialView("_MultipleTransferAuthType", transModel);
                        }
                        else
                        {
                             transModel.AuthType = "PIN";
                            return PartialView("_MultiplePINAuthentication", transModel);
                        }
                        
            }
            return PartialView("_MultipleTransferFailure");
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MultipleTransferTAPAuth(MultipleTransModel tran)
        {
            if (ModelState.IsValid)
            {
                var otp = $"{tran.PinOne}{tran.PinTwo}{tran.PinThree}{tran.PinFour}";
                if (string.IsNullOrEmpty(otp) || otp.Length < 4)
                {
                    TransferStatus transferStatus = new TransferStatus { Message = "Please add your Auth PIN" };
                    return PartialView("_MultipleTransferError", transferStatus);
                }
                tran.Pin = otp;
                tran.AuthType = "TAP";
                return PartialView("_MultipleTapAuthentication", tran);
            }
            return PartialView("_MultipleTransferError");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultipleTransferOTPAuth(MultipleTransModel transaction)
        {
            if (ModelState.IsValid)
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                transaction.SourcePhone = phone;
                if (!string.IsNullOrEmpty(transaction.SourcePhone))
                {
                    var otp = $"{transaction.PinOne}{transaction.PinTwo}{transaction.PinThree}{transaction.PinFour}";
                    if (string.IsNullOrEmpty(otp) || otp.Length < 4)
                    {
                        TransferStatus transferStatus = new TransferStatus { Message = "Please add your Auth PIN" };
                        return PartialView("_MultipleTransferError", transferStatus);
                    }
                     
                    using (var client = _flashMoneyHttpClient.GetClient())
                    {
                        var json = JsonConvert.SerializeObject(new { Phone = transaction.SourcePhone });
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("ResendCode", content);


                        transaction.Pin = otp;
                        transaction.AuthType = "OTP";
                        return PartialView("_MultipleOTPAuthentication", transaction);
                    }
                }
            }
            return PartialView("_MultipleTransferError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultiplePinTransfer(MultipleTransModel tran)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    tran.SourcePhone = phone;
                    var otp = $"{tran.One}{tran.Two}{tran.Three}{tran.Four}{tran.Five}{tran.Six}";
                    if (string.IsNullOrEmpty(otp) || otp.Length < 4)
                    {
                        TransferStatus transferStatus = new TransferStatus { Message = "Please add your Auth PIN" };
                        return PartialView("_MultipleTransferError", transferStatus);
                    }

                    var transaction = new MultiPinTransferModel
                    {
                        AuthType = tran.AuthType,
                        SourcePhone = tran.SourcePhone,
                        Pin = otp,
                        Rps = tran.Rps,
                        TransferCharge = tran.TransferCharge
                    };
                    var json = JsonConvert.SerializeObject(transaction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("MultipleTransfer", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            if (result.Resp.Count == 0)
                            {
                                return PartialView("_MultipleTransferSuccess", tran);
                            }
                            else
                            {
                                tran.RecipientCount = tran.RecipientCount - result.Resp.Count;
                                tran.Sum = tran.Sum - result.Resp.Sum(p => p.Amount);
                                ViewBag.Failed = result.Resp;
                                return PartialView("_MultipleTransferSuccess", tran);
                            }

                        }
                        else if (result.status == "fail")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_MultipleTransferFailure", Status);
                        }
                        else if (result.status == "failed")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_MultipleTransferFailure", Status);
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_MultipleTransferFailure", Status);
                        }
                    }
                }
            }
            return PartialView("_MultipleTransferError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultipleOTPTransfer(MultipleTransModel tran)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    tran.SourcePhone = phone;
                    var otp = $"{tran.One}{tran.Two}{tran.Three}{tran.Four}{tran.Five}{tran.Six}";
                    if (string.IsNullOrEmpty(otp) || otp.Length < 6)
                        return PartialView("_MultipleTransferFailure");
                    var transaction = new MultiPinTransferModel
                    {
                        AuthType = tran.AuthType,
                        SourcePhone = tran.SourcePhone,
                        Pin = tran.Pin,
                        Otp = otp,
                        Rps = tran.Rps,
                        TransferCharge = tran.TransferCharge
                    };
                    var json = JsonConvert.SerializeObject(transaction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("MultipleTransfer", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            if (result.Resp.Count==0)
                            {
                                return PartialView("_MultipleTransferSuccess", tran);
                            }
                            else
                            {
                                tran.RecipientCount = tran.RecipientCount - result.Resp.Count;
                                tran.Sum = tran.Sum - result.Resp.Sum(p => p.Amount);
                                ViewBag.Failed = result.Resp;
                                return PartialView("_MultipleTransferSuccess", tran);
                            }
                            
                        }
                        else if (result.status == "fail")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_MultipleTransferFailure", Status);
                        }
                        else if (result.status == "failed")
                        {
                            return PartialView("_MultipleTransferFailure");
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            return PartialView("_MultipleTransferFailure");
                        }
                    }
                }
            }
            return PartialView("_MultipleTransferError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultipleTAPTransfer(MultipleTransModel tran)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                    tran.SourcePhone = phone;
                    var otp = $"{tran.One}{tran.Two}{tran.Three}{tran.Four}{tran.Five}{tran.Six}";
                    if (string.IsNullOrEmpty(otp) || otp.Length < 4)
                        return PartialView("_MultipleTransferFailure");
                   
                    var transaction = new MultiPinTransferModel
                    {
                        AuthType = tran.AuthType,
                        SourcePhone = tran.SourcePhone,
                        Pin = tran.Pin,
                        Tap = otp,
                        Rps = tran.Rps,
                        TransferCharge = tran.TransferCharge
                    };
                    var json = JsonConvert.SerializeObject(transaction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("MultipleTransfer", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            if (result.Resp.Count == 0)
                            {
                                return PartialView("_MultipleTransferSuccess", tran);
                            }
                            else
                            {
                                tran.RecipientCount = tran.RecipientCount - result.Resp.Count;
                                tran.Sum = tran.Sum - result.Resp.Sum(p => p.Amount);
                                ViewBag.Failed = result.Resp;
                                return PartialView("_MultipleTransferSuccess", tran);
                            }

                        }
                        else if (result.status == "fail")
                        {
                            var Status = new TransferStatus { Message = result.message };
                            return PartialView("_MultipleTransferFailure",Status);
                        }
                        else if (result.status == "failed")
                        {
                            return PartialView("_MultipleTransferFailure");
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            return PartialView("_MultipleTransferFailure");
                        }
                    }
                }
            }
            return PartialView("_MultipleTransferFailure");
        }




        


        public IActionResult Success()
        {
            return View();
        }

   
        public IActionResult Withdrawal()
        {
            //Remove this. Query source phone before sending
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            var gg = new WithdrawalModel { Phone = phone };
            return View(gg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdrawal(WithdrawalModel withdrawalModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(withdrawalModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("Withdraw", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return RedirectToAction("Success");
                        }
                        else if (result.status == "failed")
                        {
                            ModelState.AddModelError(string.Empty, result.message);
                            return View(withdrawalModel);
                        }
                        else if (result.status == "fail" && result.message == "Wrong Pin")
                        {
                            ModelState.AddModelError(string.Empty, result.message);
                            return View(withdrawalModel);
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }
            }
            return View(withdrawalModel);
        }

        public IActionResult Airtime()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            var gg = new AirtimeModel { SourcePhone = phone };
            return View(gg);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Airtime(AirtimeModel airtimeModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(airtimeModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("Airtime", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return RedirectToAction("Success");
                        }
                        else if (result.status == "success" && result.message == "Insuficient Balance")
                        {
                            ModelState.AddModelError(string.Empty, result.message);
                            return View(airtimeModel);
                        }
                        else if (result.status == "success" && result.message == "Wrong Pin")
                        {
                            ModelState.AddModelError(string.Empty, result.message);
                            return View(airtimeModel);
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }
            }
            return View(airtimeModel);
        }

        public IActionResult AddCard()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            var gg = new AddCardModel { Phone = phone };
            return View(gg);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCard(AddCardModel fundWalletModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(fundWalletModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("ChargeCard", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return RedirectToAction("Success");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }
            }
            return View(fundWalletModel);
        }


        public IActionResult FundWallet()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            var gg = new FundWalletModel { Phone = phone };
            return View(gg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FundWallet(FundWalletModel fm)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(fm);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.GetAsync($"FundWallet/{fm.Phone}/{fm.Amount}");
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);

                        if (result.status == "success")
                        {
                            return RedirectToAction("Success");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }
            }
            return View(fm);
        }

        


        public IActionResult UploadCSV(IFormFile file)
        {
            try
            {
                var res = new List<string>();
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                var phonee = $"{phone.Substring(phone.Length - 10)}";
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        res.Add(reader.ReadLine());
                }
                var j = ProcessFile(res);

                if (j.Any(p=>p.Amount<0))
                {
                    TransferStatus transferStatus = new TransferStatus { Message = "You entered  negative/invalid amount for one/more recipient,please check and reupload" };
                    return PartialView("_MultipleTransferError", transferStatus);
                }
                else if (j.Any(l => l.DestinationPhone == phonee))
                {
                    TransferStatus transferStatus = new TransferStatus { Message = "You entered your account as one of the recipient, please check and try again" };
                    return PartialView("_MultipleTransferError", transferStatus);
                }
                else if (j.Sum(p => p.Amount) >50000M)
                {
                    ViewBag.Sendingtome = "Transfer Limit of 50000 Exceeded,Reduce Amount";
                    return PartialView("_TransError");
                }

                var transModel = new MultipleTransModel();
                var json = JsonConvert.SerializeObject(j);
                transModel.Rps = json;
                transModel.TransferCharge = decimal.Parse(_configuration["Transaction:Charge"]);
                transModel.Sum = j.Sum(p => p.Amount);
                transModel.RecipientCount = j.Count;
                
                if (j.Sum(p => p.Amount) > 20000M)
                {
                    return PartialView("_MultipleTransferAuthType", transModel);
                }
                else
                {
                    transModel.AuthType = "PIN";
                    return PartialView("_MultiplePINAuthentication", transModel);
                }
            }
            catch (Exception ex)
            {
                TransferStatus transferStatus = new TransferStatus { Message = "Invalid File Format" };
                return PartialView("_MultipleTransferError", transferStatus);
            }
         
        }

        private static List<MultipleTransModel> ProcessFile(List<string> lines)
        {
          return lines.Skip(1).Where(p => p.Length > 1)
                .Select(ParseFromCsv).ToList();
        }

        private static MultipleTransModel ParseFromCsv(string arg)
        {
            var columns = arg.Split(",");

            decimal amount;
            if (!decimal.TryParse(columns[1],out amount))
            {
                return null;
            }

            long number;
            if (!long.TryParse(columns[0], out number))
            {
                return null;
            }

            return new MultipleTransModel
            {
                DestinationPhone = columns[0],
                Amount = decimal.Parse(columns[1])
            };
        }
    }
}