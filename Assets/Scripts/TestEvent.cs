using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : EventParentClass
{
    //プレイヤーが近いかどうか確認
    void Update()
    {
        //プレイヤーの確認 ↓半径      ↓オフセット  使っているのはCheckSphere（球体）
        EventPlayerCheck(0.5f, Vector3.zero);
    }
    //例としてこういう形になります
    /// /// /// /// /// /// /// 

    protected override void EventHappiness()
    {
        Debug.Log("1");
    }

    protected override void EventSadness()
    {
        Debug.Log("2");
    }

    protected override void EventAnger()
    {

    }
    /// /// /// /// /// /// /// 
}
