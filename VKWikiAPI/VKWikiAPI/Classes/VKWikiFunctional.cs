using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace VKWikiAPI.Classes
{
    public class VKWikiFunctional
    {


        public object VKGetFriends()
        {
            List<string> friends = new List<string>();

            string responseFromServer = null;
            string url = "https://api.vk.com/method/friends.get?count=2&user_id=166724944&access_token=";

            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();


            using (Stream dataStream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }

            JObject result = JObject.Parse(responseFromServer);
            List<string> friendsIds = new List<string>();

            foreach (var c in result["response"]) {
                friendsIds.Add(c.ToString());
            }

            var friendsIdsJSON = JsonConvert.SerializeObject(friendsIds);
            return friendsIdsJSON;
        }
        public object VKGetTextsFromSomePosts()
        {
            string responseFromServer = null;
            string url = "https://api.vk.com/method/wall.get?count=1&offset=1&owner_id=166724944";

            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();

            using (Stream dataStream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }
            JObject result = JObject.Parse(responseFromServer);
            List<string> textsOfThePosts = new List<string>();

            foreach (var c in result["response"]) {
                if (c.Type != JTokenType.Object || c["text"].ToString().Length == 0) continue;
                textsOfThePosts.Add(c["text"].ToString());
            }
            

            var textsOfThePostsJSON = JsonConvert.SerializeObject(textsOfThePosts);
            return textsOfThePostsJSON;
        }



        public string WikiGetLinksBy(string word,string secondWord) {
            string responseFromServer = null;
            
            string url = "https://ru.wikipedia.org/w/api.php?action=opensearch&search="+ word +"&utf8=true";

            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();

            using (Stream dataStream = resp.GetResponseStream()) {
                using (StreamReader reader = new StreamReader(dataStream)) {
                    responseFromServer = reader.ReadToEnd();
                }
            }

            JArray result = JArray.Parse(responseFromServer);
            if (result.Count != 0)
            {
                return result[3].ElementAt(0).ToString();
            }
            return null;
        }
        

    }
}