using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//キャラクタの感情を管理するスクリプト
public class EmotionSystem : MonoBehaviour
{
    /// //////////
    //シングルトン

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
    /// 変数
    /// //////////

    [Header("Emotions starting values")]
    [Range(0,100)]  //喜び
    [SerializeField] int emotionHappiness;
    [Range(0,100)]  //悲しみ
    [SerializeField] int emotionSadness;
    [Range(0,100)]  //怒り
    [SerializeField] int emotionAnger;
    [Range(0,100)]　//恐がり
    [SerializeField] int emotionFear;

    /// //////////
    /// //////////

    /// //////////
    /// 関数
    /// //////////

    void Start()
    {
        
    }


    //感情の値を変える関数
    public void EventEmotionChange(int eHap, int eSad, int eAng, int eFea, Action resultAction = null)
    {
        //このシステムにある値を変えます
        current.emotionHappiness    -= eHap;
        current.emotionSadness      -= eSad;
        current.emotionAnger        -= eAng;
        current.emotionFear         -= eFea;
        resultAction();
    }

    /// //////////
    /// //////////

}
