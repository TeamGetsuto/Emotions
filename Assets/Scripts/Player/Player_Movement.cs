using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //プレイヤーの移動
    Rigidbody playerRig;
    public Vector3 direction;
    [SerializeField] float runForce = 10;
    [SerializeField] float runForceMultiplier = 10; //移動速度の入力に対する追従度
    float x, z;

    //プレイヤースプライト
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody>();
        spriteRenderer = transform.Find("Player_Front").GetComponent<SpriteRenderer>();
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

        if (x == 0 && z < 0)　//正面
        {
            spriteRenderer.sprite = sprites[0];
        }
        if (x < 0)            //左
        {
            spriteRenderer.sprite = sprites[1];
        }
        if (x > 0)            //右
        {
            spriteRenderer.sprite = sprites[2];
        }
        if (z > 0 && x == 0)  //後
        {
            spriteRenderer.sprite = sprites[3];
        }
    }

    void Player_Move()
    {
        direction = Vector3.zero;

        //入力したらどっちに動くか
        //direction.x = x * runForce;
        //direction.z = z * runForce;

        direction = new Vector3(x, 0, z).normalized * runForce;

        playerRig.AddForce(runForceMultiplier * (direction - playerRig.velocity));
        //Debug.Log(direction.x + " " + direction.z + " " + x + " " + z);

        
    }
}
