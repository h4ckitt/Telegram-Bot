namespace TBot
{
    public class User
    {
        private readonly long _userId;
        //private string _dictionaryType="";

        public User(long id)
        {
            _userId = id;
        }

        public long GetId()
        {
            return _userId;
        }
            
    }
}
