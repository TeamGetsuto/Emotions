using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;   //�v���C���[���i�[�p
    [SerializeField] Vector3 offset;      //���΋����擾�p

    void Start()
    {
        //offset���g���ăJ�����̈ʒu��ݒ肷��
        transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        //�V�����g�����X�t�H�[���̒l��������
        transform.position = player.transform.position + offset;
    }
}
