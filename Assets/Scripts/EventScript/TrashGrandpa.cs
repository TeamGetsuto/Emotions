using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGrandpa : EventParentClass
{
    //eventHasEnded をtrue に設定する

    SpriteRenderer spriteRenderer;
    public Sprite girlSprite;
    private void Awake()
    {
        spriteRenderer = transform.Find("mob_grand").GetComponent<SpriteRenderer>();
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
        spriteRenderer.sprite = girlSprite;
        EmotionSystem.current.PlayerEventEmotionChange(10, -5, 0);
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
