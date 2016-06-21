using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VKWikiAPI.Classes;
using Newtonsoft.Json.Linq;
namespace VKWikiAPI.Controllers
{
    public class HomeController : Controller
    {
        public static string AccessToken { set; get; } 
        private VKWikiFunctional functional;

        public HomeController() {
            functional = new VKWikiFunctional();
            
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SetToken(string accessToken) {
           AccessToken = accessToken;
            return View();
        } 
      
       
        [HttpGet]
        public JObject GetKeyWords(string userId , string offset) {
           return JObject.FromObject(functional.VKGetKeywords(userId,offset));
        }

        [HttpGet]
        public JArray GetPosts(string userId,string offset) {
            return JArray.FromObject(functional.VKGetTextsFromPosts(userId, offset));
        }
    }
}