using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlashMoney.DTO;
using FlashMoney.Hubs;
using FlashMoney.Models;
using FlashMoney.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace FlashMoney.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private IFlashHttpClient _flashMoneyHttpClient;
        private ILogger<AuthController> _logger;
        private IHubContext<ChatHub> _NotificationHub;
        

        public AuthController(IFlashHttpClient flashMoneyHttpClient,ILogger<AuthController> logger, IHubContext<ChatHub> chatHub)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _logger = logger;
            _NotificationHub = chatHub;
            
        }


        public IActionResult Register(string phone = null)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthenticationModel AuthenticationModel)
        {
            if (ModelState.IsValid)
            {
                    if (!string.IsNullOrEmpty(AuthenticationModel.BvnOrPhoneModel?.Phone))
                    {
                       
                        try
                        {
                            var addr = new System.Net.Mail.MailAddress(AuthenticationModel.BvnOrPhoneModel.Phone);
                  
                        }
                        catch
                        {
                            var errormessage = new TransferStatus { Message = "Invalid Email Address" };
                            return PartialView("_RegistrationError", errormessage);
                        }
                                           
                        using (var client = _flashMoneyHttpClient.GetClient())
                        {

                            var json = JsonConvert.SerializeObject(AuthenticationModel.BvnOrPhoneModel);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            var response = await client.PostAsync("PhoneBvn", content);
                            if (response.IsSuccessStatusCode)
                            {
                                var b = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<UserCheckInfo>(b);

                                if (result.Status == "success" && result.ReasonPhrase == "Phone: New User")
                                {
                                    var gjh = new OTPValidationModel { Phone = AuthenticationModel.BvnOrPhoneModel.Phone};
                                    return PartialView("_ValidatePhone", gjh);
                                    //return RedirectToAction("ValidatePhone", "Auth", new { phone = phonee });
                                }
                                else if (result.Status == "failed")
                                {
                                    var gjh = new OTPValidationModel { Phone = AuthenticationModel.BvnOrPhoneModel.Phone };
                                    return PartialView("_ValidatePhone", gjh);
                                    //return RedirectToAction("ValidatePhone", "Auth", new { phone = phonee });
                                }
                                else if (result.Status == "Existing User")
                                {
                                    var gjh = new OTPValidationModel { Phone = AuthenticationModel.BvnOrPhoneModel.Phone };
                                    return PartialView("_VerifyPin", gjh);
                                    //return RedirectToAction("VerifyPin", "Auth", new { phone = phonee });
                                }

                            }
                            ModelState.AddModelError(string.Empty, "OTP Could not be sent. Please try again");
                        }
                }
            }
            return PartialView("_RegistrationError");
            // return View(AuthenticationModel.BvnOrPhoneModel);
        }

       
       
        public IActionResult VerifyPin(string phone)
        {
            var gjh = new OTPValidationModel { Phone = phone };
            return View(gjh);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPin(OTPValidationModel oTP)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var otp = $"{oTP.One}{oTP.Two}{oTP.Three}{oTP.Four}{oTP.Five}{oTP.Six}";

                    var json = JsonConvert.SerializeObject(new { oTP.Phone, Otp = otp });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("VerifyPin", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<UserCheckInfo>(b);

                        if (result.Status == "fail")
                        {
                            ViewData["LoginState"] = "Wrong PIN";
                            return PartialView("_VerifyPin", oTP);
                        }
                        //result.Phone = oTP.Phone;
                        //return RedirectToAction("EXSignUp", "Auth", result);
                        var gjh = new SignUpViewmodel
                        {
                            Phone = result.Phone,
                            FirstName = result.FirstName,
                            LastName = result.LastName,
                            date = result.DOB,
                            Gender = result.Gender
                        };
                        return PartialView("_EXSignUp", gjh);
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            //return View(oTP);
            return PartialView("_VerifyPin", oTP);
        }


        public IActionResult EXSignUp(UserCheckInfo userCheckInfo)
        {
            var gjh = new SignUpViewmodel {
                Phone = userCheckInfo.Phone,
                FirstName = userCheckInfo.FirstName,
                LastName = userCheckInfo.LastName,
                date = userCheckInfo.DOB,
                Gender = userCheckInfo.Gender
            };
            return View(gjh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EXSignUp(SignUpViewmodel signUpViewmodel)
        {
            if (ModelState.IsValid)
            {
                DateTime dateofB;
                var d = DateTime.TryParseExact(signUpViewmodel.date, "dd/MM/yyyy", null, DateTimeStyles.None, out dateofB);
                if (!d)
                {
                    var dd = DateTime.TryParseExact(signUpViewmodel.date, "dd/MM/yy", null, DateTimeStyles.None, out dateofB);
                    if (!dd)
                    {
                        ViewData["LoginState"] = "Invalid Date Format";
                        return PartialView("_EXSignUp", signUpViewmodel);
                    }
                }

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    signUpViewmodel.DOB = dateofB;
                    var json = JsonConvert.SerializeObject(signUpViewmodel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("Create", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var gg = JsonConvert.DeserializeObject<LoginDTO>(b);
                        if (gg.Status == "fail")
                        {
                            ViewData["LoginState"] = gg.message;
                            return PartialView("_SignUp", signUpViewmodel);
                        }
                        if (gg != null)
                        {
                            await SignInUser(gg);
                            return PartialView("_RegistrationSuccess");
                            //return RedirectToAction("Index","Home");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }
            }
            return PartialView("_EXSignUp", signUpViewmodel);
        }

        public IActionResult ValidatePhone(string phone)
        {
            var gjh = new OTPValidationModel { Phone = phone };
            return View(gjh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidatePhone(OTPValidationModel oTP)
        {
            if (ModelState.IsValid)
            {
                    using (var client = _flashMoneyHttpClient.GetClient())
                    {
                     var otp = $"{oTP.One}{oTP.Two}{oTP.Three}{oTP.Four}{oTP.Five}{oTP.Six}";

                        var json = JsonConvert.SerializeObject(new {oTP.Phone,Otp = otp});
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("ValidateOTP", content);
                        if (response.IsSuccessStatusCode)
                        {
                           var b = await response.Content.ReadAsStringAsync();
                           var result = JsonConvert.DeserializeObject<UserCheckInfo>(b);

                            if (result.Status == "fail")
                            {
                                ViewData["LoginState"] = "Wrong OTP";
                                return PartialView("_ValidatePhone",oTP);
                            }

                            if (result.ReasonPhrase == "New User")
                            {
                                var g = new SignUpViewmodel { Phone = result.Phone };
                                //return RedirectToAction("SignUp", "Auth", result);
                                return PartialView("_SignUp", g);
                            }
                            else
                            {
                                var g = new SignUpViewmodel
                                {
                                    FirstName = result.FirstName,
                                    LastName = result.LastName,
                                    Phone = result.Phone
                                };
                                if (!string.IsNullOrEmpty(result.DOB))
                                    g.date = result.DOB;
                                 if (!string.IsNullOrEmpty(result.Gender))
                                g.Gender = result.Gender == "M" ? "Male" : "Female";
                                return PartialView("_SignUp", g);
                                //return RedirectToAction("SignUp", "Auth", result);
                            }
             
                        }
                        ModelState.AddModelError(string.Empty, "Error");
                    }
                
            }
            return PartialView("_ValidatePhone", oTP);
        }




        public IActionResult SignUp(UserCheckInfo userCheckInfo)
        {
            if (userCheckInfo.ReasonPhrase == "New User")
            {
                var g = new SignUpViewmodel { Phone = userCheckInfo.Phone };
                return View(g);
            }
            else
            {
                var g = new SignUpViewmodel
                {
                    FirstName = userCheckInfo.FirstName,
                    LastName = userCheckInfo.LastName,
                    Phone = userCheckInfo.Phone,

                    
                };
                if (!string.IsNullOrEmpty(userCheckInfo.DOB))
                    g.date =userCheckInfo.DOB;
                //if (!string.IsNullOrEmpty(userCheckInfo.Gender))
                //    g.Gender = userCheckInfo.Gender == "M" ? Gender.Male : Gender.Female;
                  
                return View(g);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewmodel  signUpViewmodel)
        {
            if (ModelState.IsValid)
            {
                DateTime dateofB;
                var d = DateTime.TryParseExact(signUpViewmodel.date,"dd/MM/yyyy",null,DateTimeStyles.None,out dateofB);
                
                if (!d)
                {
                    var dd = DateTime.TryParseExact(signUpViewmodel.date, "dd/MM/yy", null, DateTimeStyles.None, out dateofB);
                    if (!dd)
                    {
                        ViewData["LoginState"] = "Invalid Date Format";
                        return PartialView("_SignUp", signUpViewmodel);
                    }
                   
                }
                if (dateofB >= DateTime.Today || dateofB.Year < 1800)
                {
                    ViewData["LoginState"] = "Invalid Date of Birth";
                    return PartialView("_SignUp", signUpViewmodel);
                }
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    signUpViewmodel.DOB = dateofB;
                    var json = JsonConvert.SerializeObject(signUpViewmodel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("Create", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var gg = JsonConvert.DeserializeObject<LoginDTO>(b);
                        if(gg.Status == "fail")
                        {
                            ViewData["LoginState"] = gg.message;
                            return PartialView("_SignUp", signUpViewmodel);
                        }
                        if (gg != null)
                        {
                            gg.session = HttpContext.Session.Id;
                            var log = new Login { LoggedIn = true, SessionId = gg.session, UserId = signUpViewmodel.Email };
                            var json2 = JsonConvert.SerializeObject(log);
                            var content2 = new StringContent(json2, Encoding.UTF8, "application/json");

                            var response2 = await client.PostAsync("LogSession", content2);
                            
                            await SignInUser(gg);
                           
                            //return RedirectToAction("RegistrationSuccess", new { pin = gg.Pin });
                            var g = new PinDTO { Pin = gg.Pin};
                            return PartialView("_RegistrationSuccess",g);
                        }
                    }
                    ModelState.AddModelError(string.Empty, "An Error was encountered while creating your account. Please try again");
                }
            }
            //ViewData["LoginState"] = ViewData.ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage;
            return PartialView("_SignUp", signUpViewmodel);
        }

        public IActionResult RegistrationSuccess(string Pin)
        {
            var g = new PinDTO { Pin = Pin };
            return View(g);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendCode(OTPValidationModel oTP)
        {
            if (!string.IsNullOrEmpty(oTP.Phone))
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(new { oTP.Phone});
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("ResendCode", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("ValidatePhone", new { phone = Phone });
                        var gjh = new OTPValidationModel { Phone = oTP.Phone};
                        return PartialView("_ValidatePhone", gjh);
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            //return RedirectToAction("ValidatePhone",new { phone = Phone });
            return PartialView("_RegistrationError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendResetCode(OTPValidationModel reset)
        {
            if (!string.IsNullOrEmpty(reset.Phone))
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(new { reset.Phone });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("ResendCode", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("ValidateResetCode", new { phone = reset.Phone });
                        return PartialView("_ValidateResetCode", reset);
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            //return RedirectToAction("ValidateResetCode", new { phone = Phone });
            return PartialView("_PasswordRestError");
        }




        //public IActionResult Pin(SignUpViewmodel signUpViewmodel )
        //{
        //    var PSV = new PinSignUpViewModel
        //    {
        //        DOB = signUpViewmodel.DOB,
        //        FirstName = signUpViewmodel.FirstName,
        //        Gender = signUpViewmodel.Gender,
        //        LastName = signUpViewmodel.LastName,
        //        Password = signUpViewmodel.Password,
        //        Phone = signUpViewmodel.Phone,

        //    };
        //        return View(PSV);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Pin(PinSignUpViewModel signUpViewmodel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var client = _flashMoneyHttpClient.GetClient())
        //        {
        //            var json = JsonConvert.SerializeObject(signUpViewmodel);
        //            var content = new StringContent(json, Encoding.UTF8, "application/json");
        //            var response = await client.PostAsync("Create", content);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var b = await response.Content.ReadAsStringAsync();
        //                var gg = JsonConvert.DeserializeObject<LoginDTO>(b);
        //                if (gg != null)
        //                {
        //                    await SignInUser(gg);
        //                    return RedirectToAction("RegistrationSuccess");
        //                }
        //            }
        //            ModelState.AddModelError(string.Empty, "Error");
        //        }
        //    }
        //    return View(signUpViewmodel);
        //}

        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(AuthenticationModel AuthModel)
        {
            if (!string.IsNullOrEmpty(AuthModel.ResetDTO.Phone))
            {
                var phonee = $"234{AuthModel.ResetDTO.Phone.Substring(AuthModel.ResetDTO.Phone.Length - 10)}";

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(new { Phone = phonee});
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("ResendCode", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("ValidateResetCode", new { phone = phonee});
                        return PartialView("_ValidateResetCode", new OTPValidationModel { Phone = phonee});
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            //return View(reset);
            return PartialView("_PasswordRestError");
        }

        public IActionResult ValidateResetCode(string Phone)
        {
            return View(new OTPValidationModel { Phone = Phone});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateResetCode(OTPValidationModel reset)
        {
            if (!string.IsNullOrEmpty(reset.Phone))
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var otp = $"{reset.One}{reset.Two}{reset.Three}{reset.Four}{reset.Five}{reset.Six}";
                    var json = JsonConvert.SerializeObject(new { reset.Phone, Otp = otp });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("ValidateResetCode", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PasswordResetDTO>(b);

                        if (result.Status == "fail")
                        {
                            //ModelState.AddModelError(string.Empty, result.ReasonPhrase);
                            //return View(reset);
                            ViewData["LoginState"] = "Wrong OTP";
                            return PartialView("_ValidateResetCode",reset);
                        }

                        //return RedirectToAction("ResetPassword", new { phone = reset.Phone,result.Rcode});
                        return PartialView("_ResetPassword", new PasswordChangeViewModel { Phone = reset.Phone, RCode = result.Rcode });
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return PartialView("_ValidateResetCode", reset);
        }


        public IActionResult ResetPassword(string Phone,string RCode)
        {
            return View(new PasswordChangeViewModel { Phone = Phone,RCode = RCode});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(PasswordChangeViewModel reset)
        {
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(reset);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("ResetPassword", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("PasswordResetSuccess", new { phone = reset.Phone });
                        return PartialView("_PasswordResetSuccess");
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            //return View(reset);
            return PartialView("_ResetPassword",reset);
        }



        public IActionResult PasswordResetSuccess()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasswordResetSuccess(string Pin)
        {
            return RedirectToAction("SignIn");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var phone = HttpContext.User?.Claims.FirstOrDefault(p => p.Type == "phone")?.Value;
            await _NotificationHub.Clients.User(phone).SendAsync("Notify", "Logout");
            return RedirectToAction("SignIn");
        }

        public IActionResult SignIn()
        {
            _logger.LogInformation("User requested the Login page");
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(AuthenticationModel AuthenticationModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                using(var client = _flashMoneyHttpClient.GetClient())
                {

                    var log = new Login { LoggedIn = true, SessionId = HttpContext.Session.Id, UserId = AuthenticationModel.SignInViewModel.Phone.Substring(AuthenticationModel.SignInViewModel.Phone.Length - 10) };
                    var json2 = JsonConvert.SerializeObject(log);
                    var content2 = new StringContent(json2, Encoding.UTF8, "application/json");
                    var response2 = await client.PostAsync("LogSession", content2);
                    if (response2.IsSuccessStatusCode)
                    {
                        await _NotificationHub.Clients.User($"234{AuthenticationModel.SignInViewModel.Phone.Substring(AuthenticationModel.SignInViewModel.Phone.Length - 10)}").SendAsync("Notify", "NewUser");

                    }

                    var json = JsonConvert.SerializeObject(AuthenticationModel.SignInViewModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var st = await response.Content.ReadAsStringAsync();
                        var gg = JsonConvert.DeserializeObject<LoginDTO>(st);
                        
                        if (gg != null)
                        {
                            gg.session = HttpContext.Session.Id;
                           
                            
                            await SignInUser(gg);
                           

                            if (returnUrl != null)
                            {
                                return Redirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(AuthenticationModel);
                }
            }
            return View(AuthenticationModel);
        }

        async Task SignInUser(LoginDTO gg)
        {
      
                var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier,gg.Id.ToString()),
                                new Claim("firstname",gg.FirstName),
                                 new Claim("lastname",gg.LastName),
                                 new Claim("fullname",gg.FirstName+" "+gg.LastName),
                                  new Claim("phone",gg.Phone),
                                  new Claim("email",gg.Email),
                                   new Claim("dob",gg.DOB),
                                   new Claim("token",gg.Token),
                                   new Claim("sessionId",gg.session),
                };

                //if (!string.IsNullOrEmpty(gg.Email))
                //{
                //    claims.Add(new Claim("email", gg.Email));
                //}
                if (!string.IsNullOrEmpty(gg.Passport))
                {
                    claims.Add(new Claim("passport", gg.Passport));
                }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "firstname", null);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = false
                });
            
        }
    }
}