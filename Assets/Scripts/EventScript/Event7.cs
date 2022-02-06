using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event7 : EventParentClass
{
    //eventHasEnded をtrue に設定する



    //初期化はこっち！
    /// /// /// /// /// /// /// ///
    private void Awake()
    {
        
    }

    /// /// /// /// /// /// /// ///

    //この関数を変えたらない！
    /// /// /// /// /// /// /// ///
    void FixedUpdate()
    {
        //プレイヤーの確認 ↓半径      ↓オフセット  使っているのはCheckSphere（球体）
        EventPlayerCheck(base.eventRadius, base.eventOffset);
    }
    /// /// /// /// /// /// /// ///
    
    //例としてこういう形になります
    /// /// /// /// /// /// /// 

    protected override void EventHappiness()
    {
        Debug.Log("喜びを発生しました");   
        
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("悲しみを発生しました");
 
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("怒りを発生しました");
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
