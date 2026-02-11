using DependencyInjectionDemo.NetFrameworkAspNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DependencyInjectionDemo.NetFrameworkAspNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValuesService valuesService;

        public HomeController(IValuesService valuesService)
        {
            this.valuesService = valuesService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
