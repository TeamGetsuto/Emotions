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

    //[Header("City starting values")]
    //public int cityEmotionHappiness;//喜び
    //public int cityEmotionSadness;//悲しみ
    //public int cityEmotionAnger;//怒り

    /// //////////
    /// //////////

    /// //////////
    /// 関数
    /// //////////
    //感情の値を変える関数
    public void PlayerEventEmotionChange(int eHap, int eSad, int eAng)
    {
        //CityEventEmotionChange(eHap, eSad, eAng);
        //このシステムにある値を変えます
        playerEmotionHappiness += eHap;
        playerEmotionSadness += eSad;
        playerEmotionAnger += eAng;
    }

    //public void CityEventEmotionChange(int eHap, int eSad, int eAng)
    //{
    //    //このシステムにある値を変えます
    //    cityEmotionHappiness -= eHap;
    //    cityEmotionSadness -= eSad;
    //    cityEmotionAnger -= eAng;
    //}

    /// //////////
    /// //////////

}
