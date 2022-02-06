using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //プレイヤーの移動
    Rigidbody playerRig;
    Vector3 direction;
    [SerializeField] float runForce = 10;
    [SerializeField] float runForceMultiplier = 10; //移動速度の入力に対する追従度
    float x, z;

    //プレイヤーの音
    [SerializeField] AudioSource playerAud;
    [SerializeField] AudioClip footStep;

    //プレイヤースプライト

    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody>();
        playerAud = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Player_Move();
    }

    void Update()
    {
        //入力
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x == 0 && z == 0)
        {
            Player_SE();
        }
    }

    void Player_Move()
    {
        direction = Vector3.zero;

        //入力したらどっちに動くか
        direction.x = x * runForce;
        direction.z = z * runForce;

        playerRig.AddForce(runForceMultiplier * (direction - playerRig.velocity));
        //Debug.Log(direction.x + " " + direction.z + " " + x + " " + z);

    }
    void Player_SE()
    {
        playerAud.clip = footStep;
        playerAud.Play();
        //playerAud.PlayOneShot(footStep);
    }

}
