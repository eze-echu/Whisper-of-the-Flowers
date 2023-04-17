using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;


//Hecho por Bronson Zgeb https://github.com/UnityTechnologies/UniteNow20-Persistent-Data
public static class FileManager
{
    private static string _gameName = "Plants_V_Zombies_Clone";
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath + @$"\{_gameName}\Save", a_FileName);

        try
        {
            if (!File.Exists(Application.persistentDataPath + @$"\{_gameName}\Save"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + @$"\{_gameName}\Save");
            }
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath + @$"\{_gameName}\Save", a_FileName);
        if (File.Exists(fullPath))
        {
            try
            {
                result = File.ReadAllText(fullPath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {fullPath} with exception {e}");
                result = "";
                return false;
            }
        }
        else
        {
            result = "";
            return false;
        }
    }
    public static bool DeleteFile(string a_FileName)
    {
        var fullPath = Path.Combine(Application.persistentDataPath + @$"\{_gameName}\Save", a_FileName);
        try
        {
            File.Delete(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"No Save File found in {fullPath}");
            return false;
        }
    }
}
