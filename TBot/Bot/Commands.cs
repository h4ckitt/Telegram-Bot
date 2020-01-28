using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;

namespace TBot
{
    public static class Commands
    {
        public static string Handle(MessageEventArgs e)
        {
            string result = "";
            List <string> cmds = new List<string>{"/define","/check","/translate","/set"};
            User tempUser = new User(e.Message.Chat.Id);
            string message = e.Message.Text;
            foreach (var command in cmds)
            {
                if (message.StartsWith(command))
                {
                    string swtch = message.Split(' ').First();
                    string word = message.Substring(message.IndexOf(swtch)+swtch.Length);
                    word=word.Trim();
                    if (word.Length<=0 && swtch!="/set")
                    {
                         result= "Enter A Word Or Check Help For "+ swtch +" Usage";
                    }
                    else
                    {
                        switch (swtch)
                        {
                            case "/define":
                                result = DefinitionHandler.Definition(word, UserManager.GetDictionary(tempUser));
                                break;
                            case "/check":
                                result = SpellCheck.Check(word);
                                break;
                            case "/set":
                                Settings.SetDictionary(tempUser);
                                break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
