/// <summary>
/// 单例
/// </summary>
/// <typeparam name="T"></typeparam>
namespace Assets.Framework
{
    public class Singleton<T> where T : new()
    {
        private static T instance;
        public static T getInstance()
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}