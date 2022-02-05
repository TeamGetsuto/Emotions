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
    const string ableScene = "SampleScene"; //�^�[���V�X�e����������V�[��

    EventParentClass eventSystem;

    //�V���O���g��
    public static TurnSystem instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += ReLoaded;
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

        DebugFunc();
    }

    //������
    void TurnInitialize()
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

            Debug.Log("Noon");
        }

        if(turnNum == 4)
        {
            isTimeChange = true;

            Debug.Log("Night");
        }
    }

    //�ēǂݍ���
    private void ReLoaded(Scene scene, LoadSceneMode mode)
    {
        //�ǂݍ��܂ꂽ�̂��Q�[���V�[���łȂ���Ώ������Ȃ�
        var currentScene = SceneManager.GetActiveScene().name;

        if(currentScene != ableScene)
        {
            return;
        }

        if (dayCounter < maxDay)
        {
            turnNum = 0;
            isTimeChange = true;
            dayCounter += 1;

            Debug.Log(dayCounter + "����");
            Debug.Log("Morning");
        }
        else
        {
            Debug.Log("Ending");
        }
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
