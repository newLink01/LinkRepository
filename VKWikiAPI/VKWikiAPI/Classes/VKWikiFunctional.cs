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

        private List<string> VKGetTextsFromPosts()
        {
            string responseFromServer = null;
            string url = "https://api.vk.com/method/wall.get?count=1&offset=0&owner_id=166724944";

            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            // про предлоги спросить 
            //про то , сколько обрабатывать постов
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

             return textsOfThePosts; 
           
        }




        public object GetCountOfWords() { //SetWeightsOfWordsInPosts
            List<string> posts = this.VKGetTextsFromPosts();
            List<string> allWords = new List<string>();//all words of all posts
            Dictionary<string, int> countOfWords = new Dictionary<string, int>();//word , count of this word in total posts
            

         
            string [] preparedPost = null;

            foreach (var current in posts) { //нашли все слова
                preparedPost = current.ToLower().Split(new char[] {'$','!','&','?','.',' ','-'});
                foreach (var c in preparedPost) { allWords.Add(c); }
            }

            int numberOfOccurrences = 0;

            foreach (var currentWord in allWords)
            {
                if (!countOfWords.ContainsKey(currentWord))
                {
                    numberOfOccurrences = allWords.Where(x => x == currentWord).Count();
                    countOfWords.Add(currentWord, numberOfOccurrences);
                    numberOfOccurrences = 0;
                }

            }
                var numberOfOccurencesToJSON = JsonConvert.SerializeObject(numberOfOccurrences);
                return numberOfOccurrences;
        }

        
        public string WikiGetLinksBy(string word) {
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