using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlashMoney.DTO;
using FlashMoney.Models;
using FlashMoney.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlashMoney.Controllers
{
    public class SettingsController : Controller
    {
        private IFlashMoneyHttpClient _flashMoneyHttpClient;
        private readonly IHostingEnvironment _hostingEnv;
        ImageService imageService = new ImageService();
        public SettingsController(IFlashMoneyHttpClient flashMoneyHttpClient, IHostingEnvironment hostingEnv)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _hostingEnv = hostingEnv;
        }

        public async Task<IActionResult> Index(int? update)
        {
            if (update==null)
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var response = await client.GetAsync($"UserProfile/{phone}");
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ProfileViewModel>(b);
                        return View(result);
                    }
                }
                return View(new ProfileViewModel());
            }
            else
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var response = await client.GetAsync($"UserProfile/{phone}");
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ProfileViewModel>(b);
                        ViewData["UpdateStatus"] = "Your Information has been successfuly updated";
                        return View(result);
                    }
                }
                return View(new ProfileViewModel());
            }
          
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProfileViewModel profileViewModel)
        {
            bool changeddp = false;
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            profileViewModel.Phone = phone;
            DateTime dateofB;
            var d = DateTime.TryParseExact(profileViewModel.DOB, "dd/MM/yyyy", null, DateTimeStyles.None, out dateofB);
            if (!d)
            {
                var dd = DateTime.TryParseExact(profileViewModel.DOB, "dd/MM/yy", null, DateTimeStyles.None, out dateofB);
                if (!dd)
                {
                    ViewData["UpdateState"] = "Invalid Date Format";
                    return View("Index");
                }
            }
            if (profileViewModel.PictureFile != null && FormFileExtensions.IsImage(profileViewModel.PictureFile))
            {
                //upload files to wwwroot/docs
                //var fileName = Guid.NewGuid() + Path.GetExtension(profileViewModel.PictureFile.FileName).ToLower();
                ////MAKE SURE YOU CREATE a docs folder in wwwroot
                //var filePath = Path.Combine(_hostingEnv.WebRootPath, "docs", fileName);

                //using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //{
                //    await profileViewModel.PictureFile.CopyToAsync(fileSteam);
                //}
                ////your logic to save filePath to database, for example

               
                var imageUrl = await imageService.UploadImageAsync(profileViewModel);
                profileViewModel.Passport = imageUrl;
                changeddp = true;
                
            }
            if (string.IsNullOrEmpty(profileViewModel.FirstName)  || string.IsNullOrEmpty(profileViewModel.LastName)|| string.IsNullOrEmpty(profileViewModel.Phone)
                    || string.IsNullOrEmpty(profileViewModel.DOB) || string.IsNullOrEmpty(profileViewModel.Gender))
                {
               
                    ViewData["UpdateState"] = "First name, Last name, Phone number, Date of Birth and Gender must be provided.";
                    return View("Index");
                }
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    profileViewModel.DOB = dateofB.ToString("dd/MM/yyyy");
                    var json = JsonConvert.SerializeObject(profileViewModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"UpdateUser/{profileViewModel.Phone}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        if (changeddp)
                        {

                            var firstname = User.Claims.FirstOrDefault(p => p.Type == "firstname").Value;
                            var lastname = User.Claims.FirstOrDefault(p => p.Type == "lastname").Value;
                            var dob = User.Claims.FirstOrDefault(p => p.Type == "dob").Value;
                            var token = User.Claims.FirstOrDefault(p => p.Type == "token").Value;
                            var Id = User.FindFirstValue(ClaimTypes.NameIdentifier).FirstOrDefault();
                            
                            
                            
                                var claims = new List<Claim>
                                        {
                                            new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                                            new Claim("firstname",profileViewModel.FirstName),
                                             new Claim("lastname",profileViewModel.LastName),
                                             new Claim("fullname",profileViewModel.FirstName+" "+profileViewModel.LastName),
                                              new Claim("phone",phone),
                                               new Claim("dob",profileViewModel.DOB),
                                               new Claim("token",token),
                                               new Claim("passport",profileViewModel.Passport),
                                };


                              


                                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "firstname", null);
                                var principal = new ClaimsPrincipal(identity);

                                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                                {
                                    IsPersistent = false
                                });


                    }


                   
                    //return View(profileViewModel);
                    return RedirectToAction("Index", new { update=1} );
                }
                    ModelState.AddModelError(string.Empty, "Error");
                }
               


            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> KYC(ProfileViewModel profileViewModel)
        //{
        //    var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
        //    profileViewModel.Phone = phone;
        //    if (ModelState.IsValid)
        //    {

        //        using (var client = _flashMoneyHttpClient.GetClient())
        //        {
        //            var json = JsonConvert.SerializeObject(profileViewModel);
        //            var content = new StringContent(json, Encoding.UTF8, "application/json");
        //            var response = await client.PutAsync($"UpdateUserKYC/{profileViewModel.Phone}", content);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            ModelState.AddModelError(string.Empty, "Error");
        //        }

        //    }
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NextOfKin(ProfileViewModel profileViewModel)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            profileViewModel.Phone = phone;
            if (ModelState.IsValid)
            {
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(profileViewModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"UpdateNextOfKin/{profileViewModel.Phone}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["UpdateStatus"] = "Your Information has been successfuly updated";
                        
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return RedirectToAction("Index");
        }


        public IActionResult Password()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Password(PasswordChangeViewModel passwordChangeViewModel)
        {
        
            if (ModelState.IsValid)
            {
                var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
                passwordChangeViewModel.Phone = phone;
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(passwordChangeViewModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"PasswordChange", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);
                        if (result.status == "fail")
                        {
                            ViewData["UpdateState"] = result.message;
                            return View(passwordChangeViewModel);
                        }
                        ViewData["UpdateStatus"] = "Success";
                        return View(passwordChangeViewModel);
                    }
                    ModelState.AddModelError(string.Empty, "Wrong Password");
                }

            }
            return View(passwordChangeViewModel);
        }



        public IActionResult AuthPin()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthPin(ResetPinDTO resetPinDTO)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            resetPinDTO.OldPin = resetPinDTO.OldPin1 + resetPinDTO.OldPin2 + resetPinDTO.OldPin3 + resetPinDTO.OldPin4;
            resetPinDTO.NewPin = resetPinDTO.NewPin1 + resetPinDTO.NewPin2 + resetPinDTO.NewPin3 + resetPinDTO.NewPin4;
            resetPinDTO.Phone = phone;
            if (ModelState.IsValid)
            {

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(resetPinDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"ResetPin", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);
                        if (result.status == "fail")
                        {
                            ViewData["UpdateState"] = result.message;
                            return View(resetPinDTO);
                        }
                        ViewData["UpdateStatus"] = "Success";
                        return View(resetPinDTO);
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(resetPinDTO);
        }




        public async Task<IActionResult> TransactionPin()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;

            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.GetAsync($"UserProfile/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ProfileViewModel>(b);
                    var pr = new TwoFADTO { HasTransactionPin = result.HasTransactionPin, HasAuthPin=result.HasAuthPin };

                    return View(pr);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionPin(TwoFADTO twoFADTO)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            twoFADTO.Phone = phone;
            if (ModelState.IsValid)
            {
                twoFADTO.TwoFA = twoFADTO.TwoFA1 + twoFADTO.TwoFA2 + twoFADTO.TwoFA3 + twoFADTO.TwoFA4;
                twoFADTO.NewTwoFA=twoFADTO.NewTwoFA1+ twoFADTO.NewTwoFA2 + twoFADTO.NewTwoFA3 + twoFADTO.NewTwoFA4;
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(twoFADTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"Change2FA", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var b = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TransInfo>(b);
                        if (result.status == "fail")
                        {
                            int msg;
                            var response2 = await client.GetAsync($"UserProfile/{phone}");
                            if (response2.IsSuccessStatusCode)
                            {
                                var b2 = await response2.Content.ReadAsStringAsync();
                                var result2 = JsonConvert.DeserializeObject<ProfileViewModel>(b2);

                                twoFADTO.HasAuthPin = result2.HasAuthPin;
                                twoFADTO.HasTransactionPin = result2.HasTransactionPin;


                            }

                            if (int.TryParse(result.message, out msg))
                            {
                                ViewData["UpdateState"] = "You can't use the your authorization pin as your transaction pin, please choose a different 4 digit pin";
                            }
                            else
                            {
                                ViewData["UpdateState"] = result.message;
                            }
                           

                            return View(twoFADTO);
                        }
                        ViewData["UpdateStatus"] = "Success";
                        return View(twoFADTO);
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(twoFADTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionPinSetUp(TwoFADTO twoFADTO)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            twoFADTO.Phone = phone;
            if (string.IsNullOrEmpty(twoFADTO.NewTwoFA))
            {
                ModelState.AddModelError(string.Empty, "Error");
                return View(twoFADTO);
            }
            
                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(twoFADTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"Create2FA", content);
                    if (response.IsSuccessStatusCode)
                    {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TransInfo>(b);
                    if (result.status == "fail")
                    {
                        int msg;
                        TransferStatus gb;
                        if (int.TryParse(result.message, out msg))
                        {
                             gb = new TransferStatus { Message = "You can't use your authorization pin as your transaction pin, please choose a different 4 digit pin" };
                           
                        }
                        else
                        {
                             gb = new TransferStatus { Message = result.message };
                        }

                        return PartialView("_AddFailure",gb);
                    }
                      return PartialView("_AddSuccess");

                }
                    ModelState.AddModelError(string.Empty, "Error");
                }
            return PartialView("_AddFailure");
        }





        public async Task<IActionResult> Notification()
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;

            using (var client = _flashMoneyHttpClient.GetClient())
            {
                var response = await client.GetAsync($"UserProfile/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    var b = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ProfileViewModel>(b);
                    var pr = new NotificationDTO { AllowActivityNotif = result.AllowActivityNotif, AllowTransactionNotif = result.AllowTransactionNotif };

                    return View(pr);
                }
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Notification(NotificationDTO notificationDTO)
        {
            var phone = User.Claims.FirstOrDefault(p => p.Type == "phone").Value;
            notificationDTO.Phone = phone;
            if (ModelState.IsValid)
            {

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(notificationDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"Notif/{phone}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return View();
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(notificationDTO);
        }




        public IActionResult Support()
        {
            return View();
        }



        // Not used
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTwoFA(SettingsViewModel settingsViewModel)
        {

            if (ModelState.IsValid)
            {

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(settingsViewModel.TwoFADTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"Create2FA", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(settingsViewModel);
        }

        // Not used
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate2FA(SettingsViewModel settingsViewModel)
        {

            if (ModelState.IsValid)
            {

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(settingsViewModel.TwoFADTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"Authenticate2FA", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(settingsViewModel);
        }
        // Not used
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Change2FA(SettingsViewModel settingsViewModel)
        {

            if (ModelState.IsValid)
            {

                using (var client = _flashMoneyHttpClient.GetClient())
                {
                    var json = JsonConvert.SerializeObject(settingsViewModel.TwoFADTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"Change2FA", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(settingsViewModel);
        }

    }
}