using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //プレイヤーの移動
    Rigidbody playerRigid;
    Vector3 direction;
    [SerializeField] float speed = 3.0f;
    bool isRun = false;

    //プレイヤーの音
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip footsteps_SE;
    [SerializeField] AudioClip handgun_SE;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //ｘ方向、ｚ方向に移動
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x * speed, 0, z * speed);

        if (x != 0 || z!= 0)
        {
            isRun = true;
            Debug.Log(isRun + "," + x + "," + z);
            playerRigid.MovePosition(transform.position + direction * Time.deltaTime);
        }
        else
        {
            Debug.Log(isRun + ", " +"停止");
        }
    }
    void UdDate()
    {
        if(isRun)
        {
            Footsteps_SE();
        }
    }

    //足音
    void Footsteps_SE()
    {
        playerAudio.PlayOneShot(footsteps_SE);
    }

    //打ち出す
    void HandGun_SE()
    {
        playerAudio.PlayOneShot(handgun_SE);
    }
}
