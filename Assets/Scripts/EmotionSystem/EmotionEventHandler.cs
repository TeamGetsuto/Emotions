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
    public event Action<string, int> onEventEnter;
    /// �C�x���g���o��
    public event Action<string> onEventExit;
    /// �C�x���g����
    public event Action<string, int> onEventUnlock;

    //�C�x���g�ɋ߂Â�
    public void EventTrigger(string id, int input)
    {
        if (onEventEnter != null)
            onEventEnter(id, input);
    }
    //�C�x���g���痣���
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

}
