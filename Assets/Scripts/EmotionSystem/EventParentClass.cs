using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventParentClass : MonoBehaviour
{
    //イベントプロパティ
    /// /// /// /// /// /// /// 
    [Header("Event placement")]
    [SerializeField] protected float eventRadius;
    [SerializeField] protected Vector3 eventOffset;
    [SerializeField] protected LayerMask layer;
    /// /// /// /// /// /// /// 

    //イベント管理情報
    /// /// /// /// /// /// /// 
    [Header("Event ID")]
    private float destroyTime = 5.0f;
    public string id;
    protected int input = -1;
    protected bool eventEnded = false;
    private bool isInside = false;
    private bool isDestroing = false;
    /// /// /// /// /// /// /// 
    protected bool animatiionEnded = true;
    //伝えられる感情
    [Header("Usable Emotions")]
    [SerializeField] bool canUseHappiness   = false;
    [SerializeField] bool canUseSadness     = false;
    [SerializeField] bool canUseAnger       = false;
   
    //その他
    EventTextControl textControl;
    EmoteButtonControl buttonControl;
    private void Start()
    {
        textControl = GameObject.Find("UI_System").GetComponent<EventTextControl>();
        buttonControl = GameObject.Find("Player").GetComponent<EmoteButtonControl>();
    }


    //イベント初期化

    private void OnEnable()
    {
        EventSystem.StartListening("StartEvent", EventStart);
        EventSystem.StartListening("EventEnded", EventEnding);
        EventSystem.StartListening("EventDestroying", DestroyHelper);
    }


    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //     イベント管理      //
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //イベントを始まる際  
    public static bool isStarted;
    protected void EventPlayerCheck(float radius, Vector3 eventOffset)
    {
        if (isDestroing)
            return;

        if (Physics.CheckSphere(transform.position + eventOffset, radius, layer) && !eventEnded)
        {
            EmoteButtonControl.currentEventID = id;
            //イベントのIDをボタンに伝える
            if (!isStarted)
            {
                buttonControl.ShowStart();
            }
            else
            {
                textControl.TextBoxUpdate(canUseHappiness, canUseSadness, canUseAnger);
            }

            Debug.Log("Inside");
            //仮
            /// //////////
            //if(!isDestroing)
            //    ButtonEvents.current.OnShowButtonsTrigger(canUseHappiness, canUseSadness, canUseAnger);
            //else ButtonEvents.current.OnCloseButtons();

            //input = ButtonEvents.current.OnButtonPush();

            /// //////////
            isInside = true;
        }
        else if(isInside)
        {
            buttonControl.ButtonClose();
            isInside = false;
        }
    }
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //     イベントの分岐     //
    /// /// /// /// /// /// /// 
    //イベントの内容
    /// /// /// /// /// /// /// 
    protected void EventStart(Dictionary<string, object> message)
    {
        string input = (string)message["input"];
        string id = (string)message["id"];
        Debug.Log("StartedEvent!!!! --- ");
        if (id == this.id)
        {
            switch (input)
            {
                case "h":
                    //削除するかどうか後で決める
                    ///
                    if (canUseHappiness)
                    {
                    ///
                        EventHappiness();
                    }
                    break;
                case "s":
                    if (canUseSadness)
                    {
                        EventSadness();
                    }
                    break;
                case "a":
                    if (canUseAnger)
                    {
                        EventAnger();
                    }
                    break;
                    
            }
        }
    }
    /// /// /// /// /// /// /// 
    
    //イベント結果を管理する関数
    protected void EventEnding(Dictionary<string, object> message)
    {
        string input = (string)message["input"];
        string id = (string)message["id"];

        if (id == this.id)
        {
            switch (input)
            {
                case "h":
                    //削除するかどうか後で決める
                    ///
                    if (canUseHappiness)
                    {
                        ///
                        Parser.eventListManager[Parser.GetIndexByEventID(id)][0] = true;
                    }
                    break;
                case "s":
                    if (canUseSadness)
                    {
                        Parser.eventListManager[Parser.GetIndexByEventID(id)][1] = true;
                    }
                    break;
                case "a":
                    if (canUseAnger)
                    {
                        Parser.eventListManager[Parser.GetIndexByEventID(id)][2] = true;
                    }
                    break;
            }
            eventEnded = true;
            TurnSystem.eventHasEnded = true;
            //イベントを破棄
            StartCoroutine("DestroyObject");   
        }
    }


    //イベントを破棄する前の追加時間
    /// /// /// /// /// /// /// 
    void DestroyHelper(Dictionary<string, object> message)
    {
        StartCoroutine("DestroyObject");
    }

    IEnumerator DestroyObject()
    {
        isDestroing = true;
        ButtonEvents.current.OnCloseButtons();

        yield return new WaitForSeconds(destroyTime);
        if(gameObject!=null)
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        EventSystem.StopListening("StartEvent", EventStart);
        EventSystem.StopListening("EventEnded", EventEnding);
        EventSystem.StopListening("EventDestroying", DestroyHelper);
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
