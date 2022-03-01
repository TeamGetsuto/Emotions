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
    [SerializeField] Vector3 placeLeft;
    [SerializeField] Vector3 placeRight;
    [SerializeField] float stretchRate;
    float canvasY = 1080 / 2;
    float canvasX = 1960 / 2;

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

        if(placeLeft == Vector3.zero || placeRight == Vector3.zero)
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
        {
            Vector3 showPlace = new Vector3(canvasX, placeLeft.y + canvasY, placeLeft.z);
            ButtonPlacement(happinessButton, showPlace);
        }
        if(s)
        {
            Vector3 showPlace = new Vector3(canvasX, placeLeft.y + canvasY, placeLeft.z);
            ButtonPlacement(sadnessButton, showPlace);
        }
        if(a)
        {
            Vector3 showPlace = new Vector3(canvasX, placeLeft.y + canvasY, placeLeft.z);
            ButtonPlacement(angerButton, showPlace);
        }
        if(h && s)
        {
            Vector3 showPlace = new Vector3(canvasX, placeLeft.y + canvasY, placeLeft.z);
            ButtonPlacement(happinessButton, new Vector3(showPlace.x + (placeLeft.x * stretchRate), showPlace.y, showPlace.z));
            ButtonPlacement(sadnessButton, new Vector3(showPlace.x + (placeRight.x * stretchRate), showPlace.y, showPlace.z));
        }
        if(h && a)
        {
            Vector3 showPlace = new Vector3(canvasX, placeLeft.y + canvasY, placeLeft.z);
            ButtonPlacement(happinessButton, new Vector3(showPlace.x + (placeLeft.x * stretchRate), showPlace.y, showPlace.z));
            ButtonPlacement(angerButton, new Vector3(showPlace.x + (placeRight.x * stretchRate), showPlace.y, showPlace.z));

            Debug.Log(showPlace);
        }
        if(s && a)
        {
            Vector3 showPlace = new Vector3(canvasX, placeLeft.y + canvasY, placeLeft.z);
            ButtonPlacement(sadnessButton, new Vector3(showPlace.x + (placeLeft.x * stretchRate), showPlace.y, showPlace.z));
            ButtonPlacement(angerButton, new Vector3(showPlace.x + (placeRight.x * stretchRate), showPlace.y, showPlace.z));
        }
        if(h && s && a)
        {
            Vector3 showPlace = new Vector3(canvasX, placeLeft.y + canvasY, placeLeft.z);
            ButtonPlacement(sadnessButton, new Vector3(canvasX + placeLeft.x, placeLeft.y + canvasY, placeLeft.z));
            ButtonPlacement(angerButton, new Vector3(canvasX + placeRight.x, placeRight.y + canvasY, placeRight.z));
            ButtonPlacement(happinessButton, showPlace);
        }

        Time.timeScale = 0;
        Debug.Log("Button�z�u����");
    }

    public void ButtonClose()
    {
        ButtonRemove(happinessButton);
        ButtonRemove(sadnessButton);
        ButtonRemove(angerButton);
        ButtonRemove(startButton);

        Time.timeScale = 1;
        EventTextControl.instance.isEmotoChange = true;
        EventTextControl.instance.isSetList = false;
        Debug.Log("Button�P������" + EventTextControl.instance.isSetList);
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


    public void HappinessButton(out string input)
    {
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        input = "h";
        ButtonClose();
    }

    public void SadnessButton(out string input)
    {
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        input = "s";
        ButtonClose();
    }

    public void AngerButton(out string input)
    {
        //�ォ��ǉ�
        audioPlayer.AudioPlay(audioPlayer.emoteEffect, audioPlayer.seVolume1);
        input = "a";
        ButtonClose();
    }


}
