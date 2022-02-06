using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    [Header("Fade Property")]
    [SerializeField] float fadeSpeed;�@//�t�F�[�h�̃X�s�[�h
    [SerializeField] bool isFadeIn;    //�t�F�[�h�C��
    bool isFadeOut;                    //�t�F�[�h�A�E�g

    Image fadeImage;
    float red, green, blue, alfa;

    // Start is called before the first frame update
    void Start()
    {
        Fade_Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //�t�F�[�h�C��
        if(isFadeIn)
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
    }
    //�t�F�[�h�C��
    void StartFadeIn()
    {
        alfa -= fadeSpeed;         //�s�����x�����X�ɉ�����
        SetAlpha();                //�ύX���������x���p�l���ɔ��f����
        if (alfa <= 0)
        {                          //���S�ɓ����ɂȂ�����5�b��Ƀt�F�[�h�A�E�g�Ɉڍs
            isFadeIn = false;
            Invoke("FadeOut", 5);
        }
    }
    //�t�F�[�h�A�E�g
    void StartFadeOut()
    {
        alfa += fadeSpeed;         //�s�����x�����X�ɏグ��
        SetAlpha();                //�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)
        {                          //���S�ɕs�����ɂȂ����珈���𔲂���
            isFadeOut = false;
        }
    }

    void FadeOut()
    {
        isFadeOut = true;
    }
    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}