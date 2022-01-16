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
    /// 変数
    /// //////////

    [Header("Emotions starting values")]
    [Range(0,100)]  //喜び
    public int emotionHappiness;
    [Range(0,100)]  //悲しみ
    public int emotionSadness;
    [Range(0,100)]  //怒り
    public int emotionAnger;
    [Range(0,100)]　//恐がり
    public int emotionFear;

    /// //////////
    /// //////////

    /// //////////
    /// 関数
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

    //感情の値を変える関数
    public static void EventEmotionChange(int eHap, int eSad, int eAng, int eFea, Action resultAction = null)
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
