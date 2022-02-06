using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUiControl : MonoBehaviour
{
    [Header("TitleScene")]
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject titlelogo;
    [SerializeField] GameObject startlogo;

    Image fadeImage;
    Image startImage;
    Button startButton;

    [Header("LogoMoveProperty")]
    [SerializeField] float logoSpeed;
    [SerializeField] Vector3 logoEndPos;

    RectTransform logoPos;

    private float logoStartPos;
    public bool logoMoveComp = false;

    [Header("StartMoveProperty")]
    [SerializeField] float startSpeed;
    [SerializeField] Vector3 startLogoEndPos;

    RectTransform startPos;

    private float startInitPos;
    public bool isStartMoved = false;
    bool isEventAtached = false;

    [Header("FadeProperty")]
    [SerializeField] float fadeSpeed;

    private float r1, g1, b1, a1;
    private float r2, g2, b2, a2;

    public bool isFadeIn = false;
    public bool isFadeOut = false;

    SE_Initializer soundPlayer;

    SceneChanger sceneChanger;
    
    // Start is called before the first frame update
    void Start()
    {
        UI_Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();

        if(!isFadeIn)
        {
            return;
        }

        TitleLogoMove();

        if(!logoMoveComp)
        {
            return;
        }

        StartLogoMove();

        if(!isStartMoved)
        {
            return;
        }

        
    }

    void UI_Initialize()
    {
        //�e��R���|�[�l���g�̎擾
        soundPlayer = Camera.main.GetComponent<SE_Initializer>();
        sceneChanger = GetComponent<SceneChanger>();
        fadeImage = fadePanel.GetComponent<Image>();
        startButton = startlogo.GetComponent<Button>();
        startImage = startlogo.GetComponent<Image>();
        logoPos = titlelogo.GetComponent<RectTransform>();
        startPos = startlogo.GetComponent<RectTransform>();

        //�^�C�g�����S�̏�����
        logoPos.anchoredPosition = new Vector3(0, 150.0f, -5.0f);
        logoStartPos = logoPos.anchoredPosition.y;

        //�X�^�[�g�{�^���̏�����
        startPos.anchoredPosition = new Vector3(0, -90, -5.0f);
        startInitPos = startPos.anchoredPosition.y;
        
        //�t�F�[�h�֘A�̏�����
        fadePanel.SetActive(true);

        r1 = fadeImage.color.r;
		g1 = fadeImage.color.g;
		b1 = fadeImage.color.b;
		a1 = fadeImage.color.a;

        r2 = startImage.color.r;
        g2 = startImage.color.g;
        b2 = startImage.color.b;
        a2 = startImage.color.a;
    }

    void SetAlpha(float red, float green, float blue, float alfa)
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    void FadeIn()
    {
        a1 -= fadeSpeed * Time.deltaTime; //�s�����x�����X�ɉ�����
        SetAlpha(r1, g1, b1, a1);                         //�ύX�����s�����x���p�l���ɔ��f����
        if (a1 <= 0)
        {                                   
            fadePanel.SetActive(false);     //�p�l���̕\�����I�t�ɂ���
            isFadeIn = true;                //���S�ɓ����ɂȂ����珈���𔲂���
        }
    }

    void FadeOut()
    {
        fadePanel.SetActive(true);           //�p�l���̕\�����I���ɂ���
        a1 += fadeSpeed * Time.deltaTime;    //�s�����x�����X�ɂ�����
        SetAlpha(r1, g1, b1, a1);                          //�ύX���������x���p�l���ɔ��f����
        if (a1 >= 1)
        {                                    //���S�ɕs�����ɂȂ����珈���𔲂���
            isFadeOut = false;
        }
    }

   void StartLogoMove()
    {
        startInitPos += startSpeed * Time.deltaTime;
        startPos.anchoredPosition = new Vector3(0, startInitPos, -5.0f);

        if(startInitPos >= startLogoEndPos.y || Input.GetMouseButtonDown(0))
        {
            startInitPos = startLogoEndPos.y;
            startSpeed = 0;
            isStartMoved = true;
            if(!isEventAtached)
            {
                startButton.onClick.AddListener(StartButtonDown);
            }
            isEventAtached = true;
        }
    }

    void TitleLogoMove()
    {
        logoStartPos -= logoSpeed * Time.deltaTime;
        logoPos.anchoredPosition = new Vector3(0, logoStartPos, -5.0f);

        if(logoStartPos <= logoEndPos.y || Input.GetMouseButtonDown(0))
        {
            logoStartPos = logoEndPos.y;
            logoSpeed = 0;
            logoMoveComp = true;
        }
    }

    public void StartButtonDown()
    {
        soundPlayer.AudioPlay(soundPlayer.emoteEffect, soundPlayer.emoteSeVolume);
        sceneChanger.ChangeScene(1);
    }
}
