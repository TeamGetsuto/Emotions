using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{

    #region Singleton

    private static EventSystem eventSystem;

    private void Awake()
    {
        if (eventSystem != null && eventSystem != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            eventSystem = this;
            eventSystem.Initialize();
        }
    }
    #endregion

    private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

    void Initialize()
    {
        Debug.LogWarning("Initialize");
        if (eventSystem.eventDictionary == null)
            eventSystem.eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
    }

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Debug.LogWarning("Started Listening");
        Action<Dictionary<string, object>> thisEvent;

        if(eventSystem.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventSystem.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventSystem.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        if (eventSystem == null)
            return;
        Action<Dictionary<string, object>> thisEvent;
        if(eventSystem.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventSystem.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, Dictionary<string, object> message)
    {
        Action<Dictionary<string, object>> thisEvent = null;
        if(eventSystem.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(message);
        }
    }


}
