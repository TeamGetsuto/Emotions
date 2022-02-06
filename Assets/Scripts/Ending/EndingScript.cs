using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    [Header("Fade Property")]
    [SerializeField] float fadeSpeed;　//フェードのスピード
    [SerializeField] bool isFadeIn;    //フェードイン
    bool isFadeOut;                    //フェードアウト

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
        //フェードイン
        if(isFadeIn)
        {
            StartFadeIn();
        }
        //フェードアウト
        if(isFadeOut)
        {
            StartFadeOut();
        }
    }

    void Fade_Initialize()
    {
        fadeImage = GetComponent<Image>(); //パネルを取得
        red = fadeImage.color.r;           //赤
        green = fadeImage.color.g;         //緑
        blue = fadeImage.color.b;          //青
        alfa = fadeImage.color.a;          //透明
    }
    //フェードイン
    void StartFadeIn()
    {
        alfa -= fadeSpeed;         //不透明度を徐々に下げる
        SetAlpha();                //変更した透明度をパネルに反映する
        if (alfa <= 0)
        {                          //完全に透明になったら5秒後にフェードアウトに移行
            isFadeIn = false;
            Invoke("FadeOut", 5);
        }
    }
    //フェードアウト
    void StartFadeOut()
    {
        alfa += fadeSpeed;         //不透明度を徐々に上げる
        SetAlpha();                //変更した透明度をパネルに反映する
        if (alfa >= 1)
        {                          //完全に不透明になったら処理を抜ける
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