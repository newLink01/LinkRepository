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
      

        public List<string> VKGetTextsFromPosts(string ownerId,string offset,string postsCount = "20")
            {
            string responseFromServer = null;
            string url = "https://api.vk.com/method/wall.get?count="+postsCount+"&offset="+offset+"&owner_id="+ownerId;

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
            if (result["error"] != null) { return new List<string>(); }

            foreach (var c in result["response"]) {
                if (c.Type != JTokenType.Object || c["text"].ToString().Length == 0) continue;
                textsOfThePosts.Add(c["text"].ToString());
            }

           /* foreach (var text in textsOfThePosts) {
                text.Remove(text.Substring(text.Ind('<'),text.Las))
            }*/
             return textsOfThePosts; 
           
        }

        public JObject VKGetKeyWords(string userId, string offset) { //SetWeightsOfWordsInPosts

            List<string> posts = this.VKGetTextsFromPosts(userId,offset); //posts = 30

            List<string> allWords = new List<string>();//all words of all posts
            Dictionary<string, double> freqOfWords = new Dictionary<string, double>();//word , freq in total posts
            Dictionary<string, double> weightOfWords = new Dictionary<string, double>();
            Dictionary<string,string> relatedLinksFromWiki = new Dictionary<string, string>(); //word and referense
            Dictionary<string, string> wordsAndLinks = new Dictionary<string, string>(); // sorted words and references

           

            double averageWeight = 0;

            string [] preparedPost = null;



            foreach (var current in posts) { //нашли все слова
                preparedPost = current.ToLower().Split(new char[] {'$','!','&','?','.',' ','-','[',']','+','*','/','|','_'});
                foreach (var c in preparedPost) { allWords.Add(c); }
            }



         
            foreach (var currentWord in allWords)
            {
                if (!freqOfWords.ContainsKey(currentWord))
                {
                    //allWords.Where(x => x == currentWord).Count()
                    //(double)(5 / allWords.Count
                    freqOfWords.Add(currentWord, allWords.Where(x => x == currentWord).Count() / (allWords.Count*1.0)); //word freq at the total words
                }
            }


            KeyValuePair<string,int> currentWikiToWord;

            foreach (var word in freqOfWords) {
                if (!weightOfWords.ContainsKey(word.Key)) {
                    currentWikiToWord = this.WikiGetCountOfLinksAndFirstLinkTo(word.Key);
                    if (currentWikiToWord.Value < 5) { continue; }
                    //Here we get the word surprising number of references to that more and == 5
                    relatedLinksFromWiki.Add(word.Key,currentWikiToWord.Key); //add referense to word
                    weightOfWords.Add(word.Key, currentWikiToWord.Value * word.Value); //calculate weights
                }
            }



            averageWeight = weightOfWords.Sum(freq => freq.Value) / weightOfWords.Count;
           

      

            foreach (var current in weightOfWords.Where(pair => pair.Value >= averageWeight).Select(x => x.Key).ToList()) {
                if (!wordsAndLinks.ContainsKey(current)) {
                    wordsAndLinks.Add(current, relatedLinksFromWiki.Where(x => x.Key == current).First().Value);
                }
            }

            return JObject.FromObject(wordsAndLinks);
           
        }

        public KeyValuePair<string,int> WikiGetCountOfLinksAndFirstLinkTo(string word) { //первая ссылка и колличество всех ссылок

            using (StreamReader reader = new StreamReader("F:/University/Programming/vs/Projects/GitHubLocal/LinkRepository/VKWikiAPI/VKWikiAPI/App_Data/Pretexts.txt")) {
                while (true) {
                    string str = reader.ReadLine();
                    if (str == null) { break; }

                    if (word.ToLower() == str.ToLower()) {
                         return new KeyValuePair<string, int>("", -1);
                    }
                }
            }
            
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
            if (result[3].Count()>=5)
            {
                return new KeyValuePair<string, int>(result[3][0].ToString(),result[3].Count());
            }

           return new KeyValuePair<string, int>("",-1);
        }
        

    }
}