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
    private static EmotionSystem current;
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
    public int emotionHappiness;
    [Range(0,100)]  //�߂���
    public int emotionSadness;
    [Range(0,100)]  //�{��
    public int emotionAnger;
    [Range(0,100)]�@//������
    public int emotionFear;

    /// //////////
    /// //////////

    /// //////////
    /// �֐�
    /// //////////

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            EmotionEventHandler.eventHandler += EmotionSystem.EventEmotionChange;
        else
            EmotionEventHandler.eventHandler -= EmotionSystem.EventEmotionChange;
    }

    //����̒l��ς���֐�
    public static void EventEmotionChange(int eHap, int eSad, int eAng, int eFea, Action resultAction = null)
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
