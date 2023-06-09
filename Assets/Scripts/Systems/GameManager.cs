using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void EventReceiver();
    public Camera DragCamera;

    static Dictionary<string, EventReceiver> _events = new Dictionary<string, EventReceiver>();

    public static GameManager instance;
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public static void Subscribe(string eventType, EventReceiver listener)
    {
        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, listener);
        else
            _events[eventType] += listener;
    }

    public static void Unsuscribe(string eventType, EventReceiver listener)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType] -= listener;

            if (_events[eventType] == null)
                _events.Remove(eventType);
        }
    }

    public static void Trigger(string eventType)
    {
        if (_events.ContainsKey(eventType))
            _events[eventType]();
    }

    public static void Quit()
    {
        print("Quitted");
        Application.Quit();
    }
}
