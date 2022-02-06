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
    public int playerEmotionHappiness;
    [Range(0,100)]  //悲しみ
    public int playerEmotionSadness;
    [Range(0,100)]  //怒り
    public int playerEmotionAnger;

    /// //////////
    /// //////////

    /// //////////
    /// 関数
    /// //////////

    void Start()
    {
        
    }


    //感情の値を変える関数
    public void PlayerEventEmotionChange(int eHap, int eSad, int eAng, Action resultAction = null)
    {
        //このシステムにある値を変えます
        current.playerEmotionHappiness -= eHap;
        current.playerEmotionSadness -= eSad;
        current.playerEmotionAnger -= eAng;
        resultAction();
    }

    /// //////////
    /// //////////

}
