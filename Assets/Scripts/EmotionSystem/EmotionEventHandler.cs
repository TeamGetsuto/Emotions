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

    public event Func<string,int> onUiButton;

    public event Action onTurnChange;

    public event Action onLoadEnding;

    public event Action onNewTimeStart;

    public event Action onChosedEvents;

    public event Action<bool, bool, bool> onShowButtons;
    public event Action onCloseButtons;

    public event Func<int> onButtonPush;

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

    public int OnUiButtonTrigger(string id)
    {
        if (onUiButton != null)
            return onUiButton(id);
        return -1;
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

    public void OnShowButtonsTrigger(bool h, bool s, bool a)
    {
        if(onShowButtons != null)
        {
            onShowButtons(h, s, a);
        }
    }

    public void OnCloseButtons()
    {
        if (onCloseButtons != null)
        {
            onCloseButtons();
        }
    }

    public int OnButtonPush()
    {
        if (onButtonPush != null)
            return onButtonPush();
        return -1;
    }

}
