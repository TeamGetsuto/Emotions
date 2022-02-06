using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnSystem : MonoBehaviour
{
    public static int turnNum;         //ターン数
    public static bool isTimeChange;   //時間帯を変更するか
    public static int dayCounter;      //経過日数

    public static bool eventHasEnded;

    const int maxTurn = 6;  //一日の最大ターン数
    const int maxDay = 10;  //最大日数
    const string ableScene = "GameScene"; //ターンシステムが動けるシーン

    //シングルトン
    public static TurnSystem instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        TurnInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        TurnCalc();

#if DEBUG
        DebugFunc();
#endif
    }

    //初期化
    void TurnInitialize()
    {
        turnNum = 0;
        isTimeChange = true;
        dayCounter = 1;
    }

    //ターン数の計算
    public void TurnCalc()
    {
        if (!eventHasEnded)
            return;

        if(turnNum < maxTurn)
        {
            turnNum += 1;

            TimeChanger();

            if(turnNum == maxTurn)
            {
                Debug.Log("ReLoad");
            }
            eventHasEnded = false;
        }
    }

    //時間帯の変更
    public void TimeChanger()
    {
        if(turnNum == 2)
        {
            isTimeChange = true;

            Debug.Log("Noon");
        }

        if(turnNum == 4)
        {
            isTimeChange = true;

            Debug.Log("Night");
        }
    }

    //日にち計算
    void DayCalc()
    {
        if (turnNum == maxTurn)
        {
            turnNum = 0;

            if (dayCounter < maxDay)
            {
                dayCounter += 1;
            }
        }

        Debug.Log(dayCounter + "日目");
    }
   

    //デバッグ用
    void DebugFunc()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            turnNum += 1;

            TimeChanger();

            if(turnNum == maxTurn)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
