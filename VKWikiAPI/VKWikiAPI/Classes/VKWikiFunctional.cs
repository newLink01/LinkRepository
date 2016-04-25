using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using VKWikiAPI.ToJSONParse;
using Newtonsoft.Json;
namespace VKWikiAPI.Classes
{
    public class VKWikiFunctional
    {


        public object VKGetFriends(string accessToken)
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


            /*  JObject result = JObject.Parse(responseFromServer);
              JArray items = result["response"] as JArray;
              if (items == null) return null;

              List<string> friendsIds = new List<string>();

              foreach (var c in items) {
                  friendsIds.Add(c.ToString());
              }
              */

            JObject result = JObject.Parse(responseFromServer);
            List<string> friendsIds = new List<string>();

            foreach (var c in result["response"]) {
                friendsIds.Add(c.ToString());
            }

            var friendsIdsJSON = JsonConvert.SerializeObject(friendsIds);
            return friendsIdsJSON;
        }




      





        public JObject VKGetFirstPost(string accessToken)
        {
            string responseFromServer = null;
            string url = "https://api.vk.com/method/wall.get?count=3&access_token=" + accessToken;

            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();



            using (Stream dataStream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }
            JObject o = JObject.Parse(responseFromServer);
            return o;

        }

    }
}