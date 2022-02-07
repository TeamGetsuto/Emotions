using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    [Header("Fade Property")]
    [SerializeField] float fadeSpeed;�@//�t�F�[�h�̃X�s�[�h
    [SerializeField] bool isFadeIn;    //�t�F�[�h�C��
    bool isFadeOut;                    //�t�F�[�h�A�E�g
    bool isSecondFadeIn;               //2��ڂ̃t�F�[�h�C��

    [Header("Result Pruperty")]
    [SerializeField] private Text resulutText;       //�\�������e�L�X�g
    [SerializeField] private GameObject resultPanel; //���U���g�p�̃p�l��
    [SerializeField] int emotionNum;                 //����̒l

    private EmotionSystem staticObject;

    [SerializeField] GameObject fin;

    Image fadeImage;
    float red, green, blue, alfa;

    // Start is called before the first frame update
    void Start()
    {
        Fade_Initialize();

        staticObject = GameObject.Find("TurnSystem").GetComponent<EmotionSystem>();

        if (staticObject.cityEmotionAnger > staticObject.cityEmotionHappiness && staticObject.cityEmotionAnger > staticObject.cityEmotionSadness)
            emotionNum = 2;
        else
        if (staticObject.cityEmotionAnger < staticObject.cityEmotionHappiness && staticObject.cityEmotionHappiness > staticObject.cityEmotionSadness)
            emotionNum = 0;
        else
            emotionNum = 1;

        ResultEmotion(emotionNum);
        StartCoroutine("StartEnding");
    }

    // Update is called once per frame
    void Update()
    {
        //�t�F�[�h�C��
        if (isFadeIn)
        {
            StartFadeIn();
        }
        //�t�F�[�h�A�E�g
        if(isFadeOut)
        {
            StartFadeOut();
        }
    }

    void Fade_Initialize()
    {
        fadeImage = GetComponent<Image>(); //�p�l�����擾
        red = fadeImage.color.r;           //��
        green = fadeImage.color.g;         //��
        blue = fadeImage.color.b;          //��
        alfa = fadeImage.color.a;          //����
        isSecondFadeIn = true;
    }
    //�t�F�[�h�C��
    void StartFadeIn()
    {
        alfa -= fadeSpeed * Time.deltaTime;         //�s�����x�����X�ɉ�����
        SetAlpha();                //�ύX���������x���p�l���ɔ��f����
        if (alfa <= 0)
        {                          //���S�ɓ����ɂȂ�����5�b��Ƀt�F�[�h�A�E�g�Ɉڍs
            isFadeIn = false;
            StartCoroutine("FadeWait");
        }
    }
    //�t�F�[�h�A�E�g
    void StartFadeOut()
    {
        alfa += fadeSpeed * Time.deltaTime;         //�s�����x�����X�ɏグ��
        SetAlpha();                //�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)
        {                          //���S�ɕs�����ɂȂ����珈���𔲂���
            isFadeOut = false;
        }
    }
    IEnumerator StartEnding() //�G���f�B���O�V�[���̊J�n
    {
        yield return new WaitForSeconds(2);
        isFadeIn = true;
    }
    IEnumerator FadeWait()
    {
        yield return new WaitForSeconds(3);

        isFadeOut = true;

        yield return new WaitForSeconds(3);
        if(isSecondFadeIn)
        {
            isFadeIn = true;
            isSecondFadeIn = false;
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
        resultPanel.SetActive(false);
        fin.transform.position = new Vector3(6.5f, -3.4f, 0);
    }

    void ResultEmotion(int emotionNum) //�����ɂ���ĕ\������e�L�X�g�ς���
    {
        if (emotionNum == 0)
        {
            resulutText.text = "�݂�Ȃ���т̊���ɂȂ����I�I";
        }
        else if (emotionNum == 1)
        {
            resulutText.text = "�݂�Ȃ��߂��݂̊���ɂȂ����c";
        }
        else if (emotionNum == 2)
        {
            resulutText.text = "�݂�Ȃ��{��̊���ɂȂ����c";
        }
    }
    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}