using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Serialization;


[Serializable]
public class Save
{
    private static string _fullPath = Path.Combine(Application.persistentDataPath, "save.json");

    /// <summary>
    /// Loads the data from the file and returns it as a dictionary.
    /// </summary>
    /// <returns name="saveData">sadsad</returns>
    public static Dictionary<string, object> LoadDirectly()
    {
        Debug.Log(_fullPath);
        if (!File.Exists(_fullPath))
        {
            Debug.LogError("File not found");
            return new Dictionary<string, object>();
        }
        return JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(_fullPath));
    }
    
    /// <summary>
    /// Saves the data present in the dictionary to a file.
    /// </summary>
    /// <param name="newData"></param>
    
    public static void SaveData(Dictionary<string, object> newData)
    {
        var data = LoadDirectly();
        if (newData == null)
        {
            Debug.LogError("Data is null");
            return;
        }

        foreach (var kvp in newData)
        {
            data[kvp.Key] = kvp.Value;
        }
        File.WriteAllText(_fullPath, JsonConvert.SerializeObject(data));
    }
    public static void DeleteData()
    {
        if (File.Exists(_fullPath))
        {
            File.Delete(_fullPath);
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
}