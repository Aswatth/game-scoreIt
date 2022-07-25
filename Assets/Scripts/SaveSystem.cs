using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath;

    public static void Save(string data, string fileName)
    {
        Debug.Log(SAVE_FOLDER);
        File.WriteAllText(SAVE_FOLDER + "/" + fileName + ".json", data);
    }
    public static string Load(string fileName)
    {
        Debug.Log(SAVE_FOLDER);
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
            return File.ReadAllText(SAVE_FOLDER + "/" + fileName + ".json");
        else
            return null;
    }
}
