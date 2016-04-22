using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VKWikiAPI.Controllers
{
    public class HomeController : Controller
    {
        private string AccessToken { set; get; }
       [HttpGet]
        public ActionResult Index()
        {
            ViewBag.AccessToken = this.AccessToken;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string accessToken) {
            this.AccessToken = accessToken;
            ViewBag.AccessToken = this.AccessToken;
            return View();
        }


    }
}