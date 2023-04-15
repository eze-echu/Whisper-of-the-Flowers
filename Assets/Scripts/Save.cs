using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


//Basado en el codigo de Bronson Zgeb https://github.com/UnityTechnologies/UniteNow20-Persistent-Data
[Serializable]
public class Save
{
    public int gems;
    public int level;
    public string user;
    public bool seenTutorial;
    public float volume;
    public bool bonusReward;
    public bool increaseGemChance;

    /*public void UpdateProgress(int newCoins, int newLevel)
    {
        guardado.coins = newCoins;
        guardado.level = newLevel;
    }*/

    /*public void UpdateUserName(string newUser)
    {
        guardado.user = newUser;
    }*/

    public string SaveData()
    {
        return SaveJSON(this);
    }
    private string SaveJSON(Save save)
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }

    public void LoadData(string newJson)
    {
        LoadFromJSON(newJson);
    }
    private void LoadFromJSON(string newJason)
    {
        JsonUtility.FromJsonOverwrite(newJason, this);
    }
}
public interface ISaveable<T>{
    void PopulateSaveData(Save a_Save);
    bool SaveFile(T save);
}
public interface ILoadable<T>
{
    bool LoadFile(T save);
    void LoadFromSaveData(Save a_Save);
}