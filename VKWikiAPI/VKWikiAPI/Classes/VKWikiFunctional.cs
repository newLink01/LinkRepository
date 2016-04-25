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



        public string VKGetFriendsJSON(string accessToken)
        {
            List<string> friends = new List<string>();

            string responseFromServer = null;
            string url = "https://api.vk.com/method/friends.get?count=2&user_id=166724944&access_token=" + accessToken;

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

        public string VKGetFirstPostJSON(string accessToken)
        {
            string responseFromServer = null;
            string url = "https://api.vk.com/method/wall.get?params[count]=1&params[user_id]=166724944&params[offset]=0&access_token=" + accessToken;

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