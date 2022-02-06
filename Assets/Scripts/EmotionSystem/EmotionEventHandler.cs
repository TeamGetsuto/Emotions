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
    public event Action<string, int> onEventEnter;
    /// イベントを出る
    public event Action<string> onEventExit;
    /// イベント結果
    public event Action<string, int> onEventUnlock;

    public event Action onTurnChange;

    public event Action onLoadEnding;

    public event Action onNewTimeStart;

    public event Action onChosedEvents;

    //イベントに近づく
    public void EventTrigger(string id, int input)
    {
        if (onEventEnter != null)
            onEventEnter(id, input);
    }
    //イベントから離れる
    public void EventExitTrigger(string id)
    {
        if (onEventExit != null)
            onEventExit(id);
    }
    public void EventUnlockTrigger(string id, int input)
    {
        if (onEventUnlock != null)
            onEventUnlock(id, input);
    }

    public void OnTurnChangeTrigger()
    {
        if (onTurnChange != null)
        {
            onTurnChange();
        }
    }

    public void OnLoadEndingTrigger()
    {
        if (onLoadEnding != null)
        {
            onLoadEnding();
        }
    }

    public void OnChosedEventsTrigger()
    {
        if (onChosedEvents != null)
        {
            onChosedEvents();
        }
    }
}
