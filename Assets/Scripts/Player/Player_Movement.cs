using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //�v���C���[�̈ړ�
    Rigidbody playerRig;
    public Vector3 direction;
    [SerializeField] float runForce = 10;
    [SerializeField] float runForceMultiplier = 10; //�ړ����x�̓��͂ɑ΂���Ǐ]�x
    float x, z;

    //�v���C���[�X�v���C�g
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
        //����
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x == 0 && z < 0)�@//����
        {
            spriteRenderer.sprite = sprites[0];
        }
        if (x < 0)            //��
        {
            spriteRenderer.sprite = sprites[1];
        }
        if (x > 0)            //�E
        {
            spriteRenderer.sprite = sprites[2];
        }
        if (z > 0 && x == 0)  //��
        {
            spriteRenderer.sprite = sprites[3];
        }
    }

    void Player_Move()
    {
        direction = Vector3.zero;

        //���͂�����ǂ����ɓ�����
        //direction.x = x * runForce;
        //direction.z = z * runForce;

        direction = new Vector3(x, 0, z).normalized * runForce;

        playerRig.AddForce(runForceMultiplier * (direction - playerRig.velocity));
        //Debug.Log(direction.x + " " + direction.z + " " + x + " " + z);

        
    }
}
