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

    [Header("StartButton")]
    [SerializeField] GameObject startButton;
    [SerializeField] Vector3 startButtonPos;

    //����{�^�����z�u����Ă��邩�ǂ���
    private bool isButtonPlaced = false;

    public static string currentEventID;

    //�C�x���g���X�i�[�ƂȂ�{�^���R���|�[�l���g
    Button H_Button;
    Button S_Button;
    Button A_Button;
    Button Start_Button;

    SE_Initializer audioPlayer;
    EventParentClass eventSystem;

    string emotion = "";

    private void Awake()
    {
        ButtonEvents.current.onShowButtons += ButtonShow;
        ButtonEvents.current.onCloseButtons += ButtonClose;
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

    public void ShowStart()
    {
        startButton.SetActive(true);
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

    public void ButtonClose()
    {
        ButtonRemove(happinessButton);
        ButtonRemove(sadnessButton);
        ButtonRemove(angerButton);
        ButtonRemove(startButton);
    }

    void ButtonInitialize()
    {
        //�{�^�����\����
        happinessButton.SetActive(false);
        sadnessButton.SetActive(false);
        angerButton.SetActive(false);
        startButton.SetActive(false);

        //�R���|�[�l���g�̎擾
        Start_Button = startButton.GetComponent<Button>();
        H_Button = happinessButton.GetComponent<Button>();
        S_Button = sadnessButton.GetComponent<Button>();
        A_Button = angerButton.GetComponent<Button>();
        audioPlayer = Camera.main.GetComponent<SE_Initializer>();

        //�C�x���g���X�i�\�̒ǉ�
        Start_Button.onClick.AddListener(() => StartButton  (out EventParentClass.isStarted));
        H_Button.onClick.AddListener(() => HappinessButton  (out EventTextControl.resultText));
        S_Button.onClick.AddListener(() => SadnessButton    (out EventTextControl.resultText));
        A_Button.onClick.AddListener(() => AngerButton      (out EventTextControl.resultText));
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


    public void StartButton(out bool input)
    {
        input = true;
        startButton.SetActive(false);
        EventSystem.TriggerEvent("ShowText", new Dictionary<string, object> { { "id", currentEventID } });
    }


    public void HappinessButton(out char input)
    {
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        input = 'h';
        ButtonClose();
    }

    public void SadnessButton(out char input)
    {
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        input = 's';
        ButtonClose();
    }

    public void AngerButton(out char input)
    {
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        input = 'a';
        ButtonClose();
    }


}
