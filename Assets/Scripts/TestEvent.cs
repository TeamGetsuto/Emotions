using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : EventParentClass
{
    //eventHasEnded ��true �ɐݒ肷��
    
    //�v���C���[���߂����ǂ����m�F
    void Update()
    {
        //�v���C���[�̊m�F �����a      ���I�t�Z�b�g  �g���Ă���̂�CheckSphere�i���́j
        EventPlayerCheck(0.5f, Vector3.zero);
    }
    //��Ƃ��Ă��������`�ɂȂ�܂�
    /// /// /// /// /// /// /// 

    protected override void EventHappiness()
    {
        Debug.Log("1");
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("2");
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("3");
        animatiionEnded = true;
    }
    /// /// /// /// /// /// /// 
}
