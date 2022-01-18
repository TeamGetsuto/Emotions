using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EmotionEventHandler : MonoBehaviour
{
    /// //////////
    //�V���O���g��

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
    //�G�x���g�V�X�e�������܂�
    /// //////////
    /// �C�x���g�ɓ���
    public event Action<int, int> onEventEnter;
    /// �C�x���g���o��
    public event Action<int> onEventExit;

    /// �C�x���g����
    public event Action<int, int> onEventUnlock;

    //�C�x���g�ɋ߂Â�
    public void EventTrigger(int id, int input)
    {
        if (onEventEnter != null)
            onEventEnter(id, input);
    }
    //�C�x���g���痣���
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

    //�C�x���g�́@id �Ɓ@����
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
