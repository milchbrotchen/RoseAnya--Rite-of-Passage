using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Joseph : MonoBehaviour
{
    #region Public
    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObject;
    public float ClampAngleMin = -40.0f;
    public float ClampAngleMax = 40.0f;
    public float InputSensitivity = 150.0f;
    public float CamDistanceXToPlayer;
    public float CamDistanceYToPlayer;
    public float CamDistanceZToPlayer;
    public float MouseX;
    public float MouseY;
    public float FinalInputX;
    public float FinalInputZ;
    public float SmoothX;
    public float SmoothY;
    #endregion

    #region Private
    private Vector3 FollowPOS;
    private float RotX = 0.0f;
    private float RotY = 0.0f;
    #endregion

    void Start()
    {
        //Initializing Variables and Locking/Hiding Cursor
        Vector3 Rot = transform.localRotation.eulerAngles;
        RotY = Rot.y;
        RotX = Rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Set up Gamepad Sticks here
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        FinalInputX = MouseX; //Add Stick Input Here
        FinalInputZ = MouseY; //Add Stick Input Here

        RotY += FinalInputX * InputSensitivity * Time.deltaTime;
        RotX += FinalInputZ * InputSensitivity * Time.deltaTime;

        RotX = Mathf.Clamp(RotX, ClampAngleMin, ClampAngleMax);

        Quaternion LocalRotation = Quaternion.Euler(RotX, RotY, 0.0f);
        transform.rotation = LocalRotation;
    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    private void CameraUpdater()
    {
        Transform Target = CameraFollowObject.transform;

        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.position, step);
    }
}
