using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InformationDownloading
{
    private const string googleSheetID = "1n6LDO5G4yR7RbWF15KUxDSB0ZUJAQUBxN7_6xRmXH1E";
    private const string url = "https://docs.google.com/spreadsheets/d/" + googleSheetID + "/export?gid=1434159923&format=csv";

    internal static IEnumerator DownloadData(System.Action<string> onCompleted)
        {
        yield return new WaitForEndOfFrame();

        string downloadData = null;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if(webRequest.result == UnityWebRequest.Result.ConnectionError )
            {
                Debug.Log("ダウンロードエラー: " + webRequest.error);
            }
            else
            {
                Debug.Log("ダウンロード成功しました");
                Debug.Log("データ: " + webRequest.downloadHandler.text);
                downloadData = webRequest.downloadHandler.text;
            }
        }

        onCompleted(downloadData);
        EmotionEventHandler.current.OnLoadEndingTrigger();
        }

}
