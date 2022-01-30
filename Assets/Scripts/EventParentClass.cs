using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParentClass : MonoBehaviour
{
    //イベントプロパティ
    /// /// /// /// /// /// /// 
    [Header("Event placement")]
    [SerializeField] float eventRadius;
    [SerializeField] Vector3 eventOffset;
    /// /// /// /// /// /// /// 

    //イベント管理情報
    /// /// /// /// /// /// /// 
    [Header("Event ID")]
    [SerializeField] int id;
    protected int input = -1;
    protected bool eventEnded = false;
    private bool isInside = false;
    /// /// /// /// /// /// /// 
    //伝えられる感情
    [Header("Usable Emotions")]
    [SerializeField] bool canUseHappiness   = false;
    [SerializeField] bool canUseSadness     = false;
    [SerializeField] bool canUseAnger       = false;
    

    //イベント初期化
    /// /// /// /// /// /// /// 
    private void Start()
    {
        EmotionEventHandler.current.onEventEnter += EventStart;
        EmotionEventHandler.current.onEventExit += EventExit;
        EmotionEventHandler.current.onEventUnlock += EventEnding;
    }
    /// /// /// /// /// /// /// 
    
    /// /// /// /// /// /// /// 
    //     イベント管理      //
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //イベントを始まる際
    protected void EventPlayerCheck(float radius, Vector3 eventOffset, int layerMask = 10)
    {
        //if (Physics.CheckSphere(transform.position + eventOffset, radius, layerMask)&&!eventEnded)
        if (Input.GetKey(KeyCode.Space) &&  !eventEnded)
        {
            Debug.Log("Inside");
            //仮
            /// //////////
            if (Input.GetKeyDown(KeyCode.Q))
                input = 0;
            if (Input.GetKeyDown(KeyCode.W))
                input = 1;
            if (Input.GetKeyDown(KeyCode.E))
                input = 2;
            if (Input.GetKeyDown(KeyCode.D))
                input = 3;
            /// //////////
            
            if (input != -1)
            {
                EmotionEventHandler.current.EventTrigger(id, input);
            }
                isInside = true;
        }
        else if(isInside)
        {
            EmotionEventHandler.current.EventExitTrigger(id);
            isInside = false;
        }
    }
    /// /// /// /// /// /// /// 

    //イベントから離れる際
    /// /// /// /// /// /// /// 
    protected void EventExit(int id)
    {
        if (id == this.id)
        {
            Debug.Log("Out");
            //UIを閉じる
        }
    }
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //     イベントの分岐     //
    /// /// /// /// /// /// /// 
    //イベントの内容
    /// /// /// /// /// /// /// 
    protected void EventStart(int id, int input)
    {
        if (id == this.id)
        {
            switch (input)
            {
                case 0:
                    //削除するかどうか後で決める
                    ///
                    if (canUseHappiness)
                    {
                    ///
                        EventHappiness();
                        eventEnded = true;
                    }
                    break;
                case 1:
                    if (canUseSadness)
                    {
                        EventSadness();
                        eventEnded = true;
                    }
                    break;
                case 2:
                    if (canUseAnger)
                    {
                        EventAnger();
                        eventEnded = true;
                    }
                    break;
                    
            }
            //イベントの結果
            EmotionEventHandler.current.EventUnlockTrigger(id, input);
        }
    }
    /// /// /// /// /// /// /// 
    
    //イベント結果を管理する関数
    protected void EventEnding(int id, int input)
    {
        if (id == this.id)
        {
            switch (input)
            {
                case 0:
                    //削除するかどうか後で決める
                    ///
                    if (canUseHappiness)
                    {
                    ///
                        EmotionEventHandler.eventListManager[id][0] = true;
                    }
                    break;
                case 1:
                    if (canUseSadness)
                    {
                        EmotionEventHandler.eventListManager[id][1] = true;
                    }
                    break;
                case 2:
                    if (canUseAnger)
                    {
                        EmotionEventHandler.eventListManager[id][2] = true;
                    }
                    break;
            }
        }
    }
    /// /// /// /// /// /// /// 


    /// /// /// /// /// /// /// 
    //     バーチャル関数      //
    /// /// /// /// /// /// /// 

    //  選択によっての処理
    /// /// /// /// /// /// /// /// /// 
    virtual protected void EventHappiness() { }
    virtual protected void EventSadness() { }
    virtual protected void EventAnger() { }
    /// /// /// /// /// /// /// /// /// 
}
