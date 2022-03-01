using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoy : EventParentClass
{
    //eventHasEnded をtrue に設定する

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteHappy;
    [SerializeField] Sprite spriteAnger;

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
        spriteRenderer.sprite = spriteHappy;
        EmotionSystem.current.PlayerEventEmotionChange(20, -10, 0);
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
        spriteRenderer.sprite = spriteAnger;
        EmotionSystem.current.PlayerEventEmotionChange(-10, -10, 20);
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
