using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 150f;

    public Transform playerBody;

    float xRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //make floats that are equal to system axis inputs times mouse sensitivity variable times Time.deltaTime to make it independent of framerate
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; //here we want the camera to move independently of the body, but with a limit
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamp between -90 degrees and 90 degrees

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //keeping track of rotation in a way that will allow for clamping
        //TODO ask how this works more specifically?
        playerBody.Rotate(Vector3.up * mouseX); //this makes the body rotate alongside the camera
    }
}
