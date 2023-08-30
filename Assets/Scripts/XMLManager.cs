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

    public ItemDataBase itemDB;

    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDataBase));
        //FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Create);
        //serializer.Serialize(stream, itemDB);
        //Stream.Close();
    }

    public void LoadItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDataBase));
        //FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Open);
        //itemDB = serializer.Deserialize(stream) as ItemDatabase;
        //Stream.Close();
    }
}

[System.Serializable]
public class ItemEntry
{
    public string name;
    public Material material;
    public int value;
}

[System.Serializable]
public class ItemDataBase
{
    [XmlArray("LevelInfo")]
    public List<ItemEntry> List = new List<ItemEntry>();
}