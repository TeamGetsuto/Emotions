using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParentClass : MonoBehaviour
{
    //イベントプロパティ
    /// /// /// /// /// /// /// 
    [Header("Event placement")]
    public float eventRadius;
    public Vector3 eventOffset;
    /// /// /// /// /// /// /// 

    //イベント管理情報
    /// /// /// /// /// /// /// 
    [Header("Event ID")]
    public int id;
    protected int input = -1;
    protected bool eventEnded = false;
    private bool isInside = false;
    /// /// /// /// /// /// /// 

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
            if (Input.GetKey(KeyCode.Q))
                input = 0;
            if (Input.GetKey(KeyCode.W))
                input = 1;
            if (Input.GetKey(KeyCode.E))
                input = 2;
            if (Input.GetKey(KeyCode.D))
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
                    EventA();
                    eventEnded = true;
                    
                    break;
                case 1:
                    EventB();
                    eventEnded = true;
                    break;
                case 2:
                    EventC();
                    eventEnded = true;
                    break;
                case 3:
                    EventD();
                    eventEnded = true;
                    break;
            }
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
                    ResultA();
                    break;
                case 1:
                    ResultB();
                    break;
                case 2:
                    ResultC();
                    break;
                case 3:
                    ResultD();
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
    virtual protected void EventA() { }
    virtual protected void EventB() { }
    virtual protected void EventC() { }
    virtual protected void EventD() { }
    /// /// /// /// /// /// /// /// /// 

    //  選択によっての結果
    /// /// /// /// /// /// /// /// /// 
    virtual protected void ResultA() { }
    virtual protected void ResultB() { }
    virtual protected void ResultC() { }
    virtual protected void ResultD() { }
    /// /// /// /// /// /// /// /// /// 
}
