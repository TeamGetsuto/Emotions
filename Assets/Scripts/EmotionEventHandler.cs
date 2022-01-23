using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EmotionEventHandler : MonoBehaviour
{
    /// //////////
    //シングルトン

    #region Singleton
    public static EmotionEventHandler current;
    private void Awake()
    {
        if (current != null && current != this)
            Destroy(this);
        else current = this;
    }
    #endregion

    /// //////////
    /// //////////
    
    /// //////////
    //エベントシステムを作ります
    /// //////////
    /// イベントに入る
    public event Action<int, int> onEventEnter;
    /// イベントを出る
    public event Action<int> onEventExit;

    /// イベント結果
    public event Action<int, int> onEventUnlock;

    //イベントに近づく
    public void EventTrigger(int id, int input)
    {
        if (onEventEnter != null)
            onEventEnter(id, input);
    }
    //イベントから離れる
    public void EventExitTrigger(int id)
    {
        if (onEventExit != null)
            onEventExit(id);
    }
    public void EventUnlockTrigger(int id, int input)
    {
        if (onEventUnlock != null)
            onEventUnlock(id, input);
    }

    //イベントの　id と　結果
    public static bool[][] eventListManager;
    public static bool[] eventIsOn;
    [SerializeField] int eventAmount = 10;

    private void Start()
    {
        EventListInitialize();
    }

    void EventListInitialize()
    {
        eventIsOn = new bool[eventAmount];
        eventListManager = new bool[eventAmount][];
        for (int i = 0; i < eventAmount; i++)
        {
            eventListManager[i] = new bool[4] { false, false, false, false };
            eventIsOn[i] = false;
        }
    }



}
