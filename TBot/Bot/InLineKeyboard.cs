using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TBot
{
    public class Keyboard
    {
        public static InlineKeyboardButton[][] GetInlineKeyboard(string[,] stringArray)
        {
            var keyboardInline = new[]
            {
                new[]{new  InlineKeyboardButton(){ Text="Urban Dictionary",CallbackData = "urban"}},
                new[]{new InlineKeyboardButton(){Text="Oxford Dictinoary",CallbackData = "oxford"}}
            };
            /*var keyboardButtons = new InlineKeyboardButton[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                    keyboardButtons[i] = new InlineKeyboardButton
                    {
                        Text = stringArray[i,0],
                        CallbackData = stringArray[i,1]
                    };
            }
            keyboardInline[0] = keyboardButtons;*/
            return keyboardInline;
        }
    }
}
