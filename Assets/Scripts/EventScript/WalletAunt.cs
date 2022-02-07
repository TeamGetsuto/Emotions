using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletAunt : EventParentClass
{
    //eventHasEnded ��true �ɐݒ肷��
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteHappy;
    [SerializeField] Sprite spriteSad;

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
        EmotionSystem.current.PlayerEventEmotionChange(5, 15, 5);
        animatiionEnded = true;
    }

    protected override void EventSadness()
    {
        Debug.Log("�߂��݂𔭐����܂���");
        spriteRenderer.sprite = spriteSad;
        EmotionSystem.current.PlayerEventEmotionChange(15, 5, 10);
        animatiionEnded = true;
    }

    protected override void EventAnger()
    {
        Debug.Log("�{��𔭐����܂���");
        EmotionSystem.current.PlayerEventEmotionChange(5, -5, 15);
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
