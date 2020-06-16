using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FlashMoneyApi.Data;
using FlashMoneyApi.DTOs;
using FlashMoneyApi.DTOs.InterSwitch;
using FlashMoneyApi.Models;
using FlashMoneyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace FlashMoneyApi.Controllers
{
    [Route("flashmoney")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IFlashHttpClient _flashMoneyHttpClient;
        private readonly ILogger<WalletController> _logger;

        private ApiDetail _apidetail;
        private HttpClient _client;
        private string mac;

        public WalletController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IOptions<ApiDetail> options, IConfiguration configuration,
             IFlashHttpClient flashMoneyHttpClient, ILogger<WalletController> logger)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _apidetail = options.Value;
            _logger = logger;
            _client = _flashMoneyHttpClient.GetClient();
            var text = $"{_apidetail.ClientId}:{_apidetail.ClientAPIKey}";
            var key = _apidetail.ClientSecretKey;
            var Iv = _apidetail.ClientIVKey;
            mac = GenericAes.encrypt(text, key, Iv);

        }




        [HttpPost("AddCard")]
        public async Task<IActionResult> ChargeCard([FromBody] CardDTO value)
        {
            return Ok(new { status = "success" });
            //var exp = value.ExpiryYear.Substring(value.ExpiryYear.Length - 2);
            //var expdate = $"{exp}{value.ExpiryMonth}";

            InterSwitchSetUp interSwitch = new InterSwitchSetUp(_configuration["InterSwitch:ClientId"], _configuration["InterSwitch:ClientSecret"]);

            string url = InterSwitchSetUp.getUrl(InterSwitchSetUp.SANDBOX);
            url = string.Concat(url, "/api/v3/purchases/validations");

            Token token = await interSwitch.GetClientAccessToken(_configuration["InterSwitch:ClientId"], _configuration["InterSwitch:ClientSecret"]);

            InterSwitchConfig config = new InterSwitchConfig("", url, _configuration["InterSwitch:ClientId"], _configuration["InterSwitch:ClientSecret"], token.access_token);

            using (HttpClient client = new HttpClient())
            {
                try
                {


                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", config.Authorization);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("Signature", config.Signature);
                    client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
                    client.DefaultRequestHeaders.Add("Timestamp", config.TimeStamp);
                    client.DefaultRequestHeaders.Add("Nonce", config.Nonce);

                    JObject jsonObject = new JObject();
                    jsonObject["transactionRef"] = Guid.NewGuid().ToString();
                    //jsonObject["authData"] = InterSwitchAuth.getAuthData(value.CardNumber, value.Pin, expdate, value.CVV);

                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PayStackReponseViewModel>(json);
                        if (result.data.status == "success")
                        {

                            var user = await _userManager.FindByNameAsync(value.SourcePhone);
                            if (user != null)
                            {
                                var cardOwner = await _context.User.Include(p => p.CardDetails).Include(p => p.Wallet).SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);

                                //var pcard = new CardDetail
                                //{
                                //    Id = Guid.NewGuid(),
                                //    CardExpMonth = value.ExpiryMonth,
                                //    CardExpYear = value.ExpiryYear,
                                //    CardName = value.CardHolder,
                                //    Token = result.data.authorization.authorization_code,
                                //    UserEmail = value.Email,
                                //    UserId = cardOwner.Id,
                                //    Valid = true,
                                //    CardNumber = $"************{result.data.authorization.last4}",
                                //    CardType = result.data.authorization.card_type
                                //};

                                //if (cardOwner.CardDetails.Count == 0)
                                //    pcard.Active = true;

                                //cardOwner.CardDetails.Add(pcard);
                                //cardOwner.Wallet.AvailableBalance += value.Amount;
                                //cardOwner.Wallet.CurrentBallance += value.Amount;

                                //_context.Update(cardOwner);

                                //var userTrans = new UserTransaction
                                //{
                                //    Id = Guid.NewGuid(),
                                //    UserId = cardOwner.Id,
                                //    TransactionReference = result.data.reference,
                                //    CardExpMonth = value.ExpiryMonth,
                                //    CardExpYear = value.ExpiryYear,
                                //    CardName = value.CardHolder,
                                //    CardNumber = value.CardNumber,
                                //    CVV = value.CVV,
                                //    TransactionDate = DateTime.Now,
                                //    UserEmail = value.Email,
                                //    IsAddCard = true,
                                //    Amount = value.Amount,
                                //    TransactionType = TransactionType.Debit,
                                //    TransactionStatus = TransactionStatus.Successful,
                                //    CardType = result.data.authorization.card_type
                                //};

                                //_context.Add(userTrans);

                                //await _context.SaveChangesAsync();

                                return Ok(new { status = "success" });
                            }
                        }
                        else
                        {
                            return BadRequest();
                        }

                    }
                }
                catch (Exception ex)
                {
                }
            }
            return BadRequest();
        }

        [HttpGet("QueryUserCard/{phone}")]
        public async Task<IActionResult> QueryUserCard(string phone)
        {

            var user = await _userManager.FindByNameAsync(phone);
            if (user == null)
                return BadRequest();

            var cardOwner = await _context.User.AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);
            if (cardOwner == null)
                return BadRequest();

            var cards = await _context.CardDetail.Where(p => p.UserId == cardOwner.Id && p.Deleted == false).ToListAsync();

            return Ok(new UserCardsDTO { Cards = cards });
        }
        [HttpGet("LogCardAttempt/{reference}/{phone}")]
        public async Task<IActionResult> LogCardAttempt(string reference, string phone)
        {
            var usermanager = await _userManager.FindByNameAsync(phone);
            var user = await _context.User.SingleOrDefaultAsync(k => k.ApplicationUserId == usermanager.Id);
            var cardaddprocess = new CardAddProcess { Reference = reference, UserId=user.Id, Phone=phone, CreatedAt=DateTime.Now };
            _context.Add(cardaddprocess);
            _context.SaveChanges();
            return Ok();

        }
        [HttpPost("CheckCard")]
        public async Task<IActionResult> CheckCard([FromBody] AddCardReferenceDTO cardRef)
        {
            var uri = $"https://api.paystack.co/transaction/verify/{cardRef.Reference}";
            PayStackReponseViewModel result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_test_627cc77c50babe181f84d993b38d62e9d3e7bbdb");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_live_2194ea3a8ba839bd99704ac7373e36b689392912");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<PayStackReponseViewModel>(json);
                    //result.data.customer.phone = phone;
                }
            }
            var sourcePhoneNumber = cardRef.SourcePhone;
            var usermanager = await _userManager.FindByNameAsync(sourcePhoneNumber);
            var user = await _context.User.SingleOrDefaultAsync(k => k.ApplicationUserId == usermanager.Id);
            if (result != null  && result.status && result.data.status=="success")
            {
                _logger.LogInformation("{user} successfully added new card via paystack reference {reference}", sourcePhoneNumber, result.data.reference);
                var namount = result.data.amount / 100;
                

                
                if (user != null)
                {
                    var newcard = new CardDetail
                    {
                        Active = true,
                        Token = result.data.authorization.authorization_code,
                        CardNumber = ($"**** **** **** {result.data.authorization.last4}"),
                        CardExpMonth = result.data.authorization.exp_month,
                        CardExpYear = result.data.authorization.exp_year,
                        CardType = result.data.authorization.card_type,
                        CardRef = result.data.reference,
                        CardEmail = result.data.customer.email,
                        UserId = user.Id,
                        CardName=user.FirstName+' '+user.LastName,
                        LastDebited= namount,
                        TransactionCount = 1
                };

                    _context.Add(newcard);

                    var wallet = await _context.Wallet.SingleOrDefaultAsync(p => p.UserId == user.Id);
                    wallet.AvailableBalance += namount;
                    wallet.CurrentBallance += namount;
                    user.LastWalletFundedDate = DateTime.Now;
                    var cardaddprocess = _context.CardAddProcess.SingleOrDefault(p => p.IsProcessed == false && p.Reference == result.data.reference && p.UserId == user.Id);
                    if (cardaddprocess != null)
                    {
                        cardaddprocess.IsProcessed = true;
                        cardaddprocess.CardId = result.data.authorization.last4;
                        cardaddprocess.Amount = namount;
                        cardaddprocess.Status = true;
                        _context.Update(cardaddprocess);
                    }
                    var userTrans = new UserTransaction
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        TransactionReference = result.data.reference,
                        CardExpMonth = newcard.CardExpMonth,
                        CardExpYear = newcard.CardExpYear,
                        CardName = newcard.CardName,
                        CardNumber = newcard.CardNumber,
                        CVV = newcard.CVV,
                        TransactionDate = DateTime.Now,
                        UserEmail = user.Email,
                        Amount = namount,
                        TransactionType = TransactionType.Debit,
                        TransactionStatus = TransactionStatus.Successful,
                        CardType = newcard.CardType,
                        IsAddCard = true
                    };

                    var history = new TransactionHistory
                    {
                        Id = Guid.NewGuid(),
                        Amount = namount,
                        Date = DateTime.Now,
                        Phone = sourcePhoneNumber,
                        UserId = user.Id,
                        HistOwnerId=user.Phone,
                        TransactionType = TransactionType.Credit,
                        Status = Status.Waiting,
                        Description = "Wallet Funded",
                        CardExpMonth = newcard.CardExpMonth,
                        CardExpYear = newcard.CardExpYear,
                        CardName = newcard.CardName,
                        CardNumber = newcard.CardNumber,
                        CardType = newcard.CardType,
                        CardId = newcard.Id.ToString()
                    };
                    var activi = new ActivityModel
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        UserId = user.Phone,
                        Type = TransactionType.Credit,
                        Description = $"You funded your wallet with {namount.ToString("c", new CultureInfo("ha-Latn-NG"))}"
                    };
                    
                    _context.Add(activi);
                    _context.Add(userTrans);
                    _context.Add(history);
                    _context.Update(wallet);
                    _context.SaveChanges();

                    
                    var authh = new JObject();
                    authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                    authh["clientIdField"] = _apidetail.ClientId;
                    authh["mACField"] = mac;
                    var infoo = new JObject();
                    infoo["auth"] = authh;
                    infoo["requestId"] = RandomRequestString(10);
                    infoo["paymentMode"] = RandomRequestString(6);
                    infoo["destinationPhone"] = sourcePhoneNumber;
                    infoo["sourcePhone"] = "2347039400381"; // This is constant
                    infoo["amount"] = namount.ToString();
                    infoo["transferCharges"] = "50";
                    infoo["Naration"] = "Takecharge";

                    var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                    var responsee = await _client.PostAsync("Account2WalletNAccount", contentt);
                    if (responsee.IsSuccessStatusCode)
                    {
                        JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());
                        if ((string)custt["ResponseCode"] != "00")
                        {
                            _logger.LogInformation("Unable to fund {user} wallet with N{amount}: {Reason}", sourcePhoneNumber, namount, (string)custt["ResponseMessage"]);
                            return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                        }
                        else
                        {
                            _logger.LogInformation("{user} wallet was successfully funded with N{amount}", sourcePhoneNumber, namount);
                            history.Status = Status.Completed;
                            _context.Update(history);
                            _context.SaveChanges();
                        } 
                    }
                    else
                    {
                        _logger.LogInformation("Unable to fund {user} wallet with N({amount}): {Reason}", sourcePhoneNumber, namount, "Bad Request");
                    }

                    //_logger.LogInformation("{user} successfully added new card to database and updated wallet ballance with paystack reference{reference}", response.data.customer.phone, response.data.reference);
                    return Ok();
                }
                else
                {
                    _logger.LogInformation("{user} Unsuccessfully tried to add card paystack referencer = {ref} ", sourcePhoneNumber, cardRef.Reference);
                    return Ok();
                }
            
            }
            else
            {
                if (user != null)
                {
                    var cardaddprocess = _context.CardAddProcess.SingleOrDefault(p => p.IsProcessed == false && p.Reference == cardRef.Reference && p.UserId == user.Id);
                    if (cardaddprocess != null)
                    {
                        cardaddprocess.IsProcessed = true;
                        //cardaddprocess.Status = false;
                        _context.Update(cardaddprocess);
                        _context.SaveChanges();
                    }
                }
            }
            _logger.LogInformation("{user} Unsuccessfully to add card to database, paystack referencer = {ref} ", cardRef.SourcePhone, cardRef.Reference);
            return BadRequest();
              
           
        }


        [HttpPost("FundWallet")]
        public async Task<IActionResult> FundWallet([FromBody] TransferDTO value)
        {

            //Verify that CardId belong to the user

            if (string.IsNullOrEmpty(value.SourcePhone))
                return BadRequest();

            var uri = "https://api.paystack.co/transaction/charge_authorization";

            
            var user = await _userManager.FindByNameAsync(value.SourcePhone);
            if (user == null)
                return BadRequest();
            var cardOwner = await _context.User.Include(p => p.CardDetails).Include(p => p.Wallet).SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            if (cardOwner == null || cardOwner.CardDetails.Count == 0)
                return BadRequest();

           // var activeCard = await _context.CardDetail.SingleOrDefaultAsync(p => p.UserId == cardOwner.Id && p.Active == true);
           var activeCard = await _context.CardDetail.SingleOrDefaultAsync(p => p.UserId == cardOwner.Id  && p.Id==value.CardId);

            if (activeCard == null)
                return BadRequest();

            JObject jsonObject = new JObject();
            jsonObject["authorization_code"] = activeCard.Token;
            jsonObject["email"] = activeCard.CardEmail;  //cardOwner.Email;
            if (decimal.Parse(_configuration["Transaction:TransactionFee"]) > 0)
            {
                jsonObject["amount"] = (((
                    decimal)value.Amount + decimal.Parse(_configuration["Transaction:TransactionFee"])) * 100).ToString();
            }
            else
            {
                jsonObject["amount"] = (((decimal)value.Amount) * 100).ToString();
            }
               
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_test_627cc77c50babe181f84d993b38d62e9d3e7bbdb");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_live_2194ea3a8ba839bd99704ac7373e36b689392912");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri, content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PayStackReponseViewModel>(json);
                        if (result.data.status == "success")
                        {
                            cardOwner.Wallet.AvailableBalance += value.Amount;
                            cardOwner.Wallet.CurrentBallance += value.Amount;
                            cardOwner.LastWalletFundedDate = DateTime.Now;
                            activeCard.LastDebited = value.Amount;
                            activeCard.TransactionCount += 1;
                            _context.Update(activeCard);
                            _context.Update(cardOwner);

                            var userTrans = new UserTransaction
                            {
                                Id = Guid.NewGuid(),
                                UserId = cardOwner.Id,
                                TransactionReference = result.data.reference,
                                CardExpMonth = activeCard.CardExpMonth,
                                CardExpYear = activeCard.CardExpYear,
                                CardName = activeCard.CardName,
                                CardNumber = activeCard.CardNumber,
                                CVV = activeCard.CVV,
                                TransactionDate = DateTime.Now,
                                UserEmail = cardOwner.Email,
                                Amount = value.Amount,
                                TransactionType = TransactionType.Debit,
                                TransactionStatus = TransactionStatus.Successful,
                                CardType = activeCard.CardType
                            };

                            var history = new TransactionHistory
                            {
                                Id = Guid.NewGuid(),
                                Amount = value.Amount,
                                Date = DateTime.Now,
                                Phone = value.SourcePhone,
                                UserId = cardOwner.Id,
                                HistOwnerId=cardOwner.Phone,
                                TransactionType = TransactionType.Credit,
                                Status = Status.Waiting,
                                Description = "Wallet Funded",
                                CardExpMonth = activeCard.CardExpMonth,
                                CardExpYear = activeCard.CardExpYear,
                                CardName = activeCard.CardName,
                                CardNumber = activeCard.CardNumber,
                                CardType = activeCard.CardType,
                                CardId = activeCard.Id.ToString()
                            };
                            var activi = new ActivityModel
                            {
                                Id = Guid.NewGuid(),
                                Date = DateTime.Now,
                                UserId = cardOwner.Phone,
                                Type = TransactionType.Credit,
                                Description = $"You funded your wallet with {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))}"
                            };
                            if (decimal.Parse(_configuration["Transaction:TransactionFee"])>0)
                            {
                                var chargehistory = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = decimal.Parse(_configuration["Transaction:TransactionFee"]),
                                    Date = DateTime.Now,
                                    Phone = value.SourcePhone,
                                    UserId = cardOwner.Id,
                                    HistOwnerId = cardOwner.Phone,
                                    TransactionType = TransactionType.Debit,
                                    Status = Status.Completed,
                                    Description = "Fund Wallet Charge",
                                    //DestinationPhone = phonee,
                                    Receipient = "Charge"

                                };
                                var chargeactivi = new ActivityModel
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    UserId = cardOwner.Phone,
                                    Type = TransactionType.Debit,
                                    Description = $"Fund Wallet Charge of {decimal.Parse(_configuration["Transaction:TransactionFee"]).ToString("c", new CultureInfo("ha-Latn-NG"))} was deducted from your card"
                                };
                                _context.Add(chargeactivi);
                                _context.Add(chargehistory);
                            }
                            _context.Add(activi);
                            _context.Add(userTrans);
                            _context.Add(history);
                            await _context.SaveChangesAsync();

                            var namount = result.data.amount / 100;
                            var authh = new JObject();
                            authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                            authh["clientIdField"] = _apidetail.ClientId;
                            authh["mACField"] = mac;
                            var infoo = new JObject();
                            infoo["auth"] = authh;
                            infoo["requestId"] = RandomRequestString(10);
                            infoo["paymentMode"] = RandomRequestString(6);
                            infoo["destinationPhone"] = value.SourcePhone;
                            infoo["sourcePhone"] = "2347039400381"; // This is constant
                            infoo["amount"] = namount.ToString();
                            infoo["transferCharges"] = "0";
                            infoo["Naration"] = "Takecharge";

                            var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                            var responsee = await _client.PostAsync("Account2WalletNAccount", contentt);
                            if (responsee.IsSuccessStatusCode)
                            {
                                JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());
                                if ((string)custt["ResponseCode"] != "00")
                                {
                                    _logger.LogInformation("Unable to fund {user} wallet with N{amount}: {Reason}", value.SourcePhone, namount, (string)custt["ResponseMessage"]);
                                    return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                                }
                                else
                                {
                                    _logger.LogInformation("{user} wallet was successfully funded with N{amount}", value.SourcePhone, namount);
                                    history.Status = Status.Completed;
                                    _context.Update(history);
                                    _context.SaveChanges();
                                }
                            }
                            else
                            {
                                _logger.LogInformation("Unable to fund {user} wallet with N({amount}): {Reason}", value.SourcePhone, namount, "Bad Request");
                            }

                            return Ok(new { status = "success" });
                        }
                        else
                        {
                             return BadRequest();
                            
                        }

                    }
                }
            }catch(Exception ex)
            {
                _logger.LogInformation("Exception occured: {Reason}", ex.Message);
            }
            return BadRequest();
        }

        private static Random randomm = new Random();
        string RandomRequestString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[randomm.Next(s.Length)]).ToArray());
        }

        [HttpGet("HistoryById/{content}/{Phone}")]
        public async Task<IActionResult> HistoryById( string content, string Phone)
        {
            _logger.LogInformation("{user} Requested for Transaction History", Phone);
            var user = await _userManager.FindByNameAsync(Phone);
            if (user == null) { return BadRequest(); }
            var Id= Guid.Parse(content);
            var trans = await _context.TransactionHistory.SingleOrDefaultAsync(p => p.Id == Id);
            if (trans.Description == "Transfer")
            {
                return Ok(new { status = "Transfer", Transaction = trans });
            }
            else if (trans.Description == "Withdrawal")
            {
                return Ok(new { status = "Withdrawal", Transaction = trans });
            }
            else
            {
                return Ok(new { status = "Wallet Funded", Transaction = trans });
            }
        }


        [HttpGet("MakeCardPrimary/{CardId}/{Phone}")]
        public async Task<IActionResult> MakeCardPimary(Guid CardId, string Phone)
        {
            var user = await _userManager.FindByNameAsync(Phone);
            if (user == null || CardId == null)
                return BadRequest();

            var Owner = await _context.User.AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            if (CardId == null || string.IsNullOrEmpty(Phone))
                return BadRequest();
            try
            {
                var card = await _context.CardDetail.SingleOrDefaultAsync(p => p.UserId == Owner.Id && p.Active == true);
                if (card != null)
                {
                    card.Active = false;
                    _context.Update(card);
                    var cards = await _context.CardDetail.SingleOrDefaultAsync(p => p.Id == CardId && p.UserId == Owner.Id);
                    if (cards != null)
                    {
                        cards.Active = true;
                        _context.Update(cards);
                    }
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    var cs = await _context.CardDetail.SingleOrDefaultAsync(p => p.Id == CardId && p.UserId == Owner.Id);
                    if (cs != null)
                    {
                        cs.Active = true;
                        _context.Update(cs);
                    }
                    await _context.SaveChangesAsync();
                    return Ok();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("GetCards/{phone}")]
        public async Task<IActionResult> GetCards(string phone)
        {

            var user = await _userManager.FindByNameAsync(phone);
            if (user != null)
            {
                var cardOwner = await _context.User.Include(p => p.CardDetails).Include(p => p.Wallet).SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);
                return Ok(cardOwner.CardDetails);
            }
            return NotFound();
        } 

        [HttpDelete("Card/{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (Id == null)
                return BadRequest();

            var card = await _context.CardDetail.SingleOrDefaultAsync(p => p.Id == Id);
            if (card == null)
                return NotFound();

            card.Deleted = true;

            try
            {
                _context.Update(card);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
