using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : EventParentClass
{
    //プレイヤーが近いかどうか確認
    void Update()
    {
        EventPlayerCheck(0.5f, Vector3.zero);
    }
    //例としてこういう形になります
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

    protected override void ResultA()
    {
        Debug.Log("1, 2");
    }

    protected override void ResultB()
    {
        Debug.Log("2, 3");
    }

    protected override void ResultC()
    {

    }

    protected override void ResultD()
    {

    }
    /// /// /// /// /// /// /// 

}
