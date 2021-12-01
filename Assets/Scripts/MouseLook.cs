using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public Vector3 cameraOffset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraOffset = transform.position - playerBody.transform.position;
    }

    void LateUpdate()
    {
        Rotate();
        Vector3 newPosition = playerBody.transform.position + cameraOffset;
        transform.position = newPosition;
        transform.LookAt(playerBody.transform.position);
    }

    private void Rotate()
    {
        cameraOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * cameraOffset;
    }
}
