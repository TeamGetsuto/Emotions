using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : EventParentClass
{
    //eventHasEnded をtrue に設定する


    //プレイヤーが近いかどうか確認
    void FixedUpdate()
    {
        //プレイヤーの確認 ↓半径      ↓オフセット  使っているのはCheckSphere（球体）
        EventPlayerCheck(base.eventRadius, base.eventOffset);
    }
    //例としてこういう形になります
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
    /// 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, base.eventRadius);
    }
}
