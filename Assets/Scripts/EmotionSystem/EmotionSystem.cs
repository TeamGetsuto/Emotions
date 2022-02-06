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
    public int playerEmotionHappiness;
    [Range(0,100)]  //�߂���
    public int playerEmotionSadness;
    [Range(0,100)]  //�{��
    public int playerEmotionAnger;

    /// //////////
    /// //////////

    /// //////////
    /// �֐�
    /// //////////

    void Start()
    {
        
    }


    //����̒l��ς���֐�
    public void PlayerEventEmotionChange(int eHap, int eSad, int eAng, Action resultAction = null)
    {
        //���̃V�X�e���ɂ���l��ς��܂�
        current.playerEmotionHappiness -= eHap;
        current.playerEmotionSadness -= eSad;
        current.playerEmotionAnger -= eAng;
        resultAction();
    }

    /// //////////
    /// //////////

}
