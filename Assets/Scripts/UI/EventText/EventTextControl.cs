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

    //�C�x���gUI
    [SerializeField] GameObject eventTextPanel;
    Image eventTextImage;
    [SerializeField] Text speakerName;
    [SerializeField] Image speakerImage;

    //���݂̃C�x���g�̃e�L�X�g��ǂݍ��ރ��X�g
    List<EventTextParser.EventTextInfo> currentInfo = new List<EventTextParser.EventTextInfo>();
    //�C�x���g���ʂ�ǂݍ��ރ��X�g
    List<EventTextParser.EventTextInfo> resultInfo = new List<EventTextParser.EventTextInfo>();

    bool isSetList = false;

    public bool isEmotoChange;

    //���̃C�x���gID
    [SerializeField] string eventNum;

    //��������
    [Header("TypeWriterEffect")]
    [SerializeField] Text textObj;
    [SerializeField] float delayTime;
    private float pastTime = 0f;
    private int visibleLength = 0;
    private string text = "";
    private bool isTextSet = false;

    //���݂̍s
    private int currentRow = 0;

    //���C�x���g�m�F�p
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
        //�p�l�����\����
        eventTextPanel.SetActive(false);

        isEmotoChange = false;

        //�e�L�X�g�{�b�N�X�C���[�W�̓\��t��
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

        //�C�x���g�p�l��������
        eventTextPanel.SetActive(true);

        //�s���̏�����
        currentRow = 0;

        //�C�x���gID�ƈ�v����e�L�X�g��ʂ̃��X�g�ɓǂݍ���
        foreach (EventTextParser.EventTextInfo line in EventTextParser.textInfo)
        {
            if (line.id == eventID)
            {
                currentInfo.Add(line);
            }
        }

        Debug.Log(currentInfo.Count);

        //���b�҂̃X�v���C�g��؂�ւ�

        if (currentInfo[currentRow].spritePass == "Player")
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Player/Player_Left");
        }
        else
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Mob/" + currentInfo[currentRow].spritePass);
        }

        speakerName.text = currentInfo[currentRow].speakerName;

        isSetList = true;
    }

    void EventResulted()
    {
        if (isSetList)
        {
            return;
        }

        //�s���̏�����
        currentRow = 0;

        //�C�x���g���ʂƈ�v����e�L�X�g��ʂ̃��X�g�ɓǂݍ���
        foreach (EventTextParser.EventTextInfo line in EventTextParser.textInfo)
        {
            if (line.id == EmoteButtonControl.currentEventID && line.state == resultText)
            {
                resultInfo.Add(line);
            }
        }

        Debug.Log(resultInfo[0].textMesse);

        //���b�҂̃X�v���C�g��؂�ւ�

        if (resultInfo[currentRow].spritePass == "Player")
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Player/Player_Left");
        }
        else
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Mob/" + resultInfo[currentRow].spritePass);
        }

        speakerName.text = resultInfo[currentRow].speakerName;

        isSetList = true;
    }

    //�\������ꕶ���ǂݍ���
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

        Debug.Log(text);
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
                    //���S�̂̕\�����I����ĂȂ���΁A�S���\��
                    if (visibleLength < text.Length)
                    {
                        visibleLength = text.Length;
                        textObj.text = text;
                    }
                    else
                    {
                        //�e�L�X�g�̃X�e�[�^�X���u�I�v�̎��A����{�^�����o��������
                        if (currentInfo[currentRow].state == "!")
                        {
                            isSetList = false;

                            //����{�^���̕\��
                            ButtonEvents.current.OnShowButtonsTrigger(happiness, sadness, anger);
                            if (isEmotoChange)
                            {
                                return;
                            }
                        }
                        //�s���𑗂�
                        else
                        {

                            currentRow++;
                        }

                        SpriteChange(currentInfo, currentRow);

                        isTextSet = false;
                    }
                }
            }
            else
            {
                Debug.Log(isEmotoChange);
                Debug.Log("����˓�");

                EventResulted();

                SetText(resultInfo[currentRow].textMesse);
                
                TypeWriterEffect();

                Debug.Log(currentRow);

                if (Input.GetMouseButtonDown(0))
                {
                    //���S�̂̕\�����I����ĂȂ���΁A�S���\��
                    if (visibleLength < text.Length)
                    {
                        visibleLength = text.Length;
                        textObj.text = text;
                    }
                    else
                    {
                        if (currentRow == resultInfo.Count - 1)
                        {
                            isSetList = false;
                            isEventStart = false;
                            isEventEnd = true;
                            EventSystem.TriggerEvent("StartEvent", new Dictionary<string, object> { { "id", EmoteButtonControl.currentEventID }, { "input", resultText } });
                        }
                        else
                        {
                            currentRow++;
                        }
                    }

                    SpriteChange(resultInfo, currentRow);

                    isTextSet = false;
                }
            }
        }

        if (isEventEnd)
        {
            if (currentInfo == null)
            {
                return;
            }

            EventSystem.TriggerEvent("EventEnded", new Dictionary<string, object> { { "id", EmoteButtonControl.currentEventID }, { "input", resultText } });
            //�g���I��������X�g�̒��g���폜
            currentInfo.Clear();
            resultInfo.Clear();
            //�p�l�����\����
            resultText = "n";
            eventTextPanel.SetActive(false);

            isEmotoChange = false;
            isEventEnd = false;
  
            EventParentClass.isStarted = false;
        }
    }

    void TypeWriterEffect()
    {
        //�\�������������S�̂̕�������菭�Ȃ��Ƃ�
        if (visibleLength < text.Length)
        {
            pastTime += Time.deltaTime;

            //delayTime����Ɉꕶ�����\��
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
        //�X�v���C�g�̐؂�ւ�
        if (info[row].spritePass == "Player")
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Player/Player_Left");
        }
        else
        {
            speakerImage.sprite = Resources.Load<Sprite>("Sprite/Mob/" + info[row].spritePass);
        }

        //���O�̐؂�ւ�
        speakerName.text = info[row].speakerName;
    }
}
