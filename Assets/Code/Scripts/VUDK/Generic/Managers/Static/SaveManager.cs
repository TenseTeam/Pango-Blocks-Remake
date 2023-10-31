namespace VUDK.Generic.Managers.Static
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem;

    public static class SaveManager
    {
        private const string s_DefaultExtension = ".save";

        public static void Save<T>(T data) where T : SaveDataBase
        {
            Save(data, typeof(T).Name);
        }

        public static void Save<T>(T data, string fileName, string extension = s_DefaultExtension) where T : SaveDataBase
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.persistentDataPath, fileName + extension);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        public static bool TryLoad<T>(out T data) where T : SaveDataBase
        {
            return TryLoad(out data, typeof(T).Name);
        }

        public static bool TryLoad<T>(out T data, string fileName, string extension = s_DefaultExtension) where T : SaveDataBase
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + extension);

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

        public static bool DeleteSave(string fileName, string extension = s_DefaultExtension)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + extension);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }

        public static string[] GetFilePaths(string extension = s_DefaultExtension)
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath, "*" + extension);
            return files;
        }

        public static string[] GetFileNames(string extension = s_DefaultExtension)
        {
            string[] files = GetFilePaths(extension);
            for (int i = 0; i < files.Length; i++)
                files[i] = Path.GetFileNameWithoutExtension(files[i]);

            return files;
        }
    }
}
