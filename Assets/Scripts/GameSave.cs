using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

//Basado en codigo mostrado en https://www.youtube.com/watch?v=uD7y4T4PVk0
[Serializable]
public class GameSave : MonoBehaviour, ISaveable<GameSave>, ILoadable<GameSave>
{
    //statics are save values, nonstatics are cached values, to update save values you need to SaveAllValues() or modify a static value and then SaveRemotely/RemoteSave
    public static int _Level;
    public static string _UserName;
    public int Level;
    public  string UserName;
    public static string _fileName = "SaveDAT";
    public string fileName = "SaveDAT";
    private static string _fullPath;
    [Range (0f, 1f)]
    public static float _volume;
    [Range(0f, 1f)]
    public float volume;
    public static GameSave _gameSave;

    private void Awake()
    {
        if (!_gameSave)
        {
            _gameSave = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _gameSave = this;
        GameManager.Subscribe("UpdateWithSaveValues", UpdateWithSaveValues);
        GameManager.Subscribe("UpdateEditorValues", UpdateEditorValues);
        GameManager.Subscribe("SaveRemotely", RemoteSave);
        GameManager.Subscribe("DeleteSave", DeleteSave);
        FileName();
        _fullPath = Path.Combine(Application.dataPath + @"\Save", _fileName + ".dat");
        //print(_fullPath);
        if (!LoadFile(this))
        {
            UpdateWithSaveValues();
            AudioManager.SwitchNoise(GameSave._volume);
        }
        else
        {
            _Level = 0;
            _UserName = "";
            _volume = 1f;
            FileName();
            SaveFile(this);
        }
    }
    public void LoadFromSaveData(Save a_Save)
    {
        /*print(Gems);
        print(a_Save.gems);*/
        _Level = a_Save.level;
        _UserName = a_Save.user;
        _volume = a_Save.volume;
        UpdateEditorValues();
    }

    public void PopulateSaveData(Save a_Save)
    {
        a_Save.volume = _volume;
        a_Save.level = _Level;
        a_Save.user = _UserName;
    }

    public bool SaveFile(GameSave testing)
    {
        Save sf = new Save();
        testing.PopulateSaveData(sf);
        bool b = FileManager.WriteToFile(_fileName + ".dat", sf.SaveData());
        
        if(b)
        {
            print("Save Successful" + sf.user + sf.level + sf.volume);
        }
        return (b);
    }

    public bool LoadFile(GameSave testing)
    {
        bool b = FileManager.LoadFromFile(_fileName + ".dat", out var json);
        if (b)
        {
            Save sf = new Save();
            sf.LoadData(json);

            testing.LoadFromSaveData(sf);
            print("Load Successful: " + GameSave._UserName + GameSave._Level + GameSave._volume);
        }
        return b;
    }
    public void DeleteSave()
    {
        FileManager.DeleteFile(_fileName + ".dat");
        _Level = 0;
        _UserName = string.Empty;
        _volume = 1;
        SaveFile(this);

    }
    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SaveAllData(this);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            FileName();
            LoadFile(this);
        } 
        #endif
    }
    public static void SaveAllData(GameSave gameSave)
    {
        gameSave.FileName();
        gameSave.UpdateWithSaveValues();
        gameSave.SaveFile(gameSave);
    }
    private void OnValidate()
    {
        //GameManager.Trigger("UpdateWithSaveValues");
    }

    private void UpdateWithSaveValues()
    {
        if (Level < _Level)
        {
            _Level = Level;
        }
        if(UserName != _UserName)
        {
            _UserName = UserName;
        }
        if (volume != _volume)
        {
            _volume = volume;
        }
        FileName();
    }
    private void UpdateEditorValues()
    {
        Level = _Level;
        UserName = _UserName;
        volume = _volume;
    }
    private void FileName()
    {
        _fileName = fileName;
        if (_fileName == string.Empty)
        {
            _fileName = "SaveDAT";
        }
    }
    private void RemoteSave()
    {
        if (GetComponent<GameSave>())
        {
            GameSave._gameSave.FileName();
            _gameSave.SaveFile(_gameSave);
            UpdateEditorValues();
        }
    }
}
