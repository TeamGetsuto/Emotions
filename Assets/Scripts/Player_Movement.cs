using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody playerRigid;
    Vector3 direction;
    [SerializeField] float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x * speed, 0, z * speed);
        playerRigid.MovePosition(transform.position + direction * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
