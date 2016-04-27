﻿using System;
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
        public object GetFriends() {
            return functional.VKGetFriends();
        }

     

        [HttpGet]
        public string GetNumberOfLinks(string word) {
            return functional.WikiGetLinksBy(word);
        }

    }
}