using System;
using System.Linq;
using System.Text.RegularExpressions;
using RestSharp;


namespace TBot
{
    
    public class Oxford
    {
        private const string ApiKey = "1b909c0dd1c28d3f34f924a19ac306a5";
        private const string ApiId = "a6567021";
        public static string Define(string word)
        {
            var client =
                new RestClient(
                    "https://od-api.oxforddictionaries.com/api/v2/entries/en-us/"+word+"?fields=definitions&strictMatch=false");
            var request = new RestRequest(Method.GET);
            request.AddHeader("app_id", ApiId);
            request.AddHeader("app_key", ApiKey);
            IRestResponse response = client.Execute(request);
            
            return Format(response.Content);
        }

        private static string Format(string json)
        {
            if (json.IndexOf("definitions", StringComparison.Ordinal).Equals(-1))
            {
                return "No Definition Found";
            }

            string[] delims = {"[","]","{","}"};
            foreach (string s  in delims)
            {
                while (true)
                {
                    int index = json.IndexOf(s, StringComparison.Ordinal);
                    if (!index.Equals(-1))
                    {
                        string before = json.Substring(0, index);
                        string after = json.Substring(index + 1);
                        json = before + after;
                    }
                    else break;
                }
            }

            string definition = "";
            json = json.Replace("\"", string.Empty);
            var lines = json.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            for (int i = 0; i <lines.Count; i++)
            {
                if (lines[i].Contains("definitions")) 
                {
                    definition=lines[i+1];
                    break;
                }
            }
            /*int loc = json.IndexOf("definitions");
            Console.WriteLine("Part Processed: ");
            Console.WriteLine(json);
            json = json.Remove(0, loc);
            json = json.Substring(loc, json.IndexOf("id"));
            json=Regex.Replace(json," {2,}"," ");
            Console.WriteLine("Part Part Processed: ");*/
            //json = json.Replace("definitions", "definition");
            //Thread.Sleep(50000);
            definition=definition.Replace(@"\n","\n").Replace(@"\r","\r");
            return "Definition: "+Regex.Replace(definition," {2,}","");
        }
        
    }
}
