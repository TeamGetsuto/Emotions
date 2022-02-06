using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionBarControl : MonoBehaviour
{
    //UI �X���C�_�[
    [SerializeField] GameObject hapiness; //��уQ�[�W
    [SerializeField] GameObject sadness;  //�߂��݃Q�[�W
    [SerializeField] GameObject anger;    //�{��Q�[�W
    Slider hapinessBar;
    Slider sadnessBar;
    Slider angerBar;

    //�v���C���[
    [SerializeField] GameObject player;

    //����V�X�e��
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
