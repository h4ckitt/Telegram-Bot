using System;
using System.IO;
namespace TBot
{
    public class DefinitionHandler
    {
        public static string Definition(string word, string dictionary)
        {
            switch (dictionary)
            {
                case "urban":
                    return Urban.Search(word);
                case "oxford":
                    return Oxford.Define(word);
                case "check":
                    break;
            }

            return "Dictionary Not Set Yet, Use /set To Choose A Dictionary";
        }
    }
}
