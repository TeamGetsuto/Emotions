using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteButtonControl : MonoBehaviour
{
    //感情ボタン
    [Header("EmoteButton")]
    [SerializeField] GameObject happinessButton;
    [SerializeField] GameObject sadnessButton;
    [SerializeField] GameObject angerButton;

    //ボタン配置の場所
    [Header("IntializePlace")]
    [SerializeField] Vector3 placeTop;
    [SerializeField] Vector3 placeLeft;
    [SerializeField] Vector3 placeRight;

    //感情ボタンが配置されているかどうか
    private bool isButtonPlaced = false;

    //感情ボタンが押されたかどうか
    private bool isButtonPushed = false;

    //イベントリスナーとなるボタンコンポーネント
    Button H_Button;
    Button S_Button;
    Button A_Button;

    SE_Initializer audioPlayer;
    EventParentClass eventSystem;

    string emotion = "";

    private void Awake()
    {
        EmotionEventHandler.current.onShowButtons += ButtonShow;
        EmotionEventHandler.current.onCloseButtons += ButtonClose;

        EmotionEventHandler.current.onButtonPush += InputEmot;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(happinessButton == null)
        {
            Debug.LogError("空のボタンがあります");
            return;
        }
        if(sadnessButton == null)
        {
            Debug.LogError("空のボタンがあります");
            return;
        }
        if(angerButton == null)
        {
            Debug.LogError("空のボタンがあります");
            return;
        }

        if(placeTop == Vector3.zero || placeLeft == Vector3.zero || placeRight == Vector3.zero)
        {
            Debug.LogError("入力されていないベクターがあります");
            return;
        }

        ButtonInitialize();
    }

    void ButtonShow(bool h, bool s, bool a)
    {
        if (isButtonPlaced)
        {
            return;
        }

        if(h)
            ButtonPlacement(happinessButton, placeTop);
        if(s)
            ButtonPlacement(sadnessButton, placeLeft);
        if(a)
            ButtonPlacement(angerButton, placeRight);
    }

    void ButtonClose()
    {
        string emotion = "";
        ButtonRemove(happinessButton);
        ButtonRemove(sadnessButton);
        ButtonRemove(angerButton);
    }
    void ButtonInitialize()
    {
        //ボタンを非表示に
        happinessButton.SetActive(false);
        
        sadnessButton.SetActive(false);
        angerButton.SetActive(false);

        //コンポーネントの取得
        H_Button = happinessButton.GetComponent<Button>();
        S_Button = sadnessButton.GetComponent<Button>();
        A_Button = angerButton.GetComponent<Button>();
        audioPlayer = Camera.main.GetComponent<SE_Initializer>();

        //イベントリスナ―の追加
        H_Button.onClick.AddListener(HappinessButton);


        S_Button.onClick.AddListener(SadnessButton);
        A_Button.onClick.AddListener(AngerButton);
    }




    void ButtonPlacement(GameObject obj,Vector3 place)
    {
        obj.transform.position = place;
        obj.SetActive(true);
        isButtonPlaced = true;
    }

    void ButtonRemove(GameObject obj)
    {
        obj.SetActive(false);
        isButtonPlaced = false;
    }


    public void HappinessButton()
    {
        //後から追加
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        isButtonPushed = true;
        Debug.Log("Happiness");
        emotion = "Hap";
    }

    public void SadnessButton()
    {
        //後から追加
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        isButtonPushed = true;
        Debug.Log("Sadness");
        emotion = "Sad";
    }

    public void AngerButton()
    {
        //後から追加
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        isButtonPushed = true;
        Debug.Log("Anger");
        emotion = "Ang";
    }

    int InputEmot()
    {

        int a = InputEmotion(emotion);
        emotion = "";
        return a;
    }

    int InputEmotion(string emotion)
    {
        if (emotion == "Hap")
        {
            return 0;
        }
        if (emotion == "Sad")
            return 1;
        if (emotion == "Ang")
            return 2;
        return -1;
    }

}
