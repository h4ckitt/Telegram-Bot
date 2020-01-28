using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace TBot
{
    public static class SpellCheck
    {
        public static string Check(string word)
        {
            var client = new RestClient("https://montanaflynn-spellcheck.p.rapidapi.com/check/?text=" + word);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "montanaflynn-spellcheck.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "e78423ae51mshcd39ccc6a7227adp1a53a5jsn73d7ef3a3f86");
            IRestResponse response = client.Execute(request);
            var meaning = Meaning(response.Content);
            return meaning;
        }

        static string Meaning(string something)
        {
            string[] delims = {"[", "]", "}", "{"};
            foreach (string s in delims)
            {
                while (true)
                {
                    int x = something.IndexOf(s);
                    if (x.Equals(-1) == false)
                    {
                        string before = something.Substring(0, x);
                        string after = something.Substring(x + 1);
                        something = before + after;
                    }
                    else break;
                }
            }
            
            string correction = "";
            something = something.Replace("\"", string.Empty);
            var suggested = something.Split(':', ',');
            for (int i = 0; i < suggested.Length; i++)
            {
                if (suggested[i].Contains("suggestion"))
                {
                    correction = suggested[i + 1];
                    break;
                }
            }

            return "Suggested Correction:\n"+ correction;
        }

            static List<string> CheckDiff(string original, string suggested)
        {
            List<string> diff = new List<string>();
            string[] orig = original.Split(' ');
            string[] sugg = suggested.Split(' ');

            foreach (string s in orig)
            {
                if (!sugg.Contains(s))
                {
                    diff.Add(s);
                }
            }

            return diff;
        }
    }
}
