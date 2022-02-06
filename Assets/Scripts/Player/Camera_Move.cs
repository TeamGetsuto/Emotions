using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;   //プレイヤー情報格納用
    [SerializeField] Vector3 offset;      //相対距離取得用

    void Start()
    {
        //offsetを使ってカメラの位置を設定する
        transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        //新しいトランスフォームの値を代入する
        transform.position = player.transform.position + offset;
    }
}
