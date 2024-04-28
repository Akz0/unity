using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    public float turnSpeed = 90f;

    [SerializeField]
    public float limitUp = 80f;

    [SerializeField]
    public float limitDown = -80f;

    [SerializeField]
    public float Sensitivity = 1f;

    float yaw = 0f;
    float pitch = 0f;

    Quaternion bodyStartOrientation;
    Quaternion headStartOrientation;

    Transform head;

    void Start()
    {
        head = GetComponentInChildren<Camera>().transform;

        bodyStartOrientation = transform.localRotation;
        headStartOrientation = head.transform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void FixedUpdate()
    {
        CalculateMouseLook();
    }

    void CalculateMouseLook()
    {

        //Get the MouseRotations or their positions.
        var horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * turnSpeed;
        var vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * turnSpeed;

        yaw += horizontal;
        pitch -= vertical;

        pitch = Mathf.Clamp(pitch, limitDown, limitUp);

        //Calculate the body and head Rotation;
        var bodyRotation = Quaternion.AngleAxis(yaw, Vector3.up);
        var headRotation = Quaternion.AngleAxis(pitch, Vector3.right);

        //Create New rotations for body and head.
        transform.localRotation = bodyRotation * bodyStartOrientation;
        head.transform.localRotation = headRotation * headStartOrientation;
    }
}
