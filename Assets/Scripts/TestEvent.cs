using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : EventParentClass
{
    //�v���C���[���߂����ǂ����m�F
    void Update()
    {
        EventPlayerCheck(0.5f, Vector3.zero);
    }
    //��Ƃ��Ă��������`�ɂȂ�܂�
    /// /// /// /// /// /// /// 

    protected override void EventA()
    {
        Debug.Log("1");
    }

    protected override void EventB()
    {
        Debug.Log("2");
    }

    protected override void EventC()
    {

    }

    protected override void EventD()
    {
        Debug.Log("3");
    }
    /// /// /// /// /// /// /// 
}
