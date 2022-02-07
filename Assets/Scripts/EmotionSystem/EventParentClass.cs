using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// /// /// /// /// /// /// 
    private void Start()
    {
        EmotionEventHandler.current.onEventEnter += EventStart;
        EmotionEventHandler.current.onEventExit += EventExit;
        EmotionEventHandler.current.onEventUnlock += EventEnding;
        EmotionEventHandler.current.onTurnChange += DestroyHelper;


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
                EmotionEventHandler.current.OnShowButtonsTrigger(canUseHappiness, canUseSadness, canUseAnger);
            else EmotionEventHandler.current.OnCloseButtons();

            input = EmotionEventHandler.current.OnButtonPush();

            /// //////////
            if (input != -1)
            {
                EmotionEventHandler.current.OnCloseButtons();
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

    //�C�x���g���痣����
    /// /// /// /// /// /// /// 
    protected void EventExit(string id)
    {
        if (id == this.id)
        {
            Debug.Log("Out");
            EmotionEventHandler.current.OnCloseButtons();
            //UI�����
        }
    }
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //     �C�x���g�̕���     //
    /// /// /// /// /// /// /// 
    //�C�x���g�̓��e
    /// /// /// /// /// /// /// 
    protected void EventStart(string id, int input)
    {
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
                EmotionEventHandler.current.EventUnlockTrigger(id, input);
        }
    }
    /// /// /// /// /// /// /// 
    
    //�C�x���g���ʂ��Ǘ�����֐�
    protected void EventEnding(string id, int input)
    {
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
    void DestroyHelper()
    {
        EmotionEventHandler.current.onEventEnter -= EventStart;
        EmotionEventHandler.current.onEventExit -= EventExit;
        EmotionEventHandler.current.onEventUnlock -= EventEnding;
        StartCoroutine("DestroyObject");
    }


    IEnumerator DestroyObject()
    {
        isDestroing = true;
        EmotionEventHandler.current.OnCloseButtons();

        EmotionEventHandler.current.onEventEnter -= EventStart;
        yield return new WaitForSeconds(destroyTime);
        EmotionEventHandler.current.onTurnChange -= DestroyHelper;
        Destroy(gameObject);
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
