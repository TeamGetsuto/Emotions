using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuneralBoy : EventParentClass
{
    //eventHasEnded をtrue に設定する


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
        EmotionSystem.current.PlayerEventEmotionChange(10, -10, -5);
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("悲しみを発生しました");
        EmotionSystem.current.PlayerEventEmotionChange(-5, 10, 5);
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("怒りを発生しました");
        EmotionSystem.current.PlayerEventEmotionChange(-5, -5, 10);
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
