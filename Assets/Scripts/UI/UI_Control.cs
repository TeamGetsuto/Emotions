using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] GameObject uiCanvas;
    [SerializeField] GameObject menuCanvas;

    [Header("Button")]
    [SerializeField] GameObject menuButtonObj;
    [SerializeField] GameObject returnButtonObj;
    [SerializeField] GameObject titleButtonObj;

    Button menu;
    Button reTurn;
    Button title;



    [Header("Text")]
    [SerializeField] Text turnText;
    [SerializeField] Text dayText;

    SE_Initializer seControl;
    SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        seControl = Camera.main.GetComponent<SE_Initializer>();
        sceneChanger = GetComponent<SceneChanger>();

        if (menuButtonObj == null)
        {
            Debug.LogError("Menuボタンオブジェクトが空です");
            return;
        }
        if (returnButtonObj == null)
        {
            Debug.LogError("Returnボタンオブジェクトが空です");
            return;
        }
        if (titleButtonObj == null)
        {
            Debug.LogError("Titleボタンオブジェクトが空です");
            return;
        }

        //ボタンオブジェが空じゃなければコンポーネントを取得
        menu = menuButtonObj.GetComponent<Button>();
        reTurn = returnButtonObj.GetComponent<Button>();
        title = titleButtonObj.GetComponent<Button>();

        //ボタンにイベントを設定
        menu.onClick.AddListener(MenuButtonDown);
        reTurn.onClick.AddListener(ReturnButtonDown);
        title.onClick.AddListener(TitleButtonDown);

        UI_Initialize();
    }

    //初期化処理
    void UI_Initialize()
    {
        uiCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    //メニューボタンの処理
    public void MenuButtonDown()
    {
        seControl.AudioPlay(seControl.buttonPushed, seControl.seVolume1);
        Time.timeScale = 0;
        turnText.text = "Action " + (TurnSystem.turnNum + 1) + " / 6";
        dayText.text = "Day " + TurnSystem.dayCounter;
        menuCanvas.SetActive(true);
    }

    //戻るボタンの処理
    public void ReturnButtonDown()
    {
        seControl.AudioPlay(seControl.buttonPushed, seControl.seVolume1);
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
    }

    //タイトルボタンの処理
    public void TitleButtonDown()
    {
        seControl.AudioPlay(seControl.buttonPushed, seControl.seVolume1);
        Time.timeScale = 1;
        sceneChanger.ChangeScene(0);
    }
}
