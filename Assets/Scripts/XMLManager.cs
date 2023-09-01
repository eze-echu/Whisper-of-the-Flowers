using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    private void Awake()
    {
        instance = this;
    }

    public SaveData SD;

    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/save_data.xml", FileMode.Create);
        serializer.Serialize(stream, SD);
        stream.Close();
    }

    public void LoadItems()
    {
        if (File.Exists(Application.dataPath + "/StreamingFiles/XML/save_data.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/save_data.xml", FileMode.Open);
            SD = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
        else
        {
            SD = new SaveData();
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int levelComplete;
    public Dictionary<string, bool> FlowerUnloked;
    public List<LevelStat> estadisticasNivel;
}

[System.Serializable]
public class LevelStat
{
    [XmlArray("LevelInfo")]
    public string nombreNivel;
    public int resultado;
}