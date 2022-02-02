using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreViewCamera : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float rotateSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        var movX = Input.GetAxis("Horizontal");
        var movZ = Input.GetAxis("Vertical");

        var move = new Vector3(movX, 0, movZ).normalized * moveSpeed;

        transform.Translate(move);

        var cameraRotationY = Input.GetAxis("Mouse X") * rotateSpeed;
        var cameraRotationX = Input.GetAxis("Mouse Y") * rotateSpeed;

        transform.Rotate(cameraRotationX, cameraRotationY, 0);
    }
}
