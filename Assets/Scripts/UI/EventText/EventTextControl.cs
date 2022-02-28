using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTextControl : MonoBehaviour
{
    //イベントUI
    [SerializeField] GameObject eventTextPanel;
    Image eventTextImage;
    [SerializeField] Text speakerName;
    [SerializeField] Image speakerImage;

    //現在のイベントのテキストを読み込むリスト
    List<EventTextParser.EventTextInfo> currentInfo = new List<EventTextParser.EventTextInfo>();

    //仮のイベントID
    [SerializeField] string eventNum;

    //文字送り
    [Header("TypeWriterEffect")]
    [SerializeField] Text textObj;
    [SerializeField] float delayTime;
    private float pastTime = 0f;
    private int visibleLength = 0;
    private string text = "";
    private bool isTextSet = false;

    //現在の行
    private int currentRow = 0;

    //仮イベント確認用
    bool isEventStart;
    bool isEventEnd;


    void UiInit()
    {
        //パネルを非表示に
        eventTextPanel.SetActive(false);

        //テキストボックスイメージの貼り付け
        eventTextImage = eventTextPanel.GetComponent<Image>();
        eventTextImage.sprite = Resources.Load<Sprite>("Sprite/EventText/textBox");
    }
    // Start is called before the first frame update
    void Start()
    {
        UiInit();
    }

    // Update is called once per frame
    void Update()
    {
        //Fキーが押されたタイミングでイベント発生（仮の処理
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(isEventStart)
            {
                return;
            }
            EventStarted();

            isEventStart = true;
            //Time.timeScale = 0;
        }
        TextBoxUpdate();

        Debug.Log(isTextSet);
    }

    void EventStarted()
    {
        //イベントパネルを可視化
        eventTextPanel.SetActive(true);

        //行数の初期化
        currentRow = 0;

        //イベントIDと一致するテキストを別のリストに読み込み
        foreach(EventTextParser.EventTextInfo line in EventTextParser.textInfo)
        {
            if(line.id == eventNum)
            {
                currentInfo.Add(line);
            }
        }

        Debug.Log(currentInfo.Count);

        //発話者のスプライトを切り替え

        if (currentInfo[currentRow].spritePass == "Player")
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Player/Player_Left");
        }
        else
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Mob/" + currentInfo[currentRow].spritePass);
        }
        
        speakerName.text = currentInfo[currentRow].speakerName;
    }

    //表示する一文ずつ読み込む
    public void SetText(string t_)
    {
        if (isTextSet)
        {
            return;
        }

        text = t_;
        pastTime = 0;
        visibleLength = 0;
        textObj.text = "";
        isTextSet = true;
    }

    void TextBoxUpdate()
    {
        if(isEventStart)
        {
            SetText(currentInfo[currentRow].textMesse);

            //表示文字数が文全体の文字数より少ないとき
            if (visibleLength < text.Length)
            {
                pastTime += Time.deltaTime;

                //delayTimeを基準に一文字ずつ表示
                if (pastTime >= delayTime)
                {
                    pastTime -= delayTime;
                    visibleLength++;
                    textObj.text = text.Substring(0, visibleLength);
                }
            }

            Debug.Log(currentRow);

            if(Input.GetMouseButtonDown(0))
            {
                //文全体の表示が終わってなければ、全文表示
                if(visibleLength < text.Length)
                {
                    visibleLength = text.Length;
                    textObj.text = text;
                }
                else
                {
                    //リストの要素数の範囲を超えるなら
                    if (currentRow == currentInfo.Count - 1)
                    {
                        isEventEnd = true;
                        isEventStart = false;
                    }
                    //行数を送る
                    else
                    {
                        currentRow++;
                    }

                    if(currentInfo[currentRow].state == "!")
                    {
                        //感情ボタンの表示
                    }

                    //スプライトの切り替え
                    if (currentInfo[currentRow].spritePass == "Player")
                    {
                        speakerImage.sprite = Resources.Load<Sprite>("Sprite/Player/Player_Left");
                    }
                    else
                    {
                        speakerImage.sprite = Resources.Load<Sprite>("Sprite/Mob/" + currentInfo[currentRow].spritePass);
                    }

                    //名前の切り替え
                    speakerName.text = currentInfo[currentRow].speakerName;

                    isTextSet = false;
                    
                }
            }
        }

        if(isEventEnd)
        {
            if(currentInfo == null)
            {
                return;
            }

            //使い終わったリストの中身を削除
            currentInfo.Clear();

            //パネルを非表示に
            eventTextPanel.SetActive(false);

            isEventEnd = false;
        }
    }
}
