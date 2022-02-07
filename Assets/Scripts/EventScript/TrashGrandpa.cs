using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGrandpa : EventParentClass
{
    //eventHasEnded をtrue に設定する

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteHappy;
    [SerializeField] Sprite spriteAnger;
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

        EmotionSystem.current.PlayerEventEmotionChange(15, -10, -10);
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("2");
        EmotionSystem.current.PlayerEventEmotionChange(0, 15, 0);
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("3");
        EmotionSystem.current.PlayerEventEmotionChange(-10, -10, 20);
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
