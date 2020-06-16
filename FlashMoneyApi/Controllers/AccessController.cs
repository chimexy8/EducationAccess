using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlashMoneyApi.Data;
using FlashMoneyApi.DTOs;
using FlashMoneyApi.Models;
using FlashMoneyApi.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlashMoneyApi.Controllers
{
    [Route("flashmoney")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFlashHttpClient _flashMoneyHttpClient;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccessController> _logger;
        private readonly ApiDetail _apidetail;
        private readonly HttpClient _client;

        public AccessController(
                  UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILogger<AccessController> logger,
            SignInManager<ApplicationUser> signInManager,
             IEmailSender emailSender, ApplicationDbContext context, IOptions<ApiDetail> options, IFlashHttpClient flashMoneyHttpClient)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _emailSender = emailSender;
            _context = context;
            _logger = logger;
            _apidetail = options.Value;
            _client = _flashMoneyHttpClient.GetClient();
        }

        private static Random randomm = new Random();
        string RandomPin(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[randomm.Next(s.Length)]).ToArray());
        }

        private static Random random = new Random();
        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet("HasResetPin/{phone}")]
        public async Task<IActionResult> HasResetPin( string phone)
        {
            var usermanager = await _userManager.FindByNameAsync(phone);
            if (usermanager.HasResetPin)
            {
                return Ok(new { status = "true" });
            }
            else
            {
                return Ok(new { status = "false" });
            }
           
        }

        [HttpGet("CreatePin/{Pin}/{phone}")]
        public async Task<IActionResult> CreatePin(string Pin, string phone)
        {
            var text = $"{_apidetail.ClientId}:{_apidetail.ClientAPIKey}";
            var key = _apidetail.ClientSecretKey;
            var Iv = _apidetail.ClientIVKey;
            var mac = GenericAes.encrypt(text, key, Iv);
            var auth = new JObject();
            auth["clientApiKeyField"] = _apidetail.ClientAPIKey;
            auth["clientIdField"] = _apidetail.ClientId;
            auth["mACField"] = mac;
            var userphone = phone.Substring(phone.Length - 10);
            var phonee = $"234{userphone}";
            var info = new JObject();
            info["auth"] = auth;
            info["requestId"] = RandomPin(10);
            info["paymentMode"] = RandomPin(6);
            info["sourcePhone"] = phonee;
            info["pin"] = "0000";
            info["newPin"] = Pin;

            var contentt = new StringContent(info.ToString(), Encoding.UTF8, "application/json");
            var responsee = await _client.PostAsync("ResetPIN", contentt);
            if (responsee.IsSuccessStatusCode)
            {
                JObject custt = JObject.Parse(await responsee.Content.ReadAsStringAsync());

                if ((string)custt["ResponseCode"] == "00")
                {
                    var usermanager = await _userManager.FindByNameAsync(phone);

                    var user = _context.User.FirstOrDefault(p=>p.ApplicationUserId==usermanager.Id);
                    user.HasResetPin = true;
                    usermanager.HasResetPin = true;
                    _context.Update(user);
                    _context.Update(usermanager);
                    _context.SaveChanges();
                    _logger.LogInformation("New PIN was successfuly created for {user}", phone);
                    return Ok(new { Status = "successfull" });
                }
                else
                {
                    _logger.LogError("New PIN could not be created for {user} : {Message}", phone, (string)custt["ResponseMessage"]);
                    return Ok(new { Status = "fail", Message = (string)custt["ResponseMessage"] });
                }

            }
            else
            {
                _logger.LogError("New PIN could not be created for {user} : {Message}", phone, responsee.ReasonPhrase);
                return Ok(new { Status = "fail", message = responsee.ReasonPhrase });
            }
        }
        // GET: api/Access
        [HttpPost("Create")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel value)
        {
           

            if (ModelState.IsValid)
            {

                var dateofB = value.DOB.Date.ToString("dd/MM/yyyy");

                var email = value.Phone;
                

                try
                {
                    _logger.LogInformation("{user} was successfully Updated", value.Phone);
                    var userexist = _context.User.Any(p => p.Email == email);
                    if (!userexist)
                    {
                        var user = new ApplicationUser { UserName = value.Phone, PhoneNumber = value.Phone };
                        var result = await _userManager.CreateAsync(user, value.Password);
                        _logger.LogInformation("Account created for {user}", value.Phone);
                        var NewMember = new User
                        {
                            ApplicationUserId = user.Id,
                            FirstName = value.FirstName,
                            LastName = value.LastName,
                            Phone = email,
                            Email = email,
                            DOB = dateofB,
                            Gender = value.Gender == "Male" ? Gender.Male : Gender.Female,
                            Id = Guid.NewGuid(),
                            BankName = value.BankName,
                            AccountNumber = value.AccountNumber
                        };
                        _context.Add(NewMember);


                        var wal = new Wallet
                        {
                            Id = Guid.NewGuid(),
                            UserId = NewMember.Id,
                            DateCreated = DateTime.Now
                        };
                        _context.Add(wal);

                        await _context.SaveChangesAsync();
                        var token = GenerateJWTToken();
                        var infoo = new { Id = NewMember.Id, Email = NewMember.Email, FirstName = NewMember.FirstName, LastName = NewMember.LastName, Phone = NewMember.Phone, Token = token, DOB = dateofB, Pin = "0000" };
                        return Ok(infoo);

                    }
                    else
                    {
                        
                        return Ok(new { Status = "fail", message = "User Already Exist" });
                    }


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error encountered while creating account");
                }
            }
            return BadRequest();
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel value)
        {
            _logger.LogInformation("{user} tries to log in",value.Phone);
            var email = value.Phone;
            
            var user = await _userManager.FindByNameAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, value.Password))
            {
                _logger.LogInformation("{user} successfuly to logged in", value.Phone);
                var userr = await _context.User.SingleOrDefaultAsync(p => p.Email == email);

                if (userr == null)
                {
                    return NotFound();
                }
                var token = GenerateJWTToken();
                

                var info = new { Id = userr.Id, Email = userr.Email, FirstName = userr.FirstName, LastName = userr.LastName, Phone = userr.Phone, Token = token, DOB = userr.DOB, Passport = userr.Passport };
                return Ok(info);
            }
            _logger.LogInformation("{user} couldn't log in due to wrong password or unregistered phone", value.Phone);
            return BadRequest();
        }



        [HttpPost("LogSession")]
        public async Task<IActionResult> LogSession([FromBody] Login value)
        {
            
            _context.Add(value);
            _context.SaveChanges();
            var loggeduser = _context.Logins.Where(p => p.UserId == value.UserId && p.SessionId != value.SessionId);
            if (loggeduser.Any(k => k.LoggedIn == true))
            {
                var loggedinusers = loggeduser.Where(l => l.LoggedIn == true);
                foreach (var item in loggedinusers)
                {
                    item.LoggedIn = false;
                    _context.Update(item);
                }
                _context.SaveChanges();
            }
            return Ok();
        }
       

        [HttpPost("CheckSession")]
        public async Task<IActionResult> CheckSession( [FromBody] Login log)
        {
            var loggedinuser = await _context.Logins.SingleOrDefaultAsync(l => l.SessionId == log.SessionId);
            if (loggedinuser.LoggedIn)
            {
                return Ok(new { status = "ok" });
            }
            else
            {
                return Ok(new { status = "notok" });
            }
            
        }



        string GenerateJWTToken()
        {
            _logger.LogInformation("Generating Token..");
            var signingkey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

            int expiryInDays = Convert.ToInt32(_configuration["Jwt:ExpiryHours"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Site"],
                audience: _configuration["Jwt:Site"],
                expires: DateTime.UtcNow.AddHours(expiryInDays),
                signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        async Task<string> GenerateToken()
         {
            _logger.LogInformation("Generating Token..");
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://flashmoney.cyhermes.com/IDP");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);

            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "WebApp",
                ClientSecret = "secret",
                Scope = "FlashApi"
            });

            if (tokenResponse.IsError)
            {
                Debug.WriteLine(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
         }
    }
}
