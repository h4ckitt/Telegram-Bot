using System;
using System.Collections.Generic;
using System.IO;

namespace TBot
{
    public static class UserManager
    {
     private static readonly Dictionary<long,string> UsersList = new Dictionary<long, string>();

     public static bool CheckUser(User user)
     {
         if (UsersList.ContainsKey(user.GetId()))
         {
             //Console.WriteLine("user Found");
             return true;
         }

         if (AddUser(user))
         {
             Console.WriteLine("New User {0} Added To Db Succesfully",user.GetId());
             if (SaveSession())
             {
                 Console.WriteLine("Added User {0} To Text Db Sukesfully",user.GetId());
             }
             else
             {
                 Console.WriteLine("For Some Reason, That Operation Really Failed");
             }
         }
         else
             Console.WriteLine("That Operation Failed Epicly");

         return false;
     }

     public static string GetDictionary(User user)
     {
         return UsersList[user.GetId()];
     }

     public static void SetDictionary(User user, string dictionary)
     {
         UsersList[user.GetId()] = dictionary;
     }
     public static bool AddUser(User user)
     {
         try
         {
             UsersList.Add(user.GetId(), GetDictionary(user));
         }
         catch
         {
             return false;
         }

         return true;
     }
     public static bool UpdateUser(User person)
     {
         try
         {
             UsersList[person.GetId()] = GetDictionary(person);
             if (SaveSession())
             {
                 return true;
             }

             return false;
         }
         catch
         {
             return false;
         }
     }
     
        public static bool SaveSession()
        {
            StreamWriter save = new StreamWriter("saveConfig.txt");
            try
            {
                foreach (long id in UsersList.Keys)
                {
                    save.WriteLineAsync(UsersList[id]);
                    save.WriteLineAsync(Convert.ToString(id));
                }
            }
            catch
            {
                return false;
            }
            save.Close();
            return true;
        }

        public static bool Load(string filename)
        {
            if (!File.Exists("saveConfig.txt"))
            {
                File.Create("saveConfig.txt").Dispose();
                return true;
            }
            StreamReader user = new StreamReader(filename);
            try
            {
                while (user.EndOfStream == false)
                {
                    string username = user.ReadLine();
                    string id = user.ReadLine();
                   User result = new User(Convert.ToInt64(id));
                    UsersList.Add(result.GetId(),username);
                }
            }
            catch
            {
                return false;
            }
            user.Close();
            return true;
        }
    }
}
