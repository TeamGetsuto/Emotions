using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventParentClass : MonoBehaviour
{
    //�C�x���g�v���p�e�B
    /// /// /// /// /// /// /// 
    [Header("Event placement")]
    [SerializeField] protected float eventRadius;
    [SerializeField] protected Vector3 eventOffset;
    [SerializeField] protected LayerMask layer;
    /// /// /// /// /// /// /// 

    //�C�x���g�Ǘ����
    /// /// /// /// /// /// /// 
    [Header("Event ID")]
    private float destroyTime = 5.0f;
    public string id;
    protected int input = -1;
    protected bool eventEnded = false;
    private bool isInside = false;
    private bool isDestroing = false;
    /// /// /// /// /// /// /// 
    protected bool animatiionEnded = false;
    //�`�����銴��
    [Header("Usable Emotions")]
    [SerializeField] bool canUseHappiness   = false;
    [SerializeField] bool canUseSadness     = false;
    [SerializeField] bool canUseAnger       = false;


    //�C�x���g������

    private void OnEnable()
    {
        EventSystem.StartListening("StartEvent", EventStart);
        EventSystem.StartListening("ExitEvent", EventExit);
        EventSystem.StartListening("EventEnded", EventEnding);
        EventSystem.StartListening("EventDestroying", DestroyHelper);
    }

 
    /// /// /// /// /// /// /// 
    
    /// /// /// /// /// /// /// 
    //     �C�x���g�Ǘ�      //
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //�C�x���g���n�܂��  
    protected void EventPlayerCheck(float radius, Vector3 eventOffset)
    {
        if (Physics.CheckSphere(transform.position + eventOffset, radius, layer) && !eventEnded)
        { 
            Debug.Log("Inside");
            //��
            /// //////////
            if(!isDestroing)
                ButtonEvents.current.OnShowButtonsTrigger(canUseHappiness, canUseSadness, canUseAnger);
            else ButtonEvents.current.OnCloseButtons();

            input = ButtonEvents.current.OnButtonPush();

            /// //////////
            if (input != -1)
            {
                ButtonEvents.current.OnCloseButtons();
                EventSystem.TriggerEvent("StartEvent", new Dictionary<string, object> { {"id", id},{"input", input} });
            }
                isInside = true;
        }

        else if(isInside)
        {
            EventSystem.TriggerEvent("ExitEvent", new Dictionary<string, object> { { "id", id} });
            isInside = false;
        }
    }
    /// /// /// /// /// /// /// 

    //�C�x���g���痣����
    /// /// /// /// /// /// /// 
    protected void EventExit(Dictionary<string, object> message)
    {
        string id = (string)message["id"];
        if (id == this.id)
        {
            Debug.Log("Out");
            ButtonEvents.current.OnCloseButtons();
            //UI�����
        }
    }
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //     �C�x���g�̕���     //
    /// /// /// /// /// /// /// 
    //�C�x���g�̓��e
    /// /// /// /// /// /// /// 
    protected void EventStart(Dictionary<string, object> message)
    {
        int input = (int)message["input"];
        string id = (string)message["id"];

        if (id == this.id)
        {
            switch (input)
            {
                case 0:
                    //�폜���邩�ǂ�����Ō��߂�
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
            //�C�x���g�̌���
            if(animatiionEnded)
                EventSystem.TriggerEvent("EventEnded", new Dictionary<string, object> { { "id", id }, {"input", input } });
        }
    }
    /// /// /// /// /// /// /// 
    
    //�C�x���g���ʂ��Ǘ�����֐�
    protected void EventEnding(Dictionary<string, object> message)
    {
        int input = (int)message["input"];
        string id = (string)message["id"];

        if (id == this.id)
        {
            switch (input)
            {
                case 0:
                    //�폜���邩�ǂ�����Ō��߂�
                    ///
                    if (canUseHappiness)
                    {
                        ///
                        Parser.eventListManager[Parser.GetIndexByEventID(id)][0] = true;
                    }
                    break;
                case 1:
                    if (canUseSadness)
                    {
                        Parser.eventListManager[Parser.GetIndexByEventID(id)][1] = true;
                    }
                    break;
                case 2:
                    if (canUseAnger)
                    {
                        Parser.eventListManager[Parser.GetIndexByEventID(id)][2] = true;

                    }
                    break;
            }
            TurnSystem.eventHasEnded = true;
            //�C�x���g��j��
            StartCoroutine("DestroyObject");   
        }
    }


    //�C�x���g��j������O�̒ǉ�����
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
        EventSystem.StopListening("ExitEvent", EventExit);
        EventSystem.StopListening("EventEnded", EventEnding);
        EventSystem.StopListening("EventDestroying", DestroyHelper);
    }

    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //     �o�[�`�����֐�      //
    /// /// /// /// /// /// /// 

    //  �I���ɂ���Ă̏���
    /// /// /// /// /// /// /// /// /// 
    virtual protected void EventHappiness() { }
    virtual protected void EventSadness() { }
    virtual protected void EventAnger() { }
    /// /// /// /// /// /// /// /// /// 
}
