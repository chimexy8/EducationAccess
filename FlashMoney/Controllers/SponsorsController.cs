using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationAccess.Controllers
{
    public class SponsorsController : Controller
    {
        // GET: SponsorsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SponsorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SponsorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SponsorsController/Create
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

        // GET: SponsorsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SponsorsController/Edit/5
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

        // GET: SponsorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SponsorsController/Delete/5
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
