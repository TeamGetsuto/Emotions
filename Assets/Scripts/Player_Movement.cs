using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //�v���C���[�̈ړ�
    Rigidbody playerRigid;
    Vector3 direction;
    [SerializeField] float speed = 3.0f;
    bool isRun = false;

    //�v���C���[�̉�
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
        //�������A�������Ɉړ�
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
            Debug.Log(isRun + ", " +"��~");
        }
    }
    void UdDate()
    {
        if(isRun)
        {
            Footsteps_SE();
        }
    }

    //����
    void Footsteps_SE()
    {
        playerAudio.PlayOneShot(footsteps_SE);
    }

    //�ł��o��
    void HandGun_SE()
    {
        playerAudio.PlayOneShot(handgun_SE);
    }
}
