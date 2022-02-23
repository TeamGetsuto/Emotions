using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnSystem : MonoBehaviour
{
    public static int turnNum;         //�^�[����
    public static bool isTimeChange;   //���ԑт�ύX���邩
    public static int dayCounter;      //�o�ߓ���

    public static bool eventHasEnded;

    const int maxTurn = 6;  //����̍ő�^�[����
    const int maxDay = 10;  //�ő����
    const string ableScene = "GameScene"; //�^�[���V�X�e����������V�[��

    //�V���O���g��
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

    private void OnEnable()
    {
        EventSystem.StartListening("LoadedLevel", TurnInitialize);
    }

    private void Start()
    {
        TurnInitialize(null);
    }

    // Update is called once per frame
    void Update()
    {
        TurnCalc();

#if DEBUG
        DebugFunc();
#endif
    }

    //������
    void TurnInitialize(Dictionary<string, object> message)
    {
        turnNum = 0;
        isTimeChange = true;
        dayCounter = 1;
    }

    //�^�[�����̌v�Z
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

    //���ԑт̕ύX
    public void TimeChanger()
    {
        if(turnNum == 2)
        {
            isTimeChange = true;
            EventSystem.TriggerEvent("EventDestroying", null);
            Debug.Log("Noon");
        }

        if (turnNum == 4)
        {
            isTimeChange = true;
            EventSystem.TriggerEvent("EventDestroying", null);
            Debug.Log("Night");
        }
        if(turnNum == 6)
        {
         
            turnNum = 5;
            EventSystem.TriggerEvent("EventDestroying", null);
            StartCoroutine("DayChange");

        }

    }

    IEnumerator DayChange()
    {
        yield return new WaitForSeconds(6.0f);
        turnNum = 0;
        SceneManager.LoadScene(2);
    }

    //���ɂ��v�Z
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

        Debug.Log(dayCounter + "����");
    }
   

    //�f�o�b�O�p
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
