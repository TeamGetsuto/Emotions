using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredMan: EventParentClass
{
    //eventHasEnded ��true �ɐݒ肷��

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteHappy;
    [SerializeField] Sprite spriteAnger;

    //�������͂������I
    /// /// /// /// /// /// /// ///
    private void Awake()
    {
        spriteRenderer = transform.Find("Effect").GetComponent<SpriteRenderer>();
    }

    /// /// /// /// /// /// /// ///

    //���̊֐���ς�����Ȃ��I
    /// /// /// /// /// /// /// ///
    void FixedUpdate()
    {
        //�v���C���[�̊m�F �����a      ���I�t�Z�b�g  �g���Ă���̂�CheckSphere�i���́j
        EventPlayerCheck(base.eventRadius, base.eventOffset);
    }
    /// /// /// /// /// /// /// ///
    
    //��Ƃ��Ă��������`�ɂȂ�܂�
    /// /// /// /// /// /// /// 

    protected override void EventHappiness()
    {
        Debug.Log("��т𔭐����܂���");
        spriteRenderer.sprite = spriteHappy;
        EmotionSystem.current.PlayerEventEmotionChange(-15, 20, 5);
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("�߂��݂𔭐����܂���");
        EmotionSystem.current.PlayerEventEmotionChange(15, 20, -40);
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("�{��𔭐����܂���");
        spriteRenderer.sprite = spriteAnger;
        EmotionSystem.current.PlayerEventEmotionChange(15, 10, 20);
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
