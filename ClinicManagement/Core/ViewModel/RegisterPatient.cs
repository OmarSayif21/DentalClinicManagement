using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicManagement.Core.ViewModel
{
    public class RegisterPatient : Controller
    {
        // GET: RegisterPatient
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegisterPatient/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterPatient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterPatient/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterPatient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisterPatient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterPatient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisterPatient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
