using System.IO;
using UnityEngine;

public static class SaveSystem  
{
    // Simple save system to store the high score
    public static readonly string SAVE_DATA = Application.persistentDataPath + "/saves/";
    public static readonly string FILE_EXT = ".json";

    public static void Save(string fileName, string data)
    {
        if (!Directory.Exists(SAVE_DATA))
        {
            Directory.CreateDirectory(SAVE_DATA);
        }

        File.WriteAllText(SAVE_DATA + fileName + FILE_EXT, data);
    }

    public static string Load(string fileName)
    {
        if (File.Exists(SAVE_DATA + fileName + FILE_EXT))
        {
            string loadedData = File.ReadAllText(SAVE_DATA+ fileName + FILE_EXT);

            return loadedData;
        }
        return null;
    }

}
