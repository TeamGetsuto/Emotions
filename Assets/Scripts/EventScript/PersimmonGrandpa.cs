using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersimmonGrandpa : EventParentClass
{
    //eventHasEnded ��true �ɐݒ肷��

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteHappy;

    private void Awake()
    {
        spriteRenderer = transform.Find("Effect").GetComponent<SpriteRenderer>();
    }


    //�v���C���[���߂����ǂ����m�F
    void FixedUpdate()
    {
        //�v���C���[�̊m�F �����a      ���I�t�Z�b�g  �g���Ă���̂�CheckSphere�i���́j
        EventPlayerCheck(base.eventRadius, base.eventOffset);
    }
    //��Ƃ��Ă��������`�ɂȂ�܂�
    /// /// /// /// /// /// /// 

    protected override void EventHappiness()
    {
        Debug.Log("1");
        spriteRenderer.sprite = spriteHappy;
        EmotionSystem.current.PlayerEventEmotionChange(20, 0, -10);
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
        EmotionSystem.current.PlayerEventEmotionChange(-10, 0, 20);
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