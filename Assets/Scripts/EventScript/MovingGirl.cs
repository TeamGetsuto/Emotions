using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGirl : EventParentClass
{
    //eventHasEnded ��true �ɐݒ肷��
    
    //���̊֐���ς�����Ȃ��I
    /// /// /// /// /// /// /// ///
    void FixedUpdate()
    {
        //�v���C���[�̊m�F �����a      ���I�t�Z�b�g  �g���Ă���̂�CheckSphere�i���́j
        EventPlayerCheck(base.eventRadius, base.eventOffset);
    }
    /// /// /// /// /// /// /// ///
    
    //��Ƃ��Ă��������`�ɂȂ�܂�
    /// /// /// /// /// /// /// 

    protected override void EventHappiness()
    {
        Debug.Log("��т𔭐����܂���");
        EmotionSystem.current.PlayerEventEmotionChange(20, 0, -10);
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("�߂��݂𔭐����܂���");
        EmotionSystem.current.PlayerEventEmotionChange(-10, 20, 0);
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("�{��𔭐����܂���");
        EmotionSystem.current.PlayerEventEmotionChange(0, -10, 20);
        animatiionEnded = true;
    }
    /// /// /// /// /// /// /// 
    /// 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + base.eventOffset, base.eventRadius);
    }
}
