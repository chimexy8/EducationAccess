using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FlashMoneyApi.Data;
using FlashMoneyApi.DTOs;
using FlashMoneyApi.Models;
using FlashMoneyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlashMoneyApi.Controllers
{
    [Route("flashmoney")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFlashHttpClient _flashMoneyHttpClient;
        private IConfiguration _configuration;
        private ApiDetail _apidetail;
        private ILogger<TransferController> _logger;
        private HttpClient _client;
        private string mac;
        public TransferController(ApplicationDbContext context, ILogger<TransferController> logger, UserManager<ApplicationUser> userManager,IConfiguration configuration,
            IOptions<ApiDetail> options, IFlashHttpClient flashMoneyHttpClient)
        {
            _context = context;
            _userManager = userManager;
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _apidetail = options.Value;
            _configuration = configuration;
            _logger = logger;
            _client = _flashMoneyHttpClient.GetClient();
            var text = $"{_apidetail.ClientId}:{_apidetail.ClientAPIKey}";
            var key = _apidetail.ClientSecretKey;
            var Iv = _apidetail.ClientIVKey;
            mac = GenericAes.encrypt(text, key, Iv);
        }

        private static Random random = new Random();
        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }
        private static Random randomm = new Random();
        string RandomRequestString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[randomm.Next(s.Length)]).ToArray());
        }


        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO value)
        {
            _logger.LogInformation("{user} initiated a transfer of {amount} to {reciever}", value.SourcePhone, value.Amount, value.DestinationPhone);

            var phonee = $"234{value.DestinationPhone.Substring(value.DestinationPhone.Length - 10)}";
            var senderphone = $"234{value.SourcePhone.Substring(value.SourcePhone.Length - 10)}";

            var receiver = await _userManager.FindByNameAsync(phonee);
            var sender = await _userManager.FindByNameAsync(senderphone);
            var receipient = "";
            var sendingUser = await _context.User.Include(p=>p.Wallet).AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);
            User receivingUser = null;
            if (receiver != null)
                receivingUser = await _context.User.Include(p => p.Wallet).AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == receiver.Id);
            if (receivingUser != null)
                receipient = $"{receivingUser.FirstName} {receivingUser.LastName}";

            if (value.AuthType == "PIN")
            {
                var authh = new JObject();
                authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authh["clientIdField"] = _apidetail.ClientId;
                authh["mACField"] = mac;
                var infoo = new JObject();
                infoo["auth"] = authh;
                infoo["requestId"] = RandomRequestString(10);
                infoo["paymentMode"] = RandomRequestString(6);
                infoo["sourcePhone"] = senderphone;
                infoo["pin"] = value.Pin;

                var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                var responsee = await _client.PostAsync("ValidatePIN", contentt);
                if (responsee.IsSuccessStatusCode)
                {
                    JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());
                    if ((string)custt["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} PIN was not verified for transfer : {Reason}", value.SourcePhone, (string)custt["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} PIN was verified for transfer", value.SourcePhone);

                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (value.AuthType == "OTP")
            {
                var authh = new JObject();
                authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authh["clientIdField"] = _apidetail.ClientId;
                authh["mACField"] = mac;
                var infoo = new JObject();
                infoo["auth"] = authh;
                infoo["requestId"] = RandomRequestString(10);
                infoo["paymentMode"] = RandomRequestString(6);
                infoo["sourcePhone"] = senderphone;
                infoo["pin"] = value.Pin;

                var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                var responsee = await _client.PostAsync("ValidatePIN", contentt);
                if (responsee.IsSuccessStatusCode)
                {
                    JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                    if ((string)custt["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} PIN was not verified for transfer : {Reason}", value.SourcePhone, (string)custt["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} PIN was verified for transfer", value.SourcePhone);
                    }
                }
                else
                {
                    return BadRequest();
                }

                var valid = await _context.OTPValidation.SingleOrDefaultAsync(p => p.Code == value.Otp && p.Phone == value.SourcePhone && p.Date.AddMinutes(10) >= DateTime.Now);
                if (valid == null)
                {
                    _logger.LogInformation("{user} OTP was not verified for transfer", value.SourcePhone);
                    return Ok(new { status = "fail", message = "OTP was not verified for transfer" });
                }
            }
            else if (value.AuthType == "TAP")
            {

                var authh = new JObject();
                authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authh["clientIdField"] = _apidetail.ClientId;
                authh["mACField"] = mac;
                var infoo = new JObject();
                infoo["auth"] = authh;
                infoo["requestId"] = RandomRequestString(10);
                infoo["paymentMode"] = RandomRequestString(6);
                infoo["sourcePhone"] = senderphone;
                infoo["pin"] = value.Pin;

                var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                var responsee = await _client.PostAsync("ValidatePIN", contentt);
                if (responsee.IsSuccessStatusCode)
                {
                    JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                    if ((string)custt["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} PIN was not verified for transfer : {Reason}", value.SourcePhone, (string)custt["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} PIN was verified for transfer", value.SourcePhone);
                    }
                }
                else
                {
                    return BadRequest();
                }

                var authhh = new JObject();
                authhh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authhh["clientIdField"] = _apidetail.ClientId;
                authhh["mACField"] = mac;
                var infooo = new JObject();
                infooo["auth"] = authhh;
                infooo["requestId"] = RandomRequestString(10);
                infooo["paymentMode"] = RandomRequestString(6);
                infooo["sourcePhone"] = value.SourcePhone;
                infooo["pin"] = value.Tap;


                var _content = new StringContent(infooo.ToString(), Encoding.UTF8, "application/json");
                var _response = await _client.PostAsync("Authenticate2FA", _content);
                if (_response.IsSuccessStatusCode)
                {
                    JObject _cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                    if ((string)_cust["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} TAP was not verified for transfer : {Reason}", value.SourcePhone, (string)_cust["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)_cust["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} TAP was verified for transfer", value.SourcePhone);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }



            UserCheckInfo usercheckresult = null;
            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;

            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["mobileNo"] = phonee;


            var content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("CustomerInfo", content);
            if (response.IsSuccessStatusCode)
            {
                JObject cust = JObject.Parse(await response.Content.ReadAsStringAsync());

                if ((string)cust["ResponseCode"] == "00")
                {
                    usercheckresult = new UserCheckInfo
                    {
                        FirstName = (string)cust["Firstname"],
                        LastName = (string)cust["Lastname"],
                        DOB = (string)cust["DOB"],
                        Gender = (string)cust["Gender"],
                        Balance = (decimal)cust["Balance"],
                        Status = "success",
                        ReasonPhrase = "Existing User",
                    };

                }
            }
            else
            {
                return BadRequest();
            }

            if (usercheckresult == null)
            {
                _logger.LogInformation("{user} Attempting to transfer money to a new user {newuser}", value.SourcePhone, value.DestinationPhone);
                auth = new JObject();
                auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                auth["clientIdField"] = _apidetail.ClientId;
                auth["mACField"] = mac;
                info = new JObject();
                info["auth"] = auth;
                info["requestId"] = RandomRequestString(10);
                info["paymentMode"] = RandomRequestString(6);
                info["sourcePhone"] = phonee;

                var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                var _response = await _client.PostAsync("EnrollFlashcashCustomer", _content);
                if (_response.IsSuccessStatusCode)
                {
                    JObject cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                    if ((string)cust["ResponseCode"] == "00")
                    {

                        _logger.LogInformation("{user} New Reciepient of {amount} from {sender} enrolled", value.DestinationPhone, value.Amount, value.SourcePhone);
                        if (sendingUser.Wallet.AvailableBalance < value.Amount)
                        {

                            _logger.LogError("{amount} could not be transfered to {recipient} by {user} : Insufficient Fund", value.Amount, value.DestinationPhone, value.SourcePhone);
                            return Ok(new { status = "fail", message = "Insufficient Fund" });
                        }
                        var charge = value.Amount + decimal.Parse(_configuration["Transaction:Charge"]);
                        auth = new JObject();
                        auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                        auth["clientIdField"] = _apidetail.ClientId;
                        auth["mACField"] = mac;
                        info = new JObject();
                        info["auth"] = auth;
                        info["requestId"] = RandomRequestString(10);
                        info["sourcePhone"] = senderphone;
                        info["paymentMode"] = RandomRequestString(6); ;
                        info["destinationPhone"] = phonee;
                        info["amount"] = charge.ToString();
                        info["Naration"] = value.Narration;

                        _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                        _response = await _client.PostAsync("Wallet2Wallet", _content);
                        if (_response.IsSuccessStatusCode)
                        {
                            JObject custr = JObject.Parse(await _response.Content.ReadAsStringAsync());

                            if ((string)custr["ResponseCode"] == "00")
                            {
                                _logger.LogInformation("{Amount} successfully transfered to {recipient} by {sender}", value.Amount, value.DestinationPhone, value.SourcePhone);
                                var trans = new Transfer
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = value.Amount,
                                    SenderId = sendingUser.Id,
                                    Claimed = true,
                                    Narration = value.Narration,
                                    SendDate = DateTime.Now,
                                    SenderPhone = senderphone,
                                    ReceiverPhone = phonee,
                                };

                                var history = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = value.Amount,
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                    UserId = sendingUser.Id,
                                    HistOwnerId=sendingUser.Phone,
                                    TransactionType = TransactionType.Debit,
                                    Status = Status.Completed,
                                    Description = "Transfer",
                                    DestinationPhone = phonee,
                                    Receipient = receipient
                                   
                                };
                                if (decimal.Parse(_configuration["Transaction:Charge"])>0)
                                {
                                    var chargehistory = new TransactionHistory
                                    {
                                        Id = Guid.NewGuid(),
                                        Amount = decimal.Parse(_configuration["Transaction:Charge"]),
                                        Date = DateTime.Now,
                                        Phone = senderphone,
                                        UserId = sendingUser.Id,
                                        HistOwnerId = sendingUser.Phone,
                                        TransactionType = TransactionType.Debit,
                                        Status = Status.Completed,
                                        Description = "Transfer Charge",
                                        DestinationPhone = phonee,
                                        Receipient = receipient

                                    };
                                    var chargeactivi = new ActivityModel
                                    {
                                        Id = Guid.NewGuid(),
                                        Date = DateTime.Now,
                                        UserId = sendingUser.Phone,
                                        Type = TransactionType.Debit,
                                        Description = $"Transfer charge of {decimal.Parse(_configuration["Transaction:Charge"]).ToString("c", new CultureInfo("ha-Latn-NG"))} was deducted for transfer to {phonee}"
                                    };
                                    _context.Add(chargeactivi);
                                    _context.Add(chargehistory);
                                }
                                
                                var receiverhistory = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = value.Amount,
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                   
                                    HistOwnerId = phonee,
                                    TransactionType = TransactionType.Credit,
                                    Status = Status.Completed,
                                    Description = "Credit",
                                    DestinationPhone = phonee,
                                    Receipient = sendingUser.FirstName + "" + sendingUser.LastName,
                                    ReceipientPassport = sendingUser.Passport

                                };
                                
                                _context.Add(receiverhistory);
                               
                                var activi = new ActivityModel
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    UserId = sendingUser.Phone,
                                    Type = TransactionType.Debit, 
                                    Description = $"You transferred {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} to {phonee}"
                                };
                                var receiveractivi = new ActivityModel
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    UserId = phonee,
                                    Type = TransactionType.Debit, 
                                    Description = $"You received {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} from {sendingUser.Phone}"
                                };
                                _context.Add(receiveractivi);
                                _context.Add(activi);
                               
                               

                                sendingUser.Wallet.AvailableBalance -= value.Amount;
                                sendingUser.Wallet.CurrentBallance -= value.Amount;
                                if (receivingUser!=null)
                                {
                                    receivingUser.Wallet.AvailableBalance += value.Amount;
                                    receivingUser.Wallet.CurrentBallance += value.Amount;
                                    _context.Update(receivingUser);
                                }

                                _context.Update(sendingUser);
                                _context.Add(trans);
                                _context.Add(history);
                                await _context.SaveChangesAsync();

                                return Ok(new { status = "success", message = "Existing User" });
                            }
                            else /*if ((string)cust["ResponseCode"] == "52")*/
                            {
                               
                                    _logger.LogError("{amount} could not be transfered to {recipient} by {user} : {Reason}", value.Amount, value.DestinationPhone, value.SourcePhone, (string)custr["ResponseMessage"]);
                                    return Ok(new { status = "fail", message = (string)custr["ResponseMessage"] });
                                
                               
                            }
                        }
                    }
                    else
                    {

                        _logger.LogError("{Recipient} New user could not be enrolled for transfer: {Reason}", value.DestinationPhone, (string)cust["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)cust["ResponseMessage"] });
                    }
                }

            }
            else
            {
                if (sendingUser.Wallet.AvailableBalance < value.Amount)
                {

                    _logger.LogError("{amount} could not be transfered to {recipient} by {user} : Insufficient Fund", value.Amount, value.DestinationPhone, value.SourcePhone);
                    return Ok(new { status = "fail", message = "Insufficient Fund" });
                }
                var charge = value.Amount + decimal.Parse(_configuration["Transaction:Charge"]);
                auth = new JObject();
                auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                auth["clientIdField"] = _apidetail.ClientId;
                auth["mACField"] = mac;

                info = new JObject();
                info["auth"] = auth;
                info["requestId"] = RandomRequestString(10);
                info["sourcePhone"] = senderphone;
                info["paymentMode"] = RandomRequestString(6);
                info["destinationPhone"] = phonee;
                info["amount"] = charge.ToString();
                info["Naration"] = value.Narration;

                var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                var _response = await _client.PostAsync("Wallet2Wallet", _content);
                if (_response.IsSuccessStatusCode)
                {
                    JObject cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                    if ((string)cust["ResponseCode"] == "00")
                    {
                        _logger.LogInformation("{Amount} successfully transfered to {recipient} by {sender}", value.Amount, value.DestinationPhone, value.SourcePhone);

                        var trans = new Transfer
                        {
                            Id = Guid.NewGuid(),
                            Amount = value.Amount,
                            SenderId = sendingUser.Id,
                            Claimed = true,
                            Narration = value.Narration,
                            SendDate = DateTime.Now,
                            SenderPhone = senderphone,
                            ReceiverPhone = phonee,

                        };
                        if (receivingUser==null)
                        {
                            var history = new TransactionHistory
                            {
                                Id = Guid.NewGuid(),
                                Amount = value.Amount,
                                Date = DateTime.Now,
                                Phone = senderphone,
                                UserId = sendingUser.Id,
                                HistOwnerId = sendingUser.Phone,
                                TransactionType = TransactionType.Debit,
                                Status = Status.Completed,
                                Description = "Transfer",
                                DestinationPhone = phonee,
                                Receipient = receipient,
                                //ReceipientPassport = receivingUser.Passport
                            };
                            _context.Add(history);
                        }
                        else
                        {
                            var history = new TransactionHistory
                            {
                                Id = Guid.NewGuid(),
                                Amount = value.Amount,
                                Date = DateTime.Now,
                                Phone = senderphone,
                                UserId = sendingUser.Id,
                                HistOwnerId = sendingUser.Phone,
                                TransactionType = TransactionType.Debit,
                                Status = Status.Completed,
                                Description = "Transfer",
                                DestinationPhone = phonee,
                                Receipient = receipient,
                                ReceipientPassport = receivingUser.Passport
                            };
                            _context.Add(history);
                        }
                        
                        if (decimal.Parse(_configuration["Transaction:Charge"]) > 0)
                        {
                            var chargehistory = new TransactionHistory
                            {
                                Id = Guid.NewGuid(),
                                Amount = decimal.Parse(_configuration["Transaction:Charge"]),
                                Date = DateTime.Now,
                                Phone = senderphone,
                                UserId = sendingUser.Id,
                                HistOwnerId = sendingUser.Phone,
                                TransactionType = TransactionType.Debit,
                                Status = Status.Completed,
                                Description = "Transfer Charge",
                                DestinationPhone = phonee,
                                Receipient = receipient

                            };
                            _context.Add(chargehistory);
                            var chargeactivi = new ActivityModel
                            {
                                Id = Guid.NewGuid(),
                                Date = DateTime.Now,
                                UserId = sendingUser.Phone,
                                Type = TransactionType.Debit,
                                Description = $"Transfer charge of {decimal.Parse(_configuration["Transaction:Charge"]).ToString("c", new CultureInfo("ha-Latn-NG"))} was deducted for transfer to {phonee}"
                            };
                            _context.Add(chargeactivi);
                        }
                        if (receivingUser != null)
                        {
                            var receiverhistory = new TransactionHistory
                            {
                                Id = Guid.NewGuid(),
                                Amount = value.Amount,
                                Date = DateTime.Now,
                                Phone = senderphone,
                                UserId = receivingUser.Id,
                                HistOwnerId=receivingUser.Phone,
                                TransactionType = TransactionType.Credit,
                                Status = Status.Completed,
                                Description = "Credit",
                                DestinationPhone = phonee,
                                Receipient = sendingUser.FirstName+ ""+ sendingUser.LastName ,
                                ReceipientPassport = sendingUser.Passport

                            };
                            var receiveractivity = new ActivityModel
                            {
                                Id = Guid.NewGuid(),
                                Date = DateTime.Now,
                                UserId = receivingUser.Phone,
                                Type = TransactionType.Credit,
                                Description = $"You received {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} from {senderphone}"
                            };
                            _context.Add(receiverhistory);
                            _context.Add(receiveractivity);
                        }
                        else
                        {
                            var receiverhistory = new TransactionHistory
                            {
                                Id = Guid.NewGuid(),
                                Amount = value.Amount,
                                Date = DateTime.Now,
                                Phone = senderphone,
                                UserId = Guid.NewGuid(),
                                HistOwnerId = phonee,
                                TransactionType = TransactionType.Credit,
                                Status = Status.Completed,
                                Description = "Credit",
                                DestinationPhone = phonee,
                                Receipient = sendingUser.FirstName + "" + sendingUser.LastName,
                                ReceipientPassport = sendingUser.Passport

                            };
                            var receiveractivity = new ActivityModel
                            {
                                Id = Guid.NewGuid(),
                                Date = DateTime.Now,
                                UserId = phonee,
                                Type = TransactionType.Credit,
                                Description = $"You received {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} from {senderphone}"
                            };
                            _context.Add(receiverhistory);
                            _context.Add(receiveractivity);
                        }

                        var activi = new ActivityModel
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Now,
                            UserId = sendingUser.Phone,
                            Type = TransactionType.Debit,
                            Description = $"You transferred {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} to {phonee}"
                        };
                        sendingUser.Wallet.AvailableBalance -= value.Amount;
                        sendingUser.Wallet.CurrentBallance -= value.Amount;
                        if (receivingUser != null)
                        {
                            receivingUser.Wallet.AvailableBalance += value.Amount;
                            receivingUser.Wallet.CurrentBallance += value.Amount;
                            _context.Update(receivingUser);
                        }
                        _context.Update(sendingUser);
                        _context.Add(activi);
                        _context.Add(trans);
                        
                        await _context.SaveChangesAsync();

                        return Ok(new { status = "success", message = "Existing User" });
                    }
                    else /*if ((string)cust["ResponseCode"] == "52")*/
                    {
                       
                            _logger.LogError("{amount} could not be transfered to {recipient} by {user} : {Reason}", value.Amount, value.DestinationPhone, value.SourcePhone, (string)cust["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)cust["ResponseMessage"] });


                    }
                    //else
                    //{
                    //    _logger.LogError("{amount} could not be transfered to {recipient} by {user} : {Reason}", value.Amount, value.DestinationPhone, value.SourcePhone, (string)cust["ResponseMessage"]);
                    //    return Ok(new { status = "fail", message = (string)cust["ResponseMessage"] });
                    //}
                }

            }
            return BadRequest();
        }



        [HttpPost("MultipleTransfer")]
        public async Task<IActionResult> MultipleTransfer([FromBody] MultiTransferDTO value)
        {
            //var phonee = $"234{value.DestinationPhone.Substring(value.DestinationPhone.Length - 10)}";
            var senderphone = $"234{value.SourcePhone.Substring(value.SourcePhone.Length - 10)}";


            var sender = await _userManager.FindByNameAsync(senderphone);
            var sendingUser = await _context.User.Include(p=>p.Wallet).AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);


            if (value.AuthType == "PIN")
            {
                var authh = new JObject();
                authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authh["clientIdField"] = _apidetail.ClientId;
                authh["mACField"] = mac;
                var infoo = new JObject();
                infoo["auth"] = authh;
                infoo["requestId"] = RandomRequestString(10);
                infoo["paymentMode"] = RandomRequestString(6);
                infoo["sourcePhone"] = senderphone;
                infoo["pin"] = value.Pin;

                var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                var responsee = await _client.PostAsync("ValidatePIN", contentt);
                if (responsee.IsSuccessStatusCode)
                {
                    JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                    if ((string)custt["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} PIN was not verified for transfer : {Reason}", value.SourcePhone, (string)custt["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} PIN was verified for transfer", value.SourcePhone);
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            else if (value.AuthType == "OTP")
            {
                var authh = new JObject();
                authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authh["clientIdField"] = _apidetail.ClientId;
                authh["mACField"] = mac;
                var infoo = new JObject();
                infoo["auth"] = authh;
                infoo["requestId"] = RandomRequestString(10);
                infoo["paymentMode"] = RandomRequestString(6);
                infoo["sourcePhone"] = senderphone;
                infoo["pin"] = value.Pin;

                var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                var responsee = await _client.PostAsync("ValidatePIN", contentt);
                if (responsee.IsSuccessStatusCode)
                {
                    JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                    if ((string)custt["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} PIN was not verified for transfer : {Reason}", value.SourcePhone, (string)custt["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} PIN was verified for transfer", value.SourcePhone);
                    }
                }
                else
                {
                    return BadRequest();
                }

                var valid = await _context.OTPValidation.SingleOrDefaultAsync(p => p.Code == value.Otp && p.Phone == value.SourcePhone && p.Date.AddMinutes(10) >= DateTime.Now);
                if (valid == null)
                {
                    _logger.LogInformation("{user} OTP was not verified for transfer", value.SourcePhone);
                    return Ok(new { status = "fail", message = "Wrong OTP" });
                }
                else
                {
                    _logger.LogInformation("{user} OTP was verified for transfer", value.SourcePhone);
                }
            }
            else if (value.AuthType == "TAP")
            {

                var authh = new JObject();
                authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authh["clientIdField"] = _apidetail.ClientId;
                authh["mACField"] = mac;
                var infoo = new JObject();
                infoo["auth"] = authh;
                infoo["requestId"] = RandomRequestString(10);
                infoo["paymentMode"] = RandomRequestString(6);
                infoo["sourcePhone"] = senderphone;
                infoo["pin"] = value.Pin;

                var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
                var responsee = await _client.PostAsync("ValidatePIN", contentt);
                if (responsee.IsSuccessStatusCode)
                {
                    JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                    if ((string)custt["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} PIN was not verified for transfer : {Reason}", value.SourcePhone, (string)custt["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} PIN was verified for transfer", value.SourcePhone);
                    }
                }
                else
                {
                    return BadRequest();
                }

                var authhh = new JObject();
                authhh["clientApiKeyField"] = _apidetail.ClientAPIKey;
                authhh["clientIdField"] = _apidetail.ClientId;
                authhh["mACField"] = mac;
                var infooo = new JObject();
                infooo["auth"] = authhh;
                infooo["requestId"] = RandomRequestString(10);
                infooo["paymentMode"] = RandomRequestString(6);
                infooo["sourcePhone"] = value.SourcePhone;
                infooo["pin"] = value.Tap;


                var _content = new StringContent(infooo.ToString(), Encoding.UTF8, "application/json");
                var _response = await _client.PostAsync("Authenticate2FA", _content);
                if (_response.IsSuccessStatusCode)
                {
                    JObject _cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                    if ((string)_cust["ResponseCode"] != "00")
                    {
                        _logger.LogInformation("{user} TAP was not verified for transfer : {Reason}", value.SourcePhone, (string)_cust["ResponseMessage"]);
                        return Ok(new { status = "fail", message = (string)_cust["ResponseMessage"] });
                    }
                    else
                    {
                        _logger.LogInformation("{user} TAP was verified for transfer", value.SourcePhone);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }

            var Json = JsonConvert.DeserializeObject<List<MultipleTransModel>>(value.Rps);
            _logger.LogInformation("{user} Attempting a multiple transfer to {ppl}", value.SourcePhone, value.Rps);
            var sum = Json.Sum(p => p.Amount);
            if (sendingUser.Wallet.AvailableBalance < sum)
            {

                _logger.LogError("{user} could not make multiple transfer to {ppl} : Insufficient Fund", value.SourcePhone, value.Rps, value.SourcePhone);
                return Ok(new { status = "fail", message = "Insufficient Fund" });
            }
            

            var aut = new JObject();
            aut["clientApiKeyField"] = _apidetail.ClientAPIKey;
            aut["clientIdField"] = _apidetail.ClientId;
            aut["mACField"] = mac;
            var inf = new JObject();
            inf["auth"] = aut;
            inf["requestId"] = RandomRequestString(10);
            inf["paymentMode"] = RandomRequestString(6);
            inf["sourcePhone"] = senderphone;

            var _contnt = new StringContent(inf.ToString(), Encoding.UTF8, "application/json");
            var _respnse = await _client.PostAsync("BalanceEnquiry", _contnt);
            if (_respnse.IsSuccessStatusCode)
            {
                JObject _cust = JObject.Parse(await _respnse.Content.ReadAsStringAsync());

                if ((string)_cust["ResponseCode"] == "00")
                {
                    var balance = (string)_cust["WalletBalance"];
                    var AvailableBalance = decimal.Parse(balance);
                    var AmountToTransfar = Json.Sum(p => p.Amount);
                    if (AvailableBalance < AmountToTransfar)
                        return Ok(new { status = "fail", message = "Insufficient Fund." });
                }
                else
                {
                    _logger.LogError("Could not retrieve balance Information for {user} : {Reason}", senderphone, (string)_cust["ResponseMessage"]);
                }
            }
            else
            {
                _logger.LogError("Error requesting balance Information for {user}", senderphone);
                return Ok(new { status = "fail", message = "unable to verify accout balance, please try again" });
            }

            var failedtrans = new List<MultipleTranResp>();
            foreach (var item in Json)
            {
                var phonee = $"234{item.DestinationPhone.Substring(item.DestinationPhone.Length - 10)}";
                var receiver = await _userManager.FindByNameAsync(phonee);
                var receipient = "";
                User receivingUser = null;
                if (receiver != null)
                    receivingUser = await _context.User.Include(p=>p.Wallet).AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == receiver.Id);
                if (receivingUser != null)
                    receipient = $"{receivingUser.FirstName} {receivingUser.LastName}";

                UserCheckInfo usercheckresult = null;
                var auth = new JObject();
                auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                auth["clientIdField"] = _apidetail.ClientId;
                auth["mACField"] = mac;

                var info = new JObject();
                info["auth"] = auth;
                info["requestId"] = RandomRequestString(10);
                info["mobileNo"] = phonee;


                var content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("CustomerInfo", content);
                if (response.IsSuccessStatusCode)
                {
                    JObject cust = JObject.Parse(await response.Content.ReadAsStringAsync());

                    if ((string)cust["ResponseCode"] == "00")
                    {
                        usercheckresult = new UserCheckInfo
                        {
                            FirstName = (string)cust["Firstname"],
                            LastName = (string)cust["Lastname"],
                            DOB = (string)cust["DOB"],
                            Gender = (string)cust["Gender"],
                            Balance = (decimal)cust["Balance"],
                            Status = "success",
                            ReasonPhrase = "Existing User",
                        };

                    }
                }
                else
                {
                    return BadRequest();
                }
                
                if (usercheckresult == null)
                {

                    auth = new JObject();
                    auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                    auth["clientIdField"] = _apidetail.ClientId;
                    auth["mACField"] = mac;
                    info = new JObject();
                    info["auth"] = auth;
                    info["requestId"] = RandomRequestString(10);
                    info["paymentMode"] = RandomRequestString(6);
                    info["sourcePhone"] = phonee;

                    var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                    var _response = await _client.PostAsync("EnrollFlashcashCustomer", _content);
                    if (_response.IsSuccessStatusCode)
                    {
                        JObject cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                        if ((string)cust["ResponseCode"] == "00")
                        {
                            _logger.LogInformation("{user} New user successfully enrolled for transfer", item.DestinationPhone);
                            var charge = item.Amount + decimal.Parse(_configuration["Transaction:Charge"]);
                            auth = new JObject();
                            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                            auth["clientIdField"] = _apidetail.ClientId;
                            auth["mACField"] = mac;
                            info = new JObject();
                            info["auth"] = auth;
                            info["requestId"] = RandomRequestString(10);
                            info["sourcePhone"] = senderphone;
                            info["paymentMode"] = RandomRequestString(6); ;
                            info["destinationPhone"] = phonee;
                            info["amount"] = charge.ToString();
                            info["Naration"] = "";

                            _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                            _response = await _client.PostAsync("Wallet2Wallet", _content);
                            if (_response.IsSuccessStatusCode)
                            {
                                JObject custr = JObject.Parse(await _response.Content.ReadAsStringAsync());

                                if ((string)custr["ResponseCode"] == "00")
                                {
                                    _logger.LogInformation("{Amount} successfully transfered to {recipient} by {sender}", item.Amount, item.DestinationPhone, value.SourcePhone);
                                    var trans = new Transfer
                                    {
                                        Id = Guid.NewGuid(),
                                        Amount = item.Amount,
                                        SenderId = sendingUser.Id,
                                        Claimed = true,
                                        SendDate = DateTime.Now,
                                        SenderPhone = senderphone,
                                        ReceiverPhone = phonee,
                                    };

                                    var history = new TransactionHistory
                                    {
                                        Id = Guid.NewGuid(),
                                        Amount = item.Amount,
                                        Date = DateTime.Now,
                                        Phone = senderphone,
                                        UserId = sendingUser.Id,
                                        HistOwnerId=sendingUser.Phone,
                                        TransactionType = TransactionType.Debit,
                                        Status = Status.Completed,
                                        Description = "Transfer",
                                        DestinationPhone = phonee,
                                        Receipient = receipient
                                       
                                    };
                                    if (decimal.Parse(_configuration["Transaction:Charge"]) > 0)
                                    {
                                        var chargehistory = new TransactionHistory
                                        {
                                            Id = Guid.NewGuid(),
                                            Amount = decimal.Parse(_configuration["Transaction:Charge"]),
                                            Date = DateTime.Now,
                                            Phone = senderphone,
                                            UserId = sendingUser.Id,
                                            HistOwnerId = sendingUser.Phone,
                                            TransactionType = TransactionType.Debit,
                                            Status = Status.Completed,
                                            Description = "Transfer Charge",
                                            DestinationPhone = phonee,
                                            Receipient = receipient

                                        };
                                       
                                        var chargeactivi = new ActivityModel
                                        {
                                            Id = Guid.NewGuid(),
                                            Date = DateTime.Now,
                                            UserId = sendingUser.Phone,
                                            Type = TransactionType.Debit,
                                            Description = $"Transfer charge of {decimal.Parse(_configuration["Transaction:Charge"]).ToString("c", new CultureInfo("ha-Latn-NG"))} was deducted for transfer to {phonee}"
                                        };
                                        _context.Add(chargeactivi);
                                        _context.Add(chargehistory);
                                       
                                    }
                                    var receiverhistory = new TransactionHistory
                                    {
                                        Id = Guid.NewGuid(),
                                        Amount = item.Amount,
                                        Date = DateTime.Now,
                                        Phone = senderphone,

                                        HistOwnerId = phonee,
                                        TransactionType = TransactionType.Credit,
                                        Status = Status.Completed,
                                        Description = "Credit",
                                        DestinationPhone = phonee,
                                        Receipient = sendingUser.FirstName + "" + sendingUser.LastName,
                                        ReceipientPassport = sendingUser.Passport

                                    };
                                    _context.Add(receiverhistory);
                                    var activi = new ActivityModel
                                    {
                                        Id = Guid.NewGuid(),
                                        Date = DateTime.Now,
                                        UserId = sendingUser.Phone,
                                        Type = TransactionType.Debit,
                                        Description = $"You transferred {item.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} to {phonee}"
                                    };
                                    var receiveractivi = new ActivityModel
                                    {
                                        Id = Guid.NewGuid(),
                                        Date = DateTime.Now,
                                        UserId = phonee,
                                        Type = TransactionType.Debit,
                                        Description = $"You received {item.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} from {sendingUser.Phone}"
                                    };
                                    _context.Add(receiveractivi);
                                    sendingUser.Wallet.AvailableBalance -= item.Amount;
                                    sendingUser.Wallet.CurrentBallance -= item.Amount;
                                    if (receivingUser != null)
                                    {
                                        receivingUser.Wallet.AvailableBalance += item.Amount;
                                        receivingUser.Wallet.CurrentBallance += item.Amount;
                                        _context.Update(receivingUser);
                                    }
                                    _context.Update(sendingUser);
                                    _context.Add(activi);
                                    _context.Add(trans);
                                    _context.Add(history);
                                    await _context.SaveChangesAsync();


                                }
                                else /*if ((string)cust["ResponseCode"] == "52")*/
                                {
                                    var incompletetrans = new MultipleTranResp { Phone = item.DestinationPhone, Response = (string)custr["ResponseMessage"], Amount = item.Amount };
                                    failedtrans.Add(incompletetrans);
                                    _logger.LogError("{Amount} Could not be transfered to {recipient} by {sender} : {Reasone}", item.Amount, item.DestinationPhone, value.SourcePhone, (string)custr["ResponseMessage"]);
                                }
                            }
                        }
                        else
                        {
                            var incompletetrans = new MultipleTranResp { Phone = item.DestinationPhone, Response = (string)cust["ResponseMessage"], Amount = item.Amount };
                            failedtrans.Add(incompletetrans);
                            _logger.LogError("{user} New user could not enrolled for transfer : {Reason}", item.DestinationPhone, (string)cust["ResponseMessage"]);
                        }
                    }

                }
                else
                {
                    var charge=item.Amount+ decimal.Parse(_configuration["Transaction:Charge"]);
                    auth = new JObject();
                    auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                    auth["clientIdField"] = _apidetail.ClientId;
                    auth["mACField"] = mac;

                    info = new JObject();
                    info["auth"] = auth;
                    info["requestId"] = RandomRequestString(10);
                    info["sourcePhone"] = senderphone;
                    info["paymentMode"] = RandomRequestString(6);
                    info["destinationPhone"] = phonee;
                    info["amount"] = charge.ToString();
                    info["Naration"] = "";

                    var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                    var _response = await _client.PostAsync("Wallet2Wallet", _content);
                    if (_response.IsSuccessStatusCode)
                    {
                        JObject cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                        if ((string)cust["ResponseCode"] == "00")
                        {
                            _logger.LogInformation("{Amount} successfully transfered to {recipient} by {sender}", item.Amount, item.DestinationPhone, value.SourcePhone);
                            var trans = new Transfer
                            {
                                Id = Guid.NewGuid(),
                                Amount = item.Amount,
                                SenderId = sendingUser.Id,
                                Claimed = true,
                                SendDate = DateTime.Now,
                                SenderPhone = senderphone,
                                ReceiverPhone = phonee,

                            };
                            if (receivingUser==null)
                            {
                                var history = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = item.Amount,
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                    UserId = sendingUser.Id,
                                    HistOwnerId = sendingUser.Phone,
                                    TransactionType = TransactionType.Debit,
                                    Status = Status.Completed,
                                    Description = "Transfer",
                                    DestinationPhone = phonee,
                                    Receipient = receipient,
                                    //ReceipientPassport = receivingUser.Passport
                                };
                                _context.Add(history);
                            }
                            else
                            {
                                var history = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = item.Amount,
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                    UserId = sendingUser.Id,
                                    HistOwnerId = sendingUser.Phone,
                                    TransactionType = TransactionType.Debit,
                                    Status = Status.Completed,
                                    Description = "Transfer",
                                    DestinationPhone = phonee,
                                    Receipient = receipient,
                                    ReceipientPassport = receivingUser.Passport
                                };
                            }
                            
                            if (decimal.Parse(_configuration["Transaction:Charge"]) > 0)
                            {
                                var chargehistory = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = decimal.Parse(_configuration["Transaction:Charge"]),
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                    UserId = sendingUser.Id,
                                    HistOwnerId = sendingUser.Phone,
                                    TransactionType = TransactionType.Debit,
                                    Status = Status.Completed,
                                    Description = "Transfer Charge",
                                    DestinationPhone = phonee,
                                    Receipient = receipient

                                };
                                var chargeactivi = new ActivityModel
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    UserId = sendingUser.Phone,
                                    Type = TransactionType.Debit,
                                    Description = $"Transfer charge of {decimal.Parse(_configuration["Transaction:Charge"]).ToString("c", new CultureInfo("ha-Latn-NG"))} was deducted for transfer to {phonee}"
                                };
                                _context.Add(chargeactivi);
                                _context.Add(chargehistory);
                            }
                            if (receivingUser != null)
                            {
                                var receiverhistory = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = item.Amount,
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                    UserId = receivingUser.Id,
                                    HistOwnerId=receivingUser.Phone,
                                    TransactionType = TransactionType.Credit,
                                    Status = Status.Completed,
                                    Description = "Credit",
                                    DestinationPhone = phonee,
                                    Receipient = sendingUser.FirstName + "" + sendingUser.LastName,
                                    ReceipientPassport = sendingUser.Passport

                                };
                                var receiveractivity = new ActivityModel
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    UserId = receivingUser.Phone,
                                    Type = TransactionType.Credit,
                                    Description = $"You received {item.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} from {senderphone}"
                                };
                                _context.Add(receiverhistory);
                                _context.Add(receiveractivity);
                            }
                            else
                            {
                                var receiverhistory = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = item.Amount,
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                    UserId = Guid.NewGuid(),
                                    HistOwnerId = phonee,
                                    TransactionType = TransactionType.Credit,
                                    Status = Status.Completed,
                                    Description = "Credit",
                                    DestinationPhone = phonee,
                                    Receipient = sendingUser.FirstName + "" + sendingUser.LastName,
                                    ReceipientPassport = sendingUser.Passport

                                };
                                var receiveractivity = new ActivityModel
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    UserId = phonee,
                                    Type = TransactionType.Credit,
                                    Description = $"You received {item.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} from {senderphone}"
                                };
                                _context.Add(receiverhistory);
                                _context.Add(receiveractivity);
                            }
                            var activi = new ActivityModel
                            {
                                Id = Guid.NewGuid(),
                                Date = DateTime.Now,
                                UserId = sendingUser.Phone,
                                Type = TransactionType.Debit,
                                Description = $"You transferred {item.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} to {phonee}"
                            };
                            sendingUser.Wallet.AvailableBalance -= item.Amount;
                            sendingUser.Wallet.CurrentBallance -= item.Amount;
                            if (receivingUser != null)
                            {
                                receivingUser.Wallet.AvailableBalance += item.Amount;
                                receivingUser.Wallet.CurrentBallance += item.Amount;
                                _context.Update(receivingUser);
                            }
                           
                            _context.Update(sendingUser);
                            _context.Add(activi);
                            _context.Add(trans);
                           
                            await _context.SaveChangesAsync();

                        }
                        else /*if ((string)cust["ResponseCode"] == "52")*/
                        {
                            var incompletetrans = new MultipleTranResp { Phone = item.DestinationPhone, Response = (string)cust["ResponseMessage"], Amount=item.Amount };
                            failedtrans.Add(incompletetrans);
                            _logger.LogError("{Amount} Could not be transfered to {recipient} by {sender} : {Reasone}", item.Amount, item.DestinationPhone, value.SourcePhone, (string)cust["ResponseMessage"]);
                        }
                    }

                }
            }
            return Ok(new { status = "success" , Resp=failedtrans});
        }




        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var senderphone = $"234{value.Phone.Substring(value.Phone.Length - 10)}";
            var sender = await _userManager.FindByNameAsync(senderphone);
            if (sender == null)
            {
                return BadRequest();
            }
            var sendingUser = await _context.User.Include(p => p.Wallet).SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);

            
            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;
            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["paymentMode"] = RandomRequestString(10);
            info["sourcePhone"] = senderphone;
            info["pin"] = value.Pin.ToString();
            
            var contentt = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var responsee = await _client.PostAsync("ValidatePIN", contentt);
            if (responsee.IsSuccessStatusCode)
            {
                JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                if ((string)custt["ResponseCode"] != "00")
                {
                    return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                }
            }
            else
            {
                return BadRequest();
            }


            auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;

            info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["sourcePhone"] = senderphone;
            info["paymentMode"] = RandomRequestString(6); 
            info["accountNumber"] = value.AccountNumber;
            info["amount"] = value.Amount.ToString();
            info["Naration"] = value.Naration;
            
            var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var _response = await _client.PostAsync("Wallet2Account", _content);
            if (_response.IsSuccessStatusCode)
            {
                JObject cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                if ((string)cust["ResponseCode"] == "00")
                {
                    var withrawal = new Withdrawal
                    {
                        Id = Guid.NewGuid(),
                        AccountNumber = value.AccountNumber,
                        Amount = value.Amount,
                        Date = DateTime.Now,
                        UserId = sendingUser.Id,
                        TransactionId = (string)cust["TransactionId"],
                        RequestId = (string)cust["RequestId"]
                    };

                    var history = new TransactionHistory
                    {
                        Id = Guid.NewGuid(),
                        Amount = value.Amount,
                        Date = DateTime.Now,
                        Phone = senderphone,
                        HistOwnerId = sendingUser.Phone,
                        UserId = sendingUser.Id,
                        TransactionType = TransactionType.Debit,
                        Status = Status.Completed,
                        Description = "Withdrawal"
                    };

                    var activi = new ActivityModel
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        UserId = sendingUser.Phone,
                        Type = TransactionType.Debit,
                        Description = $"You withdrew {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))}"
                    };
                    _context.Add(activi);

                    sendingUser.Wallet.AvailableBalance -= value.Amount;
                    sendingUser.Wallet.CurrentBallance -= value.Amount;

                    _context.Update(sendingUser);
                    _context.Add(withrawal);
                    _context.Add(history);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = "success" });
                }
                else if ((string)cust["ResponseCode"] == "52")
                {
                    return Ok(new { status = "failed", message = (string)cust["ResponseMessage"] });
                }
            }

            return BadRequest();
        }

        

        [HttpPost("Airtime")]
        public async Task<IActionResult> Airtime([FromBody] AirtimeDTO value)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            var phonee = $"234{value.DestinationPhone.Substring(value.DestinationPhone.Length - 10)}";
            var senderphone = $"234{value.SourcePhone.Substring(value.SourcePhone.Length - 10)}";
            var sender = await _userManager.FindByNameAsync(senderphone);
            if (sender == null)
            {
                return BadRequest();
            }
            var sendingUser = await _context.User.Include(p => p.Wallet).SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);



            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;
            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["paymentMode"] = RandomRequestString(10);
            info["sourcePhone"] = senderphone;
            info["pin"] = value.Pin.ToString();
            
            var contentt = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var responsee = await _client.PostAsync("ValidatePIN", contentt);
            if (responsee.IsSuccessStatusCode)
            {
                JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                if ((string)custt["ResponseCode"] != "00")
                {
                    return Ok(new { status = "fail", message = (string)custt["ResponseMessage"] });
                }
            }
            else
            {
                return BadRequest();
            }


                        string network = "";
                        switch (value.NetWork)
                        {
                            case NetWork.MTN:
                                network = "MTN";
                                break;
                            case NetWork.Glo:
                                network = "GLO";
                                break;
                            case NetWork.Etisalat:
                                network = "ETI";
                                break;
                            case NetWork.Airtel:
                                network = "AIR";
                                break;
                        }

                        auth = new JObject();
                        auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                        auth["clientIdField"] = _apidetail.ClientId;
                        auth["mACField"] = mac;

                        info = new JObject();
                        info["auth"] = auth;
                        info["requestId"] = RandomString(10);
                        info["sourcePhone"] = senderphone;
                        info["paymentMode"] = RandomString(6);
                        info["network"] = network;
                        info["amount"] = value.Amount.ToString();
                        info["destinationPhone"] = phonee;
            
                         var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                        var _response = await _client.PostAsync("AirtimePurchase", _content);
                        if (_response.IsSuccessStatusCode)
                        {
                            JObject cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                            if ((string)cust["ResponseCode"] == "00")
                            {
                                var withrawal = new AirtimeRecharge
                                {
                                    Id = Guid.NewGuid(),
                                    DestinationPhone = phonee,
                                    Amount = value.Amount,
                                    Date = DateTime.Now,
                                    UserId = sendingUser.Id,
                                    TransactionId = (string)cust["TransactionId"],
                                    RequestId = (string)cust["RequestId"]
                                };

                                var history = new TransactionHistory
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = value.Amount,
                                    Date = DateTime.Now,
                                    Phone = senderphone,
                                    UserId = sendingUser.Id,
                                    HistOwnerId = sendingUser.Phone,
                                    TransactionType = TransactionType.Debit,
                                    Status = Status.Completed,
                                    Description = "Airtime Purchase"
                                };


                                var activi = new ActivityModel
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    Type = TransactionType.Debit,
                                    Description = $"You purchased {value.Amount.ToString("c", new CultureInfo("ha-Latn-NG"))} worth of Airtime"
                                };
                                _context.Add(activi);

                                sendingUser.Wallet.AvailableBalance -= value.Amount;
                                sendingUser.Wallet.CurrentBallance -= value.Amount;

                                _context.Update(sendingUser);
                                _context.Add(withrawal);
                                _context.Add(history);
                                await _context.SaveChangesAsync();
                                return Ok(new { status = "success" });
                            }
                        }

             return BadRequest();
        }




   

        [HttpGet("History/{Phone}")]
        public async Task<IActionResult> History(string Phone)
        {
            _logger.LogInformation("{user} Requested for Transaction History",Phone);
            var user = await _userManager.FindByNameAsync(Phone);
            if (user == null)
                return BadRequest();
            var Owner = await _context.User.AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);
            var transhis = new List< TransactionHistory>();
            var trans = await _context.TransactionHistory.Where(p => p.HistOwnerId == Phone ).ToListAsync();

            return Ok(trans);
        }

        //[HttpGet("HistoryById/{Id}/{Phone}")]
        //public async Task<IActionResult> HistoryById(Guid Id, string phone)
        //{
            
        //    _logger.LogInformation("{user} Requested for Transaction History", phone);
        //    var user = await _userManager.FindByNameAsync(phone);
        //    if (user == null) { return BadRequest(); }



        //    var trans = await _context.TransactionHistory.Where(p => p.Id == Id).SingleAsync();

        //    return Ok(trans);
        //}

        [HttpGet("Balance/{Phone}")]
        public async Task<IActionResult> Balance(string Phone)
        {

            var user = await _userManager.FindByNameAsync(Phone);
            if (user == null)
                return BadRequest();
            var Owner = await _context.User.Include(p=>p.Wallet).AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;

            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["paymentMode"] = RandomRequestString(6);
            info["sourcePhone"] = Phone;
            
            var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var _response = await _client.PostAsync("BalanceEnquiry", _content);
            if (_response.IsSuccessStatusCode)
            {
                JObject _cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                if ((string)_cust["ResponseCode"] == "00")
                {

                    var activities = await _context.ActivityModel.AsNoTracking().Where(p => p.UserId == Owner.Phone)
                     .OrderByDescending(p => p.Date).Take(10).ToListAsync();
                    var transCount = await _context.TransactionHistory.Where(p => p.HistOwnerId == Phone && p.TransactionType == TransactionType.Debit && p.Receipient != "Charge").CountAsync();
                    var fundWalletCount = await _context.TransactionHistory.Where(p => p.HistOwnerId == Phone && p.TransactionType == TransactionType.Credit).CountAsync();

                    string Lastf = "";
                    if (Owner.LastWalletFundedDate != default(DateTime))
                    {             
                        Lastf = Owner.LastWalletFundedDate.ToString("dd MMM yyy");
                    }
                    Owner.Wallet.AvailableBalance = (decimal)_cust["WalletBalance"];
                    Owner.Wallet.CurrentBallance= (decimal)_cust["WalletBalance"];
                    _context.Update(Owner);
                    _context.SaveChanges();
                    return Ok(new { status = "success", Balance = (string)_cust["WalletBalance"], activities,TransactionCount = transCount, FundWalletCount = fundWalletCount, LastFunded = Lastf});


                }
                else
                {
                    _logger.LogError("Could not retrieve balance Information for {user} : {Reason}",Phone, (string)_cust["ResponseMessage"]);
                }
            }
            else
            {
                _logger.LogError("Error requesting balance Information for {user}", Phone);
            }
            return BadRequest();
        }



   
    }
    
}
