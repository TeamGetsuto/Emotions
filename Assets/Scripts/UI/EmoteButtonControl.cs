using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteButtonControl : MonoBehaviour
{
    //����{�^��
    [Header("EmoteButton")]
    [SerializeField] GameObject happinessButton;
    [SerializeField] GameObject sadnessButton;
    [SerializeField] GameObject angerButton;

    //�{�^���z�u�̏ꏊ
    [Header("IntializePlace")]
    [SerializeField] Vector3 placeTop;
    [SerializeField] Vector3 placeLeft;
    [SerializeField] Vector3 placeRight;

    //����{�^�����z�u����Ă��邩�ǂ���
    private bool isButtonPlaced = false;

    //����{�^���������ꂽ���ǂ���
    private bool isButtonPushed = false;

    //�C�x���g���X�i�[�ƂȂ�{�^���R���|�[�l���g
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
            Debug.LogError("��̃{�^��������܂�");
            return;
        }
        if(sadnessButton == null)
        {
            Debug.LogError("��̃{�^��������܂�");
            return;
        }
        if(angerButton == null)
        {
            Debug.LogError("��̃{�^��������܂�");
            return;
        }

        if(placeTop == Vector3.zero || placeLeft == Vector3.zero || placeRight == Vector3.zero)
        {
            Debug.LogError("���͂���Ă��Ȃ��x�N�^�[������܂�");
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
        //�{�^�����\����
        happinessButton.SetActive(false);
        
        sadnessButton.SetActive(false);
        angerButton.SetActive(false);

        //�R���|�[�l���g�̎擾
        H_Button = happinessButton.GetComponent<Button>();
        S_Button = sadnessButton.GetComponent<Button>();
        A_Button = angerButton.GetComponent<Button>();
        audioPlayer = Camera.main.GetComponent<SE_Initializer>();

        //�C�x���g���X�i�\�̒ǉ�
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
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        isButtonPushed = true;
        Debug.Log("Happiness");
        emotion = "Hap";
    }

    public void SadnessButton()
    {
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        isButtonPushed = true;
        Debug.Log("Sadness");
        emotion = "Sad";
    }

    public void AngerButton()
    {
        //�ォ��ǉ�
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
