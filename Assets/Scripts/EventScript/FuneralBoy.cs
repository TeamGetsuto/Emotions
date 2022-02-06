using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuneralBoy : EventParentClass
{
    //eventHasEnded ��true �ɐݒ肷��


    /// /// /// /// /// /// /// ///

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
        
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("�߂��݂𔭐����܂���");
        EmotionSystem.current.PlayerEventEmotionChange(-5, 10, 0);
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("�{��𔭐����܂���");
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