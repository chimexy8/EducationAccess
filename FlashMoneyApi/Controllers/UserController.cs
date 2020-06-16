using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EducationAccessApi.Components;
using EducationAccessApi.Models;
using FlashMoneyApi.Data;
using FlashMoneyApi.DTOs;
using FlashMoneyApi.Migrations;
using FlashMoneyApi.Models;
using FlashMoneyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlashMoneyApi.Controllers
{
    [Route("flashmoney")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IEmailSender _emailSender;
        private ILogger<UserController> _logger;
        private ApiDetail _apidetail;
        private UserManager<ApplicationUser> _userManager;
        private IFlashHttpClient _flashMoneyHttpClient;
        private string mac;

        public UserController(ApplicationDbContext context, IEmailSender emailSender, ILogger<UserController> logger,
            UserManager<ApplicationUser> userManager, IOptions<ApiDetail> options,
             IFlashHttpClient flashMoneyHttpClient)
        {
            _context = context;
            _emailSender = emailSender;
            _logger = logger;
            _apidetail = options.Value;
            _userManager = userManager;
            _flashMoneyHttpClient = flashMoneyHttpClient;

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

        
        [HttpGet("QetChartData/{Phone}")]
        public async Task<IActionResult> QetChartData(string Phone)
        {
            if (string.IsNullOrEmpty(Phone))
                return BadRequest();

            var sender = await _userManager.FindByNameAsync(Phone);
            if (sender == null)
            {
                return NotFound();
            }

            try
            {
                var user = await _context.User.AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);
                var transCount = await _context.TransactionHistory.Where(p => p.HistOwnerId == Phone && p.TransactionType == TransactionType.Debit).CountAsync();
                var fundCount = await _context.TransactionHistory.Where(p => p.HistOwnerId==Phone && p.TransactionType == TransactionType.Credit).CountAsync();
                return Ok(new ChartDataDTO{ Tranfers = transCount, FundWallet = fundCount });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("UserProfile/{Phone}")]
        public async Task<IActionResult> UserProfile(string Phone)
        {
            if (string.IsNullOrEmpty(Phone))
                return BadRequest();

            var sender = await _userManager.FindByNameAsync(Phone);
            if (sender == null)
            {
                return NotFound();
            }

            try
            {
                var user = await _context.User.Include(p => p.NextOfKin).AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);

                var prof = new ProfileDTO
                {
                    DOB = user.DOB,
                    FirstName = user.FirstName,
                    Gender = user.Gender == Gender.Male ? "Male" : "Female",
                    LastName = user.LastName,
                    Phone = user.Phone,
                    HasTransactionPin = user.HasTransactionPin,
                    AllowActivityNotif = user.AllowAccountActivityNotif,
                    AllowTransactionNotif = user.AllowTransactionNotif,
                    HasAuthPin = sender.HasResetPin
                };
                if (user.MothersMedianName != null)
                    prof.MothersMedianName = user.MothersMedianName;
                if (user.Email != null)
                    prof.Email = user.Email;
                if (user.NextOfKin?.Address != null)
                    prof.NextofKinAddress = user.NextOfKin.Address;
                if (user.NextOfKin?.Email != null)
                    prof.NextofKinEmail = user.NextOfKin.Email;
                if (user.NextOfKin?.FirstName != null)
                    prof.NextofKinFirstname = user.NextOfKin.FirstName;
                if (user.NextOfKin?.LastName != null)
                    prof.NextofKinLastName = user.NextOfKin.LastName;
                if (user.NextOfKin?.Phone != null)
                    prof.NextofKinPhone = user.NextOfKin.Phone;

                return Ok(prof);
            }
            catch (Exception ex)
            {

                throw;
            }


        }



        [HttpPut("Notif/{Phone}")]
        public async Task<IActionResult> UpdateUser([FromBody]NotificationDTO value, string Phone)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var sender = await _userManager.FindByNameAsync(Phone);
            if (sender == null)
            {
                return NotFound();
            }
            var user = await _context.User.SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);

            user.AllowTransactionNotif = value.AllowTransactionNotif;
            user.AllowAccountActivityNotif = value.AllowActivityNotif;
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }


        [HttpPut("UpdateUser/{Phone}")]
        public async Task<IActionResult> UpdateUser([FromBody]ProfileDTO value,string Phone)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var sender = await _userManager.FindByNameAsync(Phone);
            if (sender == null)
            {
                _logger.LogInformation("{user} Could not be found for profile update", Phone);
                return NotFound();
            }
            var user = await _context.User.SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);

            user.DOB = value.DOB;
            user.Email = value.Email;
            user.FirstName = value.FirstName;
            user.Gender = value.Gender == "Male" ? Gender.Male : Gender.Female;
            user.LastName = value.LastName;
            user.MothersMedianName = value.MothersMedianName;
            user.Passport = value.Passport;

            var activi = new ActivityModel
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                UserId = user.Phone,
                Description = $"You updated your profile"
            };

            try
            {
                _logger.LogInformation("{user} Updated Profile", Phone);
                _context.Add(activi);
                _context.Update(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                
            }
            return BadRequest();
        }

        [HttpPut("UpdateUserKYC/{Phone}")]
        public async Task<IActionResult> UpdateUserKYC([FromBody]ProfileDTO value, string Phone)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var sender = await _userManager.FindByNameAsync(Phone);
            if (sender == null)
            {
                return NotFound();
            }
            var user = await _context.User.SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);

            user.Passport = value.DOB;
            user.ValidId = value.Email;
            user.UtilityBill = value.FirstName;

            var activi = new ActivityModel
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                UserId = user.Phone,
                Description = $"You updated your profile"
            };

            try
            {
                _context.Add(activi);
                _context.Update(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

        [HttpPut("UpdateNextOfKin/{Phone}")]
        public async Task<IActionResult> UpdateNextOfKin([FromBody]ProfileDTO value, string Phone)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var sender = await _userManager.FindByNameAsync(Phone);
            if (sender == null)
            {
                return NotFound();
            }
            var user = await _context.User.SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);

            var Next = await _context.NextOfKin.SingleOrDefaultAsync(p => p.UserId == user.Id);

            if (Next == null)
            {
                Next = new NextOfKin
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Phone = value.NextofKinPhone,
                    Address = value.NextofKinAddress,
                    Email = value.NextofKinEmail,
                    FirstName = value.NextofKinFirstname,
                    LastName = value.NextofKinLastName
                };

                try
                {
                    _logger.LogInformation("{user} Updated Next of Kin", Phone);
                    _context.Add(Next);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
                {

                }
            }
            else
            {
                Next.Address = value.NextofKinAddress;
                Next.Email = value.NextofKinEmail;
                Next.FirstName = value.NextofKinFirstname;
                Next.LastName = value.NextofKinLastName;
                Next.Phone = value.NextofKinPhone;
                
                try
                {
                    _context.Update(Next);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (Exception)
                {

                }
            }
          
            return BadRequest();
        }




        [HttpPost("PasswordChange")]
        public async Task<IActionResult> PasswordChange([FromBody] PasswordChangeDTO value)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(value.Phone);
            if (user != null && await _userManager.CheckPasswordAsync(user, value.OldPassword))
            {
                var Rcode = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, Rcode, value.NewPassword);
                if (!result.Succeeded)
                {
                    return Ok(new { status = "fail", message = result.Errors.FirstOrDefault().Description});
                }
                _logger.LogInformation("{user} Successfully changed Password", value.Phone);
                return Ok(new { status = "success" });
            }
            return BadRequest();
        }



        [HttpPost("ResendCode")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendCode([FromBody] ResendDTO value)
        {
            async Task sendOPT(string OTP, string phonee)
            {
                using (var context = new HttpClient())
                {

                    var url = $"http://churchplusapi.azurewebsites.net/api/messaging/v1/sendsinglemessage?recipient{phonee}&subject=&message=Verification%20Code%20{OTP}";

                    context.DefaultRequestHeaders.Accept.Clear();

                    context.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //context.DefaultRequestHeaders.Add("Authorization", "CID 213f34ce-d22b-427a-a1aa-515d751ddc22");
                    context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CID", "213f34ce-d22b-427a-a1aa-515d751ddc22");

                    var result = await context.GetAsync(url);
                }

                await _emailSender.SendEmailAsync("chimaokoli76@yahoo.com", "Confrim Phone Number", OTP);
            }

            if (string.IsNullOrEmpty(value.Phone))
                return BadRequest();

              var hjh = RandomString(6);

            var otv = new OTPValidation { Phone = value.Phone, Code = hjh, Date = DateTime.Now, Id = Guid.NewGuid() };
            try
            {
                _context.Add(otv);
                await _context.SaveChangesAsync();
                await sendOPT(hjh, value.Phone);
                return Ok(new { status = "success"});
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("ValidateResetCode")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateResetCode([FromBody] OtpDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var valid = await _context.OTPValidation.SingleOrDefaultAsync(p => p.Code == value.Otp && p.Phone == value.Phone && p.Date.AddMinutes(10) >= DateTime.Now);
            if (valid == null)
            {
                return Ok(new PasswordResetDTO { Status = "fail", ReasonPhrase = "Invalid Code" });
            }
            else
            {
                var user = await _userManager.FindByNameAsync(value.Phone);
                if (user != null)
                {
                    var Rcode = await _userManager.GeneratePasswordResetTokenAsync(user);
                    return Ok(new PasswordResetDTO { Status = "success", Rcode = Rcode});
                }
                else
                {
                    return Ok(new PasswordResetDTO { Status = "fail", ReasonPhrase = "No User found with this number" });
                }
            }

        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordChangeDTO value)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(value.Phone);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, value.RCode, value.NewPassword);
                if (!result.Succeeded)
                {
                    return BadRequest();
                }
                _logger.LogInformation("{user} Password Reset Successful", value.Phone);
                return Ok();
            }
            return BadRequest();
        }


        [HttpPost("QueryReceipient")]
        [AllowAnonymous]
        public async Task<IActionResult> QueryReceipient([FromBody] ResendDTO value)
        {
              var user = await _userManager.FindByNameAsync(value.Senderphone);
                var sender = _context.User.FirstOrDefault(p => p.ApplicationUserId == user.Id);
            
           
            UserCheckInfo usercheckresult = null;
            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;

            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["mobileNo"] = value.Phone;

            var client = _flashMoneyHttpClient.GetClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("CustomerInfo", content);
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
                        HasSetTap=sender.HasTransactionPin
                    };

                }
            }

            return Ok(usercheckresult);
        }




        [HttpPost("PhoneBvn")]
        [AllowAnonymous]
        [Obsolete]
        public async Task<IActionResult> Phone([FromBody]BvnOrPhoneModel value)
        {
    
            if (!string.IsNullOrEmpty(value.Phone))
            {
                var Email = value.Phone;
                var hjh = RandomString(6);
                var confirmawaitinglist = _context.ConfirmationAwaitings.SingleOrDefault(p =>p.Email == Email);
                var user = _context.User.SingleOrDefault(p => p.Email == value.Phone);
                if (user==null)
                {
                    if (confirmawaitinglist!=null)
                    {
                        try
                        {
                            confirmawaitinglist.Time = DateTime.Now;
                            confirmawaitinglist.Code = hjh;
                            await UtilityHelper.sendEmailAsyncTest(Email, "Verification Code", $"{hjh} Is your verification Code, it will expire in 10 minutes.");
                            _context.Update(confirmawaitinglist);
                            _context.SaveChanges();
                            return Ok(new UserCheckInfo { Status = "success", ReasonPhrase = "Phone: New User" });
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Sending OTP for phone validation Failed : {Reason}", ex.Message);
                            return Ok(new UserCheckInfo { Status = "failed", ReasonPhrase = $"Sending OTP for phone validation Failed : {ex.Message}" });
                        }

                    }
                    else
                    {
                        try
                        {
                            var newconfirmawaitinglist = new ConfirmationAwaiting { Active = true, Code = hjh, Email = Email, Time = DateTime.Now };
                            _context.Add(newconfirmawaitinglist);
                            _context.SaveChanges();
                            await UtilityHelper.sendEmailAsyncTest(Email, "Verification Code", $"{hjh} Is your verification Code, it will expire in 10 minutes.");

                            return Ok(new UserCheckInfo { Status = "success", ReasonPhrase = "Phone: New User" });
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Sending OTP for phone validation Failed : {Reason}", ex.Message);
                            return Ok(new UserCheckInfo { Status = "failed", ReasonPhrase = $"Sending OTP for phone validation Failed : {ex.Message}" });
                        }
                       
                    }
                   
                }
                else 
                {
                   
                    return Ok(new UserCheckInfo { Status = "Existing User", ReasonPhrase = $"User Already Exist" });

                }
                

            }
            return BadRequest();
        }



        [HttpPost("ValidateOTP")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidatePhone([FromBody] OtpDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var valid = await _context.ConfirmationAwaitings.SingleOrDefaultAsync(p => p.Code == value.Otp && p.Email == value.Phone && p.Time.AddMinutes(10) >= DateTime.Now);
            if(valid == null)
            {
                return Ok(new UserCheckInfo { Status = "fail", ReasonPhrase = "Invalid OTP"});

            }
            else
            {
                //valid.Active = false;
                //_context.Update(valid);
                //_context.SaveChanges();
                return Ok(new UserCheckInfo { Status = "success", ReasonPhrase = "New User", Phone = valid.Email });
            }



        }

        [HttpPost("min")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPin([FromBody] OtpDTO value)
        {
            var phonee = $"234{value.Phone.Substring(value.Phone.Length - 10)}";

            var authh = new JObject();
            authh["clientApiKeyField"] = _apidetail.ClientAPIKey;
            authh["clientIdField"] = _apidetail.ClientId;
            authh["mACField"] = mac;
            var infoo = new JObject();
            infoo["auth"] = authh;
            infoo["requestId"] = RandomRequestString(10);
            infoo["paymentId"] = RandomRequestString(6);
            infoo["sourcePhone"] = phonee;
            infoo["pin"] = value.Otp;



            var clientt = _flashMoneyHttpClient.GetClient();
            clientt.DefaultRequestHeaders.Accept.Clear();
            clientt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var contentt = new StringContent(infoo.ToString(), Encoding.UTF8, "application/json");
            var responsee = await clientt.PostAsync("ValidatePIN", contentt);
            if (responsee.IsSuccessStatusCode)
            {
                JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                if ((string)custt["ResponseCode"] == "00")
                {

                    UserCheckInfo usercheckresult = null;

                    //Check if Customer exist
                    var auth = new JObject();
                    auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
                    auth["clientIdField"] = _apidetail.ClientId;
                    auth["mACField"] = mac;

                    var info = new JObject();
                    info["auth"] = auth;
                    info["requestId"] = RandomString(10);
                    info["mobileNo"] = phonee;

                    var client = _flashMoneyHttpClient.GetClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("CustomerInfo", content);
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

                            return Ok(usercheckresult);
                        }
                    }
                }
                else
                {
                    return Ok(new UserCheckInfo() { Status = "fail", ReasonPhrase = "Wrong Pin" });
                }
            }
            return BadRequest();
        }

        [HttpPost("ResetPin")]
        public async Task<IActionResult> ResetPin([FromBody] ResetPinDTO value)
        {
            var senderphone = $"234{value.Phone.Substring(value.Phone.Length - 10)}";
            var sender = await _userManager.FindByNameAsync(senderphone);
            if (sender == null)
                return NotFound();
            var user = await _context.User.AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);

            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;
            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["paymentMode"] = RandomRequestString(6);
            info["sourcePhone"] = value.Phone;
            info["pin"] = value.OldPin;
            info["newPin"] = value.NewPin;


            var clientt = _flashMoneyHttpClient.GetClient();
            clientt.DefaultRequestHeaders.Accept.Clear();
            clientt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var contentt = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var responsee = await clientt.PostAsync("ResetPIN", contentt);
            if (responsee.IsSuccessStatusCode)
            {
                JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                if ((string)custt["ResponseCode"] == "00")
                {
                    var activi = new ActivityModel
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                         UserId = user.Phone,
                        Description = $"You changed your PIN"
                    };
                    _context.Add(activi);

                    return Ok(new { status = "success" });
                }
                else
                {
                    return Ok(new { status = "fail",message = (string)custt["ResponseMessage"] });
                }
            }
            return BadRequest();
        }



        [HttpPost("Create2FA")]
        public async Task<IActionResult> Create2FA([FromBody] Create2FADTO value)
        {
            var user = await _userManager.FindByNameAsync(value.Phone);
            if (user == null)
                return BadRequest();
            var Owner = await _context.User.SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;

            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["paymentMode"] = RandomRequestString(6);
            info["sourcePhone"] = value.Phone;
            info["newpin"] = value.NewTwoFA;


            var _client = _flashMoneyHttpClient.GetClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var _response = await _client.PostAsync("Create2FACode", _content);
            if (_response.IsSuccessStatusCode)
            {
                JObject _cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                if ((string)_cust["ResponseCode"] == "00")
                {
                    _logger.LogInformation("{User} successfully created a TAP", value.Phone);
                    Owner.HasTransactionPin = true;
                    _context.Update(Owner);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = "success" });
                }
                else
                {
                    _logger.LogError("{User} TAP could not be created : {Reason}", value.Phone, (string)_cust["ResponseMessage"]);
                    return Ok(new { status = "fail", message = (string)_cust["ResponseMessage"] });
                }
            }
            return BadRequest();
        }


        [HttpPost("Authenticate2FA")]
        public async Task<IActionResult> Authenticate2FA([FromBody] Create2FADTO value)
        {
            var user = await _userManager.FindByNameAsync(value.Phone);
            if (user == null)
                return BadRequest();
            var Owner = await _context.User.AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;

            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["paymentMode"] = RandomRequestString(6);
            info["sourcePhone"] = value.Phone;
            info["pin"] = value.TwoFA;


            var _client = _flashMoneyHttpClient.GetClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var _response = await _client.PostAsync("Authenticate2FA", _content);
            if (_response.IsSuccessStatusCode)
            {
                JObject _cust = JObject.Parse(await _response.Content.ReadAsStringAsync());

                if ((string)_cust["ResponseCode"] == "00")
                {

                }
            }
            return BadRequest();
        }


        [HttpPost("Change2FA")]
        public async Task<IActionResult> Change2FA([FromBody] Create2FADTO value)
        {
            var user = await _userManager.FindByNameAsync(value.Phone);
            if (user == null)
                return BadRequest();
            var Owner = await _context.User.AsNoTracking().SingleOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;

            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomRequestString(10);
            info["paymentMode"] = RandomRequestString(6);
            info["sourcePhone"] = value.Phone;
            info["mypin"] = value.TwoFA;
            info["newpin"] = value.NewTwoFA;

            var _client = _flashMoneyHttpClient.GetClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var _content = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var _response = await _client.PostAsync("Change2FA", _content);
            if (_response.IsSuccessStatusCode)
            {
                JObject _cust = JObject.Parse(await _response.Content.ReadAsStringAsync());
                if ((string)_cust["ResponseCode"] == "00")
                {
                    _logger.LogInformation("{User} successfully changed TAP", value.Phone);
                    return Ok(new { status = "success"});
                }
                else
                {
                    _logger.LogError("{User} TAP could not be changed : {Reason}", value.Phone, (string)_cust["ResponseMessage"]);
                    return Ok(new { status = "fail", message = (string)_cust["ResponseMessage"] });
                }
            }
            return BadRequest();
        }
    }
}
