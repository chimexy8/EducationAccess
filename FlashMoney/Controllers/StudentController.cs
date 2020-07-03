using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EducationAccess.Models;
using FlashMoney.Controllers;
using FlashMoney.DTO;
using FlashMoney.Hubs;
using FlashMoney.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace EducationAccess.Controllers
{
    public class StudentController : Controller
    {
        private IFlashHttpClient _flashMoneyHttpClient;
        private ILogger<AuthController> _logger;

        public StudentController(IFlashHttpClient flashMoneyHttpClient, ILogger<AuthController> logger)
        {
            _flashMoneyHttpClient = flashMoneyHttpClient;
            _logger = logger;
          
        }
        // GET: StudentController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ScholarshipPayConfirm(string reference, string Email, string Phone)
        {
            try
            {
                //var uID = Request.QueryString["uID"];
                var msg = string.Concat("Parameter received was ", reference);
                Trace.TraceInformation(msg);
                //verify and save paystack transaction entity
                string uri = "https://api.paystack.co/transaction/verify/" + reference;
                var client = new RestClient(uri);
                var request = new RestRequest(Method.GET);

                var opt = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                request.AddHeader("Authorization", "Bearer sk_test_c776a5efc4aecc338462ce8bddccbb27dac44cbd");
                var response2 = client.Execute(request);
                var response = JsonConvert.DeserializeObject<PaymentModel>(response2.Content);

                if (response.status && response.data.status == "success" && response.data.amount == 100000)
                {

                    //var amt2 = AMOUNT.Split("?");
                    //var amt = amt2[0];
                    //var transId = TransactionId.Split("?");
                    //using (var client = _flashMoneyHttpClient.GetClient())
                    //{
                    //    var json = JsonConvert.SerializeObject(transId[0]);
                    //    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    //    var response = await client.PostAsync("Paychec", content);
                    //    if (response.IsSuccessStatusCode)
                    //    {
                    //        var b = await response.Content.ReadAsStringAsync();
                    //        var paymentcheck = JsonConvert.DeserializeObject<PaymentCheckAcc>(b);
                    //        string value = $"{amt}0000000001{paymentcheck.TransactionId}"+"https://localhost/44380/Students/ScholarshipPayConfirm/?reference="+$"{TransactionId}&email={Email}&ZoneId={ZoneId}&AMOUNT={amt}&FullName={fullname}&TransactionId={TransactionId}" + "DEMO_KEY";
                    //        string md5hash = CreateMD5Hash(value);
                    //        if (paymentcheck.CheckSum==md5hash)
                    //        {
                    //            return Json("Ok");
                    //        }
                    //        return Json("Ok");
                    //    }

                    //        //What E-Tranzact should do if Payment is successful
                    //        if (SUCCESS == "0")
                    //        {
                    //var invoice = new Invoice
                    //{
                    //    DateAdded = DateTime.Now,
                    //    AmountPaid = Decimal.Parse(amt[0]),
                    //    Status = Convert.ToInt32(SUCCESS),
                    //    MemberId = 1,
                    //    ReferenceNumber = reference
                    //};
                    //_context.Invoices.Add(invoice);
                    //_context.SaveChanges();

                    //var donation = new Donation
                    //{
                    //    Email = Email,
                    //    Amount = Decimal.Parse(amt[0]),
                    //    Message = "Donation",
                    //    Status = "True",
                    //    ReferenceId = reference,
                    //    InvoiceId = invoice.InvoiceId.ToString(),
                    //    Name = Email,
                    //    DonationType = don[0],
                    //    DateAdded = DateTime.Now
                    //};
                    //_context.Donations.Add(donation);
                    //_context.SaveChanges();
                    //ViewData["msg"] = "Donation Recieved";


                    //using (var mail = new MailMessage())
                    //{
                    //    const string email = "nes@maxfront.com";
                    //    const string password1 = "admin@3030";

                    //    var loginInfo = new NetworkCredential(email, password1);

                    //    mail.From = new MailAddress(email);
                    //    mail.To.Add(new MailAddress(Email));


                    //    mail.Subject = "Donation Confirmation";
                    //    var body = _context.EmailTemplates.FirstOrDefault(x => x.Name == "Donation Confirmation").Body;
                    //    body = body.Replace("{email}", Email);
                    //    mail.Body = body;
                    //    mail.IsBodyHtml = true;

                    //    try
                    //    {
                    //        using (var smtpClient = new SmtpClient("mail.maxfront.com", 587))
                    //        {
                    //            smtpClient.EnableSsl = true;
                    //            smtpClient.UseDefaultCredentials = false;
                    //            smtpClient.Credentials = loginInfo;
                    //            smtpClient.Send(mail);

                    //        }
                    //    }
                    //    finally
                    //    {
                    //        //dispose the client
                    //        mail.Dispose();
                    //    }
                    //}

                    return Json("Done");

                }
                return Json("Done");
            }

            catch (Exception e)
            {
                _logger.LogInformation("Message displayed: {Message}", e.Message);
                ViewData["Message"] = e.Message;
                return LocalRedirect("~/");
            }


        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
