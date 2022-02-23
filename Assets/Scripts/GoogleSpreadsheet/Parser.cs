using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser: MonoBehaviour
{

    #region Singleton
    public static Parser current;
    private void Awake()
    {
        if (current != null && current != this)
            Destroy(this);
        else current = this;
        
    }
    #endregion

    #region EventSystem
    private void OnEnable()
    {
        EventSystem.StartListening("LoadHasEnded", EventListInitialize);
    }
    #endregion



    //�C�x���g�́@id �Ɓ@����
    public static bool[][] eventListManager;
    public static bool[] eventIsOn;
    public static string[] id;

    private bool loadIsOver = false;


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

    void EventListInitialize(Dictionary<string, object> message)
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
        DataParsing();
        loadIsOver = true;
    }

    private float timer = 0.0f;

    private void Update()
    {
        if (!loadIsOver)
            return;

        if (TurnSystem.isTimeChange)
        {
            //���E��̑O��6�b��҂��ăC�x���g�������܂�
            timer -= Time.deltaTime;
            if (timer > 0)
                return;
            timer = 6.0f;

            Debug.Log("StartedCheckingEvents");

            List<int> turnOnEvents = new List<int>();

            for (int i = 0; i < eventIsOn.Length; i++)
            {
                eventIsOn[i] = false;
            }

            turnOnEvents = ReturnAllGoodEvents();

            Debug.Log("!!!!!!!!!" + turnOnEvents.Count);

            for (int i = 0; i < turnOnEvents.Count; i++)
            {
                eventIsOn[turnOnEvents[i]] = true;
            }

            EventSystem.TriggerEvent("SpawnEvents", null);
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
        //���t�̊m�F
        {
            foreach (int day in eventInformation.day)
            {
                if (day == TurnSystem.dayCounter)
                    cont = true;
            }
            if (!cont)
                return false;
        }
        //�^�[���̊m�F
        {
            cont = false;
            int temp = eventInformation.time & 0b001;
            if ( temp == 0b001 )
            {
                if (TurnSystem.turnNum == 0 || TurnSystem.turnNum == 1)
                    cont = true;
            }
            temp = eventInformation.time & 0b010;
            if (temp == 0b010)
            {
                if (TurnSystem.turnNum == 1 || TurnSystem.turnNum == 2)
                    cont = true;
            }
            temp = eventInformation.time & 0b100;
            if (temp == 0b100)
            {
                if (TurnSystem.turnNum == 3 || TurnSystem.turnNum == 4)
                    cont = true;
            }
            if (!cont)
                return false;
        }
        //�����������m�F
        Debug.Log("SecondCheck");

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
            //���ɂ�
            {
                string[] dataDay = new string[eventInformation[i].dayString.Split(';').Length];
                eventInformation[i].day = new int[eventInformation[i].dayString.Split(';').Length];

                for (int j = 0; j < eventInformation[i].day.Length; j++)
                {
                    dataDay[j] = eventInformation[i].dayString.Split(';')[j];
                    eventInformation[i].day[j] = int.Parse(dataDay[j]);
                }
            }
            //����
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
            //�ǉ�����
            {
                //TODO
            }


        }
    }

}

