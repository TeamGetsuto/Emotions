using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    //イベント発生位置
    [SerializeField] GameObject[] eventOccurrence;

    //場所ごとに発生できるイベント
    string[][] eventPlaceNum;

    int count;            //生成するイベントの数
    int[] randEventPos;   //発生するイベントの場所(空のオブジェクトの番号)

    /// /// /// /// /// /// /// 
    //使っているかどうか確認
    bool[] isUsed;

    public static bool spawnEvents = false;

    // Start is called before the first frame update
    void Start()
    {
        //メモリの初期化
        eventPlaceNum = new string[eventOccurrence.Length][];
        isUsed = new bool[eventOccurrence.Length];
        for (int i = 0; i <eventPlaceNum.Length; ++i)
        {
            eventPlaceNum[i] = new string[eventOccurrence[i].GetComponent<EventPositionProperty>().eventNum.Length];
            eventPlaceNum[i] = eventOccurrence[i].GetComponent<EventPositionProperty>().eventNum;
            isUsed[i] = eventOccurrence[i].GetComponent<EventPositionProperty>().isUsed;
        }
    }

    public void Update()
    {
        if (!spawnEvents)
            return;

        SpawnEvents();
    }

    public void SpawnEvents()
    {
        for (int i = 0; i < Parser.eventIsOn.Length; i++)
        {
            if (Parser.eventIsOn[i])
                RandomFunction(Parser.id[i]);
        }
    }


    /// /// /// /// /// /// /// /// /// /// /// 
    //IDに合わせて位置を出力する関数
    private Transform RandomFunction(string id)
    {
        //IDに合わせた位置を保存する
        List<Transform> transforms = new List<Transform>();
        //リストから適当な位置を選ぶ：
        for(int i = 0; i<eventOccurrence.Length; i++)
        {
            //今他イベントはその位置を使っていない
            if (!isUsed[i])
            {
                //その位置には私たちのイベントIDが許される
                for(int j = 0; j< eventPlaceNum[i].Length; j++)
                {
                    if (id == eventPlaceNum[i][j])
                    {
                        //そうであれば、その位置をリストに追加します
                        transforms.Add(eventOccurrence[i].transform);
                        break;
                    }
                }
            }
        }
        //リストには入っている位置の数の確認
        int membersAmount = transforms.Count;
        //もしリストは空っぽではないと
        if (membersAmount != 0)
        {
            //そのリストからランダム位置を選んで
            int chosenPosition = Random.Range(0, membersAmount);
            Debug.Log("リストには入っているアイテムの数 "　+ membersAmount);
            //使っているフラグを設定します
            isUsed[chosenPosition] = true;
            Debug.Log("ID " + id + "イベントを" + transforms[chosenPosition] + "に配置しました");
            //プログラムに位置を返す
            return transforms[chosenPosition];
        }
        //そうでなければ
        else
        {
            //イベントを配置できなかったと出力します
            Debug.Log("ID　" + id + " イベントを配置できませんでした");
            return default;
        }
    }
    /// /// /// /// /// /// /// /// /// /// /// /// 
}