namespace VUDK.DataSave.SaveSystem
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    public static class SaveManager
    {
        private const string s_Extension = ".save";
        private const string s_Folder = "/Saves/";

        public static void Save<T>(T data) where T : DataSave
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.persistentDataPath, s_Folder + typeof(T).Name + s_Extension);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        public static T Load<T>() where T : DataSave
        {
            string path = Path.Combine(Application.persistentDataPath, s_Folder + typeof(T).Name + s_Extension);

            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using(FileStream stream = new FileStream(path, FileMode.Open))
                {
                    return (T)formatter.Deserialize(stream);
                }
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}
