using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Web.Mvc;
using ClinicManagement.Core;
using ClinicManagement.Core.Models;
using ClinicManagement.Core.ViewModel;
using Microsoft.AspNet.Identity;

namespace ClinicManagement.Controllers
{
    public class DentalChartController : Controller
    {
        
        public ActionResult Index()
        {
            var model = new DentalChartViewModel
            {
                ToothStatuses = new Dictionary<string, string>()
            };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
