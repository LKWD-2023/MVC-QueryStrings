using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult FormDemo()
        {
            return View();
        }

        public ActionResult FormSubmit(string address, string phone)
        {
            FormSubmitViewModel vm = new FormSubmitViewModel
            {
                Address = address,
                Phone = phone
            };

            return View(vm);
        }
    }
}