using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTextControl : MonoBehaviour
{
    //�C�x���gUI
    [SerializeField] GameObject eventTextPanel;
    Image eventTextImage;
    [SerializeField] Text speakerName;
    [SerializeField] Image speakerImage;

    //���݂̃C�x���g�̃e�L�X�g��ǂݍ��ރ��X�g
    List<EventTextParser.EventTextInfo> currentInfo = new List<EventTextParser.EventTextInfo>();

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
    public static char resultText = 's';
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

    void StartText(Dictionary<string,object> message)
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
        //�C�x���g�p�l��������
        eventTextPanel.SetActive(true);

        //�s���̏�����
        currentRow = 0;

        //�C�x���gID�ƈ�v����e�L�X�g��ʂ̃��X�g�ɓǂݍ���
        foreach(EventTextParser.EventTextInfo line in EventTextParser.textInfo)
        {
            if(line.id == eventID)
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
    }

    public void TextBoxUpdate(bool happiness, bool sadness, bool anger)
    {
        if(isEventStart)
        {
            SetText(currentInfo[currentRow].textMesse);

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

            Debug.Log(currentRow);

            if(Input.GetMouseButtonDown(0))
            {
                //���S�̂̕\�����I����ĂȂ���΁A�S���\��
                if(visibleLength < text.Length)
                {
                    visibleLength = text.Length;
                    textObj.text = text;
                }
                else
                {
                    //���X�g�̗v�f���͈̔͂𒴂���Ȃ�
                    if (currentRow == currentInfo.Count - 1)
                    {
                        isEventEnd = true;
                        isEventStart = false;
                    }
                    //�s���𑗂�
                    else
                    {
                        currentRow++;
                    }

                    if(currentInfo[currentRow].state == "!")
                    {
                        //����{�^���̕\��
                        ButtonEvents.current.OnShowButtonsTrigger(happiness, sadness, anger);
                        //���ʂ�char��resultText�ɕۑ�����Ă���
                            
                    }




                    //�X�v���C�g�̐؂�ւ�
                    if (currentInfo[currentRow].spritePass == "Player")
                    {
                        speakerImage.sprite = Resources.Load<Sprite>("Sprite/Player/Player_Left");
                    }
                    else
                    {
                        speakerImage.sprite = Resources.Load<Sprite>("Sprite/Mob/" + currentInfo[currentRow].spritePass);
                    }

                    //���O�̐؂�ւ�
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

            resultText = 's';
            //�g���I��������X�g�̒��g���폜
            currentInfo.Clear();

            //�p�l�����\����
            eventTextPanel.SetActive(false);

            isEventEnd = false;
        }
    }
}
