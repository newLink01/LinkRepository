using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace VKWikiAPI.Classes
{
    public class VKWikiFunctional
    {
        public string VKGetStatus(string accessToken) {
            if (accessToken == null) { return null; }
            string responseFromServer = null;
            
            string url = "https://api.vk.com/method/status.get?user_id=166724944&access_token=" + accessToken;
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            


            using (Stream dataStream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }
            return responseFromServer;
        }

        public object VKGetWall(string accessToken) {
            if (accessToken == null) { return null; }
            string responseFromServer = null;

            string url = "https://api.vk.com/method/status.get?user_id=166724944&access_token=" + accessToken;
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();



            using (Stream dataStream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }
            return responseFromServer;


        }

        public string VKGetFriends(string accessToken) {
            List<string> friends = new List<string>();

            string responseFromServer = null;
            string url = "https://api.vk.com/method/friends.get?user_id=166724944&access_token=" + accessToken;

            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();



            using (Stream dataStream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }
            return responseFromServer;
        }




    }
}