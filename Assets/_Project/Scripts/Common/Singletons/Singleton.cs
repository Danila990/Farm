namespace MyCode
{
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance => CreateClass();

        public static T InitInstance()
        {
            return CreateClass();
        }

        private static T CreateClass()
        {
            if (_instance == null)
                _instance = new T();

            return _instance;
        }
    }
}
