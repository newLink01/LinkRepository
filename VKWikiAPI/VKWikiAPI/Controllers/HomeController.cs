using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VKWikiAPI.Controllers
{
    public class HomeController : Controller
    {
        private string AccessToken { set; get; } = "blabla";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetToken(string accessToken) {
            this.AccessToken = accessToken;
            return View();
        }




    }
}