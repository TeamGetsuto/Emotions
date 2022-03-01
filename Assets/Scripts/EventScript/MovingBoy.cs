using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoy : EventParentClass
{
    //eventHasEnded ��true �ɐݒ肷��

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteHappy;
    [SerializeField] Sprite spriteAnger;

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
        EmotionSystem.current.PlayerEventEmotionChange(20, -10, 0);
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("�߂��݂𔭐����܂���");
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("�{��𔭐����܂���");
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
