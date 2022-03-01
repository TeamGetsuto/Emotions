using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTextControl : MonoBehaviour
{
    public static EventTextControl instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    //イベントUI
    [SerializeField] GameObject eventTextPanel;
    Image eventTextImage;
    [SerializeField] Text speakerName;
    [SerializeField] Image speakerImage;

    //現在のイベントのテキストを読み込むリスト
    List<EventTextParser.EventTextInfo> currentInfo = new List<EventTextParser.EventTextInfo>();
    //イベント結果を読み込むリスト
    List<EventTextParser.EventTextInfo> resultInfo = new List<EventTextParser.EventTextInfo>();
    bool isInitialize = false;
    public bool isSetList = false;

    public bool isEmotoChange;

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
    public static string resultText = "n";
    private void OnEnable()
    {
        EventSystem.StartListening("ShowText", StartText);
    }

    private void OnDisable()
    {
        EventSystem.StopListening("ShowText", StartText);
    }

    void UiInit()
    {
        //パネルを非表示に
        eventTextPanel.SetActive(false);

        isEmotoChange = false;

        //テキストボックスイメージの貼り付け
        eventTextImage = eventTextPanel.GetComponent<Image>();
        eventTextImage.sprite = Resources.Load<Sprite>("Sprite/EventText/textBox");
    }

    void EventStartInit()
    {
        if(isInitialize)
        {
            return;
        }

        //イベントパネルを可視化
        eventTextPanel.SetActive(true);

        //行数の初期化
        currentRow = 0;

        //感情変化のブールをfalseに
        isEmotoChange = false;

        isInitialize = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        UiInit();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isTextSet);
    }

    void StartText(Dictionary<string, object> message)
    {
        if (isEventStart)
        {
            return;
        }
        EventStarted((string)message["id"]);

        isEventStart = true;
        //Time.timeScale = 0;
    }

    void EventStarted(string eventID)
    {
        if (isSetList)
        {
            return;
        }

        EventStartInit();

        //イベントIDと一致するテキストを別のリストに読み込み
        foreach (EventTextParser.EventTextInfo line in EventTextParser.textInfo)
        {
            if (line.id == eventID)
            {
                currentInfo.Add(line);
            }
        }

        Debug.Log("リストの読み込み完了" + currentInfo.Count);

        //発話者のスプライトを切り替え
        Debug.LogWarning("スプライト確認" + currentInfo[currentRow].spritePass);
        SpriteChange(currentInfo, currentRow);
        speakerName.text = currentInfo[currentRow].speakerName;

        isSetList = true;
    }

    void EventResulted()
    {
        if (isSetList)
        {
            return;
        }

        //行数の初期化
        currentRow = 0;

        //イベント結果と一致するテキストを別のリストに読み込み
        foreach (EventTextParser.EventTextInfo line in EventTextParser.textInfo)
        {
            if (line.id == EmoteButtonControl.currentEventID && line.state == resultText)
            {
                resultInfo.Add(line);
            }
        }

        EventTextParser.EventTextInfo temp = default;
        resultInfo.Add(temp);

        Debug.Log("リストの読み込み完了" + resultInfo.Count);

        //発話者のスプライトを切り替え
        SpriteChange(resultInfo, currentRow);
        speakerName.text = resultInfo[currentRow].speakerName;

        isSetList = true;
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

    public void TextBoxUpdate(bool happiness, bool sadness, bool anger)
    {
        if (isEventStart)
        {
            if (!isEmotoChange)
            {
                EventStarted(EmoteButtonControl.currentEventID);

                SetText(currentInfo[currentRow].textMesse);

                TypeWriterEffect();

                Debug.Log(currentRow);
                
                if (Input.GetMouseButtonDown(0))
                {
                    //文全体の表示が終わってなければ、全文表示
                    if (visibleLength < text.Length)
                    {
                        visibleLength = text.Length;
                        textObj.text = text;
                    }
                    else
                    {
                        //テキストのステータスが「！」の時、感情ボタンを出現させる
                        if (currentInfo[currentRow].state == "!")
                        {
                            //感情ボタンの表示
                            ButtonEvents.current.OnShowButtonsTrigger(happiness, sadness, anger);
                        }
                        else
                        {
                            //行数を送る
                            currentRow++;
                        }

                        isTextSet = false;
                    }

                    SpriteChange(currentInfo, currentRow);

                    

                }
            }
            else
            {
                Debug.Log(isEmotoChange);
                Debug.Log("分岐突入");

                EventResulted();

                SetText(resultInfo[currentRow].textMesse);
                
                TypeWriterEffect();

                Debug.Log(currentRow);

                if (Input.GetMouseButtonDown(0))
                {
                    //文全体の表示が終わってなければ、全文表示
                    if (visibleLength < text.Length)
                    {
                        Debug.LogWarning(text + text.Length);
                        Debug.LogWarning(visibleLength);
                        visibleLength = text.Length;
                        Debug.LogWarning("クリックされました" + visibleLength);
                        textObj.text = text;
                    }
                    else
                    {
                        currentRow++;

                        if (resultInfo.Count - 1 == currentRow)
                        {
                            Debug.Log("分岐２突入");
                            currentRow--;
                            isSetList = false;
                            isEventStart = false;
                            isEventEnd = true;
                            EventSystem.TriggerEvent("StartEvent", new Dictionary<string, object> { { "id", EmoteButtonControl.currentEventID }, { "input", resultText } });
                        }

                            isTextSet = false;
                    }

                    SpriteChange(resultInfo, currentRow); 
                }
            }
        }

        if (isEventEnd)
        {
            Debug.Log("イベント終了");

            //使い終わったリストの中身を削除
            currentInfo.Clear();
            resultInfo.Clear();
          
            //パネルを非表示に
            resultText = "n";
            eventTextPanel.SetActive(false);
            isInitialize = false;

            EventSystem.TriggerEvent("EventEnded", new Dictionary<string, object> { { "id", EmoteButtonControl.currentEventID }, { "input", resultText } });

            isEventEnd = false;

            EventParentClass.isStarted = false;
        }
    }

    void TypeWriterEffect()
    {
        Debug.LogWarning("typeWriterEffect_start");
        //表示文字数が文全体の文字数より少ないとき
        if (visibleLength < text.Length)
        {
            Debug.Log(pastTime);
            pastTime += Time.deltaTime;

            //delayTimeを基準に一文字ずつ表示
            if (pastTime >= delayTime)
            {
                pastTime -= delayTime;
                visibleLength++;
                textObj.text = text.Substring(0, visibleLength);
            }
        }
    }


    void SpriteChange(List<EventTextParser.EventTextInfo> info, int row)
    {
        //スプライトの切り替え
        if (info[row].spritePass == "Player")
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Player/Player_Left");
        }
        else
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Mob/" + info[row].spritePass);
        }

        //名前の切り替え
        speakerName.text = info[row].speakerName;
    }
}
