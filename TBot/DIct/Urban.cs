using System;
using System.Text.RegularExpressions;
using RestSharp;

namespace TBot
{
    public static class Urban
    {
       private static string Format(string word)
        {
            if (word.IndexOf("definition").Equals(-1))
                return "No Definition Found";
            string[] delims = {"[", "]", "}", "{"};
            foreach (string s in delims)
            {
                while (true)
                {
                    int x = word.IndexOf(s);
                    if (x.Equals(-1) == false)
                    {    
                        string before = word.Substring(0, x);
                        string after = word.Substring(x + 1);
                        word = before + after;
                    }
                    else break;
                }
            }

            try
            {
                string definition = "";
                word = word.Replace("\"", string.Empty);
                var defs = word.Split(':');
                for (int i = 0; i < defs.Length; i++)
                {
                    if (defs[i].Contains("definition"))
                    {
                        for (int j = i+1; j < defs.Length-i; j++)
                        {
                            if (!defs[j].Equals("permalink"))
                            {
                                definition += defs[j];
                                continue;
                            }
                            break;
                        }
                        break;
                    }
                }
                definition = definition.Substring(0, definition.IndexOf(",permalink", StringComparison.Ordinal));
                string[] escapeChars = {@"\n", @"\r", @"\"};
                foreach (string s in escapeChars)
                {
                    definition = definition.Replace(s, String.Empty);
                }

                return "Definition: " + Regex.Replace(definition, " {2,}", "");
            }
            catch
            {
                return "Something Went Wrong While Processing Your Request, The Developer Has Been Notified And Is Working On It";
            }
        }

           public static string Search(string term)
            {
                const string api = "e78423ae51mshcd39ccc6a7227adp1a53a5jsn73d7ef3a3f86";
                const string host = "mashape-community-urban-dictionary.p.rapidapi.com";
                var client =
                    new RestClient("https://mashape-community-urban-dictionary.p.rapidapi.com/define?term=" + term);
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", host);
                request.AddHeader("x-rapidapi-key", api);
                IRestResponse response = client.Execute(request);
                return Format(response.Content);
            }
        }
    }
