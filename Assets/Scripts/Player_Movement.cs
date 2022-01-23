using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody playerRigid;
    Vector3 direction;
    [SerializeField] float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal");
        direction = new Vector3(movement * speed, 0, 0);
        playerRigid.MovePosition(transform.position + direction * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
