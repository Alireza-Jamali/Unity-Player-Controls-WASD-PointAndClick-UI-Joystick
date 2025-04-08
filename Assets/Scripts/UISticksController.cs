using System;
using UnityEngine;
using UnityEngine.UI;

public class UISticksController : PlayerControllerBase
{
    [Header("UI Stick Settings")]
    public FixedJoystick moveJoystick;
    public FixedJoystick cameraJoystick;
    public float moveSpeed = 5f;
    public float cameraSpeed = 2f;

    Transform camTransform;

    void Awake()
    {
        camTransform = Camera.main?.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
        UpdateCameraFollow();
    }

    void HandleMovement()
    {
        Vector2 input = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
        Vector3 camForward = camTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = camTransform.right;
        camRight.y = 0;
        camRight.Normalize();
        
        Vector3 movement = (camRight * input.x + camForward * input.y);
        movement = Vector3.ClampMagnitude(movement, 1); 
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    void HandleCamera()
    {
        Vector2 delta = new Vector2(
            cameraJoystick.Horizontal * cameraSpeed,
            cameraJoystick.Vertical * cameraSpeed
        );
        freeLookCamera.m_XAxis.Value += delta.x;
        freeLookCamera.m_YAxis.Value -= delta.y;
    }

    void OnEnable()
    {
        if (moveJoystick) moveJoystick.gameObject.SetActive(true);
        if (cameraJoystick) cameraJoystick.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        if (moveJoystick) moveJoystick.gameObject.SetActive(false);
        if (cameraJoystick) cameraJoystick.gameObject.SetActive(false);
    }
}