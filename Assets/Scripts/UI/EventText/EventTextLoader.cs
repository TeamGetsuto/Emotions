using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTextLoader : MonoBehaviour
{
    //Singleton
    public static EventTextLoader instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }


    string textLine;          //�e�L�X�g�S��

    public string[] message;  //��s���Ƃɓǂݍ��񂾉��̃e�L�X�g�f�[�^
    public string[,] words;  //��̃f�[�^��TAB���Ƃɋ�؂����f�[�^

    public int rowLength;    //�s��
    public int columnLength; //��

    TextAsset textAsset;      //�e�L�X�g�t�@�C�����擾����C���X�^���X

    
    void TextInit()
    {
        textAsset = new TextAsset(); //�C���X�^���X����
        //Resources�t�H���_����e�L�X�g��ǂݍ���
        textAsset = Resources.Load("Text/EventText", typeof(TextAsset)) as TextAsset;

        textLine = textAsset.text; //�e�L�X�g�S�̂���

        //Split�ň�s����������z���p��
        message = textLine.Split('\n');

        //�s�Ɨ���擾
        columnLength = message[0].Split('\t').Length;
        rowLength = message.Length;

        //2���z����`
        words = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; ++i)
        {

            string[] temp = message[i].Split('\t');

            for (int n = 0; n < columnLength; ++n)
            {
                words[i, n] = temp[n];
                //Debug.Log(words[i, n]);
            }
        }

        EventTextParser.textInfo = new EventTextParser.EventTextInfo[rowLength];
        for(int i = 0;i < rowLength;++i)
        {
            EventTextParser.textInfo[i].id = words[i, 0];
            EventTextParser.textInfo[i].state = words[i, 1];
            EventTextParser.textInfo[i].spritePass = words[i, 2];
            EventTextParser.textInfo[i].speakerName = words[i, 3];
            EventTextParser.textInfo[i].textMesse = words[i, 4];

            Debug.Log(EventTextParser.textInfo[i].id);
            Debug.Log(EventTextParser.textInfo[i].state);
            Debug.Log(EventTextParser.textInfo[i].spritePass);
            Debug.Log(EventTextParser.textInfo[i].speakerName);
            Debug.Log(EventTextParser.textInfo[i].textMesse);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        TextInit();
    }
}