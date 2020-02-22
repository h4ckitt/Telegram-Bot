using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;
using RestSharp.Extensions;

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
                    if (!x.Equals(-1))
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
               // Console.WriteLine(word);
                string example = "";
                //string definition = "";
                //word = word.Replace("\"", string.Empty);
                var defs = word.Split(new[] {"\",\""},StringSplitOptions.None);
                //Console.WriteLine(String.Join(",",defs));
                StringBuilder defintion = new StringBuilder(defs[0]);
                defintion.Remove(0, 21);
                //definition = definition.Substring(definition.IndexOf("definition")+13);
                Dictionary<string,string> replacer = new Dictionary<string, string>
                {
                    {@"\n","\n"},
                    {@"\r","\r"},
                    {@"\",string.Empty}
                };
                
                foreach (string replacement in replacer.Keys)
                {
                    defintion.Replace(replacement, replacer[replacement]);
                }

                return "Definition: \n\n" + Regex.Replace(defintion.ToString(), " {2,}", "")+"\n\nExample:\n"+Regex.Replace(example," {2,}","");
            }
            catch (Exception e)    
            {
                return "Something Went Wrong While Processing Your Request, The Developer Has Been Notified And Is Working On It" + e.Message;
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
