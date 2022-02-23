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
    /// �C�x���g����
    public event Action<string, int> onEventUnlock;

    public event Func<string,int> onUiButton;

    public event Action onNewTimeStart;

    public event Action<bool, bool, bool> onShowButtons;
    public event Action onCloseButtons;

    public event Func<int> onButtonPush;

    //�C�x���g�ɋ߂�




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
