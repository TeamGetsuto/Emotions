using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionBarControl : MonoBehaviour
{
    //UI スライダー
    [SerializeField] GameObject hapiness; //喜びゲージ
    [SerializeField] GameObject sadness;  //悲しみゲージ
    [SerializeField] GameObject anger;    //怒りゲージ
    Slider hapinessBar;
    Slider sadnessBar;
    Slider angerBar;

    //プレイヤー
    [SerializeField] GameObject player;

    //感情システム
    EmotionSystem emotionSystem;

    // Start is called before the first frame update
    void Start()
    {
        hapinessBar = hapiness.GetComponent<Slider>();
        sadnessBar = sadness.GetComponent<Slider>();
        angerBar = anger.GetComponent<Slider>();

        emotionSystem = player.GetComponent<EmotionSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hapinessBar == null)
        {
            Debug.Log("null");
            return;
        }
        
        BarUpDate();
    }

    void BarUpDate()
    {
        hapinessBar.value = emotionSystem.playerEmotionHappiness;
        sadnessBar.value = emotionSystem.playerEmotionSadness;
        angerBar.value = emotionSystem.playerEmotionAnger;
    }
}
