using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //�v���C���[�̈ړ�
    Rigidbody playerRig;
    Vector3 direction;
    [SerializeField] float runForce = 10;
    [SerializeField] float runForceMultiplier = 10; //�ړ����x�̓��͂ɑ΂���Ǐ]�x
    float x, z;

    //�v���C���[�̉�
    [SerializeField] AudioSource playerAud;
    [SerializeField] AudioClip footStep;

    //�v���C���[�X�v���C�g

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
        //����
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

        //���͂�����ǂ����ɓ�����
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
