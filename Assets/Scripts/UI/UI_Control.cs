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

    SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();

        if (menuButtonObj == null)
        {
            Debug.LogError("Menu�{�^���I�u�W�F�N�g����ł�");
            return;
        }
        if (returnButtonObj == null)
        {
            Debug.LogError("Return�{�^���I�u�W�F�N�g����ł�");
            return;
        }
        if (titleButtonObj == null)
        {
            Debug.LogError("Title�{�^���I�u�W�F�N�g����ł�");
            return;
        }

        //�{�^���I�u�W�F���󂶂�Ȃ���΃R���|�[�l���g���擾
        menu = menuButtonObj.GetComponent<Button>();
        reTurn = returnButtonObj.GetComponent<Button>();
        title = titleButtonObj.GetComponent<Button>();

        //�{�^���ɃC�x���g��ݒ�
        menu.onClick.AddListener(MenuButtonDown);
        reTurn.onClick.AddListener(ReturnButtonDown);
        title.onClick.AddListener(TitleButtonDown);

        UI_Initialize();
    }

    //����������
    void UI_Initialize()
    {
        uiCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    //���j���[�{�^���̏���
    public void MenuButtonDown()
    {
        Time.timeScale = 0;
        turnText.text = "Action " + (TurnSystem.turnNum + 1) + " / 6";
        dayText.text = "Day " + TurnSystem.dayCounter;
        menuCanvas.SetActive(true);
    }

    //�߂�{�^���̏���
    public void ReturnButtonDown()
    {
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
    }

    //�^�C�g���{�^���̏���
    public void TitleButtonDown()
    {
        sceneChanger.ChangeScene(0);
    }
}
