using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float verticalSpeed = 1.5f;
    public float horizontalSpeed = 1.5f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * verticalSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * horizontalSpeed * Time.deltaTime;

        xRotation += mouseX;
        yRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
    }
}
