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

      

        private List<string> VKGetTextsFromPosts()
        {
            string responseFromServer = null;
            string url = "https://api.vk.com/method/wall.get?count=3&offset=0&owner_id=166724944";

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

             return textsOfThePosts; 
           
        }




        public JArray VKGetKeyWords() { //SetWeightsOfWordsInPosts

            List<string> posts = this.VKGetTextsFromPosts();
            List<string> allWords = new List<string>();//all words of all posts
            Dictionary<string, double> freqOfWords = new Dictionary<string, double>();//word , freq in total posts
            Dictionary<string, double> weightOfWords = new Dictionary<string, double>();
            int countOfLinks = 0;
            double averageWeight = 0;

            string [] preparedPost = null;



            foreach (var current in posts) { //нашли все слова
                preparedPost = current.ToLower().Split(new char[] {'$','!','&','?','.',' ','-'});
                foreach (var c in preparedPost) { allWords.Add(c); }
            }



            int numberOfOccurrences = 0;
            foreach (var currentWord in allWords)
            {
                if (!freqOfWords.ContainsKey(currentWord))
                {
                    numberOfOccurrences = allWords.Select(x => x == currentWord).Count();
                    freqOfWords.Add(currentWord, numberOfOccurrences/allWords.Count); //word freq at the total words
                    numberOfOccurrences = 0;
                }
            }



            foreach (var word in freqOfWords) {
                if (!weightOfWords.ContainsKey(word.Key)) {

                    countOfLinks = this.WikiGetCountOfLinksTo(word.Key);
                    if (countOfLinks <= 5) { continue; }

                    weightOfWords.Add(word.Key, countOfLinks * word.Value);

                }
            }

            averageWeight = weightOfWords.Sum(freq => freq.Value) / weightOfWords.Count;
            string keyWords = JsonConvert.SerializeObject(weightOfWords.Where(pair => pair.Value >= averageWeight).Select(x => x.Key).ToList());

            return JArray.FromObject(weightOfWords.Where(pair => pair.Value >= averageWeight).Select(x => x.Key).ToList());
           
        }

        










        public int WikiGetCountOfLinksTo(string word) {
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
            if (result.Count != 0 &&result[3].Count()>=5)
            {
                return result[3].Count();
            }

            return -1;
        }
        

    }
}