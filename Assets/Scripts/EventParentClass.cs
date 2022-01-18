using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParentClass : MonoBehaviour
{
    //�C�x���g�v���p�e�B
    /// /// /// /// /// /// /// 
    [Header("Event placement")]
    public float eventRadius;
    public Vector3 eventOffset;
    /// /// /// /// /// /// /// 

    //�C�x���g�Ǘ����
    /// /// /// /// /// /// /// 
    [Header("Event ID")]
    public int id;
    protected int input = -1;
    protected bool eventEnded = false;
    private bool isInside = false;
    /// /// /// /// /// /// /// 

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
    
    //�C�x���g���ʂ��Ǘ�����֐�
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
    //     �o�[�`�����֐�      //
    /// /// /// /// /// /// /// 

    //  �I���ɂ���Ă̏���
    /// /// /// /// /// /// /// /// /// 
    virtual protected void EventA() { }
    virtual protected void EventB() { }
    virtual protected void EventC() { }
    virtual protected void EventD() { }
    /// /// /// /// /// /// /// /// /// 

    //  �I���ɂ���Ă̌���
    /// /// /// /// /// /// /// /// /// 
    virtual protected void ResultA() { }
    virtual protected void ResultB() { }
    virtual protected void ResultC() { }
    virtual protected void ResultD() { }
    /// /// /// /// /// /// /// /// /// 
}
