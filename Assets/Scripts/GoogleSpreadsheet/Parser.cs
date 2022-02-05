using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser: MonoBehaviour
{
    //イベントの　id と　結果
    public static bool[][] eventListManager;
    public static bool[] eventIsOn;
    public static string[] id;

    public static bool isLoad = false;
    public static bool spawnEvents = false;

    public struct EventInformation
    {
        public string id;
        public string eventName;
        public string dayString;
        public int[]  day;
        public string timeString;
        public int time; 
        public string additionalInformationString;
        public bool additionalInformation;
    }

    public static EventInformation[] eventInformation;

    public GameObject[] events;

    int today;
    int turnNow;


    
    void EventListInitialize()
    {
        eventIsOn = new bool[eventInformation.Length];  
        eventListManager = new bool[eventInformation.Length][];
        id = new string[eventInformation.Length];
        for (int i = 0; i < eventInformation.Length; i++)
        {
            id[i] = events[i].GetComponent<EventParentClass>().id;
            eventListManager[i] = new bool[3] { false, false, false };
            eventIsOn[i] = false;
        }
    }

    public bool check = false;



    private void Update()
    {
        //TODO ターンが進んだら 条件を追加
        if (isLoad)
        {
            Debug.Log("イベントをロードします");
            EventListInitialize();
            DataParsing();
            isLoad = false;
            Debug.Log("イベントをロードしきれました");
        }

        if(TurnSystem.isTimeChange)
        { 
            List<int> turnOnEvents = new List<int>();

            turnOnEvents = ReturnAllGoodEvents();

            for (int i = 0; i < turnOnEvents.Count; i++)
            {
                eventIsOn[turnOnEvents[i]] = true;
            }
            spawnEvents = true;
            TurnSystem.isTimeChange = false;
        }
    }

    public List<int> ReturnAllGoodEvents()
    {
        List<int> ret = new List<int>();
        for (int i = 0; i < eventInformation.Length; i++)
        {
            if (CheckingEvents(eventInformation[i]))
            {
                for(int j = 0; j< eventInformation.Length; j ++ )
                {
                    if(eventInformation[i].id == id[j])
                    {
                        ret.Add(j);
                    }
                }
            }
        }
        return ret;
    }

    bool CheckingEvents(EventInformation eventInformation)
    {
        bool cont = false;
        //日付の確認
        {
            foreach (int day in eventInformation.day)
            {
                if (day == today)
                    cont = true;
            }
            if (!cont)
                return false;
        }
        //ターンの確認
        {
            cont = false;
            int temp = eventInformation.time & 0b001;
            if ( temp == 0b001 )
            {
                if (turnNow == 0 || turnNow == 1)
                    cont = true;
            }
            temp = eventInformation.time & 0b010;
            if (temp == 0b010)
            {
                if (turnNow == 1 || turnNow == 2)
                    cont = true;
            }
            temp = eventInformation.time & 0b100;
            if (temp == 0b100)
            {
                if (turnNow == 3 || turnNow == 4)
                    cont = true;
            }
            if (!cont)
                return false;
        }
        //発生条件を確認
      

        return cont;
    }

    public static int GetIndexByEventID(string id)
    {
        for(int i = 0; i< id.Length;i++)
        {
            if (id == Parser.id[i])
                return i;

        }
        return 0;
    }

    void DataParsing()
    {

        for (int i = 0; i < eventInformation.Length; i++)
        {
            //日にち
            {
                string[] dataDay = new string[eventInformation[i].dayString.Split(';').Length];
                eventInformation[i].day = new int[eventInformation[i].dayString.Split(';').Length];

                for (int j = 0; j < eventInformation[i].day.Length; j++)
                {
                    dataDay[j] = eventInformation[i].dayString.Split(';')[j];
                    eventInformation[i].day[j] = int.Parse(dataDay[j]);
                }
            }
            //時間
            {
                char[] time = new char[2];
                int timeCode = 0b000;
                time[0] = eventInformation[i].timeString[0];
                time[1] = eventInformation[i].timeString[1];
                for (int j = 0; j < 2; j++)
                {
                    switch (time[j])
                    {
                        case 'M':
                            timeCode |= 0b001;
                            break;
                        case 'D':
                            timeCode |= 0b010;
                            break;
                        case 'N':
                            timeCode |= 0b100;
                            break;
                        case ' ':
                            break;
                        default:
                            timeCode = 0b111;
                            break;
                    }
                }
                eventInformation[i].time = timeCode;
            }
            //追加条件
            {
                //TODO
            }


        }
    }

}

