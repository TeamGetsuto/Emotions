using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�L�����N�^�̊�����Ǘ�����X�N���v�g
public class EmotionSystem : MonoBehaviour
{
    /// //////////
    //�V���O���g��

    #region Singleton
    public static EmotionSystem current;
    private void Awake()
    {
        if (current != null && current != this)
            Destroy(this);
        else current = this;
    }
    #endregion

    /// //////////
    /// //////////

    /// //////////
    /// �ϐ�
    /// //////////

    [Header("Emotions starting values")]
    [Range(0,100)]  //���
    [SerializeField] int emotionHappiness;
    [Range(0,100)]  //�߂���
    [SerializeField] int emotionSadness;
    [Range(0,100)]  //�{��
    [SerializeField] int emotionAnger;
    [Range(0,100)]�@//������
    [SerializeField] int emotionFear;

    /// //////////
    /// //////////

    /// //////////
    /// �֐�
    /// //////////

    void Start()
    {
        
    }


    //����̒l��ς���֐�
    public void EventEmotionChange(int eHap, int eSad, int eAng, int eFea, Action resultAction = null)
    {
        //���̃V�X�e���ɂ���l��ς��܂�
        current.emotionHappiness    -= eHap;
        current.emotionSadness      -= eSad;
        current.emotionAnger        -= eAng;
        current.emotionFear         -= eFea;
        resultAction();
    }

    /// //////////
    /// //////////

}
