using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Loader : MonoBehaviour
{

    private int progress;
    List<string> data = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public void Load()
    {
        StartCoroutine(InformationDownloading.DownloadData(AfterDownload));
    }

    void AfterDownload(string data)
    {
        if (data == null)
        {
            Debug.LogError("ダウンロードが失敗しました");
        }
        else
        {
            ProcessData(data);
        }
    }

    public void ProcessData(string data)
    {
        string[,] lineData = new string[data.Split(',', '\n').Length / 22-1, 22];
        Debug.Log(data.Split(',', '\n').Length / 22 -1 + " " + data.Split(',', '\n').Length % 22);
        for (int i = 22; i < data.Split(',', '\n').Length - 1; i++)
        {
            lineData[i / 22 -1, i % 22] = data.Split(',', '\n')[i];
        }

        Parser.eventInformation = new Parser.EventInformation[lineData.Length / 22];
        for (int i = 0; i < lineData.Length / 22; i++)
        {
            Parser.eventInformation[i].id = lineData[i, 0];
            Parser.eventInformation[i].eventName = lineData[i, 1];
            Parser.eventInformation[i].dayString = lineData[i, 2];
            Parser.eventInformation[i].timeString = lineData[i, 3];
            Parser.eventInformation[i].additionalInformationString = lineData[i, 5];

        }
    }


    
}


  
 

