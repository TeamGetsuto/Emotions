using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParentClass : MonoBehaviour
{
    //�C�x���g�v���p�e�B
    /// /// /// /// /// /// /// 
    [Header("Event placement")]
    [SerializeField] float eventRadius;
    [SerializeField] Vector3 eventOffset;
    /// /// /// /// /// /// /// 

    //�C�x���g�Ǘ����
    /// /// /// /// /// /// /// 
    [Header("Event ID")]
    [SerializeField] int id;
    protected int input = -1;
    protected bool eventEnded = false;
    private bool isInside = false;
    /// /// /// /// /// /// /// 
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
    }
    /// /// /// /// /// /// /// 
    
    /// /// /// /// /// /// /// 
    //     �C�x���g�Ǘ�      //
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //�C�x���g���n�܂��
    protected void EventPlayerCheck(float radius, Vector3 eventOffset, int layerMask = 10)
    {
        //if (Physics.CheckSphere(transform.position + eventOffset, radius, layerMask)&&!eventEnded)
        if (Input.GetKey(KeyCode.Space) &&  !eventEnded)
        {
            Debug.Log("Inside");
            //��
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

    //�C�x���g���痣����
    /// /// /// /// /// /// /// 
    protected void EventExit(int id)
    {
        if (id == this.id)
        {
            Debug.Log("Out");
            //UI�����
        }
    }
    /// /// /// /// /// /// /// 

    /// /// /// /// /// /// /// 
    //     �C�x���g�̕���     //
    /// /// /// /// /// /// /// 
    //�C�x���g�̓��e
    /// /// /// /// /// /// /// 
    protected void EventStart(int id, int input)
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
            EmotionEventHandler.current.EventUnlockTrigger(id, input);
        }
    }
    /// /// /// /// /// /// /// 
    
    //�C�x���g���ʂ��Ǘ�����֐�
    protected void EventEnding(int id, int input)
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
    //     �o�[�`�����֐�      //
    /// /// /// /// /// /// /// 

    //  �I���ɂ���Ă̏���
    /// /// /// /// /// /// /// /// /// 
    virtual protected void EventHappiness() { }
    virtual protected void EventSadness() { }
    virtual protected void EventAnger() { }
    /// /// /// /// /// /// /// /// /// 
}
