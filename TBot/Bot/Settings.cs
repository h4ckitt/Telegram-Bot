using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TBot
{
    public static class Settings
    {
        public static async void SetDictionary(User person)
        {
            string dictionary= "Not Set";
            Program.Bot.OnCallbackQuery += BotOnCallBackQuery;
            var keyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Urban Dictionary","urban"),
                    InlineKeyboardButton.WithCallbackData("Oxford Dictionary", "oxford"), 
                }
            });
            if (UserManager.GetDictionary(person) != "")
            {
                switch (UserManager.GetDictionary(person))
                {
                    case "urban":
                        dictionary = "Urban Dictionary";
                        break;
                    case "oxford":
                        dictionary = "Oxford Dictionary";
                        break;
                }
            }
            
            await Program.Bot.SendTextMessageAsync(person.GetId(), $"Your Present Dictionary Is: {dictionary}\nChoose A Dictionary", replyMarkup: keyboardMarkup);
            

            
            
            async void BotOnCallBackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
            {
                var cbq = callbackQueryEventArgs.CallbackQuery;
                SetDictionary(person,cbq.Data);
                await Program.Bot.AnswerCallbackQueryAsync(cbq.Id, "Dictionary Updated Successfully.");
            }
            }

        static void SetDictionary(User user, string dictionary)
        {
            UserManager.SetDictionary(user, dictionary);
            if(!UserManager.UpdateUser(user))
                Program.Bot.SendTextMessageAsync(user.GetId(), "Couldn't Save Your Preferences, Contact The Admin");
        }

        
    }
}
