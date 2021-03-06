using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetsGrandpa : EventParentClass
{
    //eventHasEnded をtrue に設定する

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteHappy;
    [SerializeField] Sprite spriteSad;
    private void Awake()
    {
        spriteRenderer = transform.Find("Effect").GetComponent<SpriteRenderer>();
    }


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

        EmotionSystem.current.PlayerEventEmotionChange(20, -10, 0);
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("2");
        EmotionSystem.current.PlayerEventEmotionChange(-10, 20, 0);
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
