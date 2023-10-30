namespace VUDK.SaveSystem
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    public static class SaveManager
    {
        private const string s_Extension = ".save";

        public static void Save<T>(T data) where T : SaveDataBase
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.persistentDataPath, typeof(T).Name + s_Extension);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        public static bool TryLoad<T>(out T data) where T : SaveDataBase
        {
            string path = Path.Combine(Application.persistentDataPath, typeof(T).Name + s_Extension);

            if (!File.Exists(path))
            {
                data = null;
                return false;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                data = (T)formatter.Deserialize(stream);
                return true;
            }
        }
    }
}
