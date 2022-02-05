using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;   //�v���C���[���i�[�p
    private Vector3 offset;      //���΋����擾�p

    void Start()
    {
        player = GameObject.Find("Player");

        // MainCamera(�������g)��player�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        //�V�����g�����X�t�H�[���̒l��������
        transform.position = player.transform.position + offset;

    }
}
