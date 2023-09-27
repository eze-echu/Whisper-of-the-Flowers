using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystems : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public static EventSystems instance;
    
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = SceneLoader.Instance();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            print(GameSave._volume);
        }*/
    }

    public void LoadScene(string name)
    {
        sceneLoader.AsyncLoadScene(name);
        //sceneLoader.AsyncLoadScene(name);
    }
    public void DeleteSave()
    {/*
        GameManager.Trigger("DeleteSave");
        GameSave._seenTutorial = false;
        GameSave.gems = 0;
        GameManager.Trigger("SaveRemotely");*/
    }
}
