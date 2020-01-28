using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Threading;
using System.Threading.Tasks;

namespace TBot
{
    internal class Program
    {
        public static ITelegramBotClient Bot;

        public static void Main(string[] args)
        {
           Bot = new TelegramBotClient("911112005:AAFSM5o7Cb1dmvETAqwwk496tp8RPSxpdjQ");
            var me = Bot.GetMeAsync().Result;
            Console.WriteLine($"Hello World! I Am User {me.Id} and my name is {me.FirstName}");
            if (UserManager.Load("saveConfig.txt"))
            {
                Console.WriteLine("Non-Existing DB Loaded Successfully");
            }
            Bot.OnMessage += Bot_OnMessage;
            Bot.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            User tempUser = new User(e.Message.Chat.Id);
            if (!UserManager.CheckUser(tempUser))
            {
                UserManager.AddUser(tempUser);
            }
            
            Task<string> task = new Task<string>(()=>Commands.Handle(e));
            task.Start();
            string result = await task;
            if (e.Message.Text != null && result != "")
            {
                //Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}");
                await Bot.SendTextMessageAsync(e.Message.Chat, text: result);
                /*Message message = await bot.SendTextMessageAsync(e.Message.Chat, "Trying *all* `parameters` ~of~ _Markdown_",ParseMode.Markdown,disableNotification:true, replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("Check SendMessage Method",    "https://core.telegram.org/bots/api#sendmessage"
                //)));
                Console.WriteLine($"{message.From.FirstName} sent Message {message.MessageId} to chat {message.Chat.Id} at {message.Date}");
                //Console.WriteLine($"It Is A Reply To message {message.ReplyToMessage.MessageId} and has {message.Entities.Length} message entities");
                //Message msg1 = await bot.SendStickerAsync(e.Message.Chat,
                    sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"
                );
                Message msg2 = await bot.SendStickerAsync(e.Message.Chat,sticker:msg1.Sticker.FileId);*/
            }
        }
    }
}
