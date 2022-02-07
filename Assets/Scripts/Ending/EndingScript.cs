using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    [Header("Fade Property")]
    [SerializeField] float fadeSpeed;　//フェードのスピード
    [SerializeField] bool isFadeIn;    //フェードイン
    bool isFadeOut;                    //フェードアウト
    bool isSecondFadeIn;               //2回目のフェードイン

    [Header("Result Pruperty")]
    [SerializeField] private Text resulutText;       //表示されるテキスト
    [SerializeField] private GameObject resultPanel; //リザルト用のパネル
    [SerializeField] int emotionNum;                 //感情の値

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
        //フェードイン
        if (isFadeIn)
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
        isSecondFadeIn = true;
    }
    //フェードイン
    void StartFadeIn()
    {
        alfa -= fadeSpeed * Time.deltaTime;         //不透明度を徐々に下げる
        SetAlpha();                //変更した透明度をパネルに反映する
        if (alfa <= 0)
        {                          //完全に透明になったら5秒後にフェードアウトに移行
            isFadeIn = false;
            StartCoroutine("FadeWait");
        }
    }
    //フェードアウト
    void StartFadeOut()
    {
        alfa += fadeSpeed * Time.deltaTime;         //不透明度を徐々に上げる
        SetAlpha();                //変更した透明度をパネルに反映する
        if (alfa >= 1)
        {                          //完全に不透明になったら処理を抜ける
            isFadeOut = false;
        }
    }
    IEnumerator StartEnding() //エンディングシーンの開始
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

    void ResultEmotion(int emotionNum) //引数によって表示するテキスト変える
    {
        if (emotionNum == 0)
        {
            resulutText.text = "みんなが喜びの感情になった！！";
        }
        else if (emotionNum == 1)
        {
            resulutText.text = "みんなが悲しみの感情になった…";
        }
        else if (emotionNum == 2)
        {
            resulutText.text = "みんなが怒りの感情になった…";
        }
    }
    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}