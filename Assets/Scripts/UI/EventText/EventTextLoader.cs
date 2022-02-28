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


    string textLine;          //テキスト全体

    public string[] message;  //一行ごとに読み込んだ仮のテキストデータ
    public string[,] words;  //上のデータをTABごとに区切ったデータ

    public int rowLength;    //行数
    public int columnLength; //列数

    TextAsset textAsset;      //テキストファイルを取得するインスタンス

    
    void TextInit()
    {
        textAsset = new TextAsset(); //インスタンス生成
        //Resourcesフォルダからテキストを読み込み
        textAsset = Resources.Load("Text/EventText", typeof(TextAsset)) as TextAsset;

        textLine = textAsset.text; //テキスト全体を代入

        //Splitで一行ずつ代入した配列を用意
        message = textLine.Split('\n');

        //行と列を取得
        columnLength = message[0].Split('\t').Length;
        rowLength = message.Length;

        //2次配列を定義
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